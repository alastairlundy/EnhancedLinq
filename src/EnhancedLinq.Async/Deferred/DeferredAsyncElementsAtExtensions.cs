using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using EnhancedLinq.Async.Deferred.Enumerators;

namespace EnhancedLinq.Async.Deferred;

/// <summary>
/// Provides extension methods for deferred retrieval of elements at specified positions in asynchronous sequences.
/// </summary>
public static class DeferredAsyncElementsAtExtensions
{
    /// <param name="source">The <see cref="IAsyncEnumerable{T}"/> from which to retrieve elements.</param>
    /// <typeparam name="TSource">The type of the elements in the source and returned <see cref="IAsyncEnumerable{T}"/>.</typeparam>
    extension<TSource>(IAsyncEnumerable<TSource> source)
    {
        /// <summary>Retrieves elements from the source at the given indices.</summary>
        /// <returns>An async enumerable containing the elements at the specified positions.</returns>
        /// <param name="indices">The sequence of zero‑based indices to retrieve elements.</param>
        public IAsyncEnumerable<TSource> ElementsAt(IAsyncEnumerable<int> indices)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(indices);

            return new CustomAsyncEnumerable<TSource>(ct => new AsyncElementsAtEnumerator<TSource>(source, indices, ct));
        }

        /// <summary>
        /// Retrieves elements from the source at the given range of indices.
        /// </summary>
        /// <param name="range">The <see cref="Range"/> from which to retrieve elements.</param>
        /// <typeparam name="TSource">The type of the elements in the source and returned <see cref="IAsyncEnumerable{T}"/>.</typeparam>
        /// <returns>An async enumerable containing the elements at the specified positions.</returns>
        public IAsyncEnumerable<TSource> ElementsAt(Range range)
        {
            ArgumentNullException.ThrowIfNull(source);

            return Core(source, range);
        }

        private static async IAsyncEnumerable<TSource> Core(IAsyncEnumerable<TSource> src, Range range,
            [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            if (!range.Start.IsFromEnd && !range.End.IsFromEnd)
            {
                int start = range.Start.Value;
                int end = range.End.Value;

                ArgumentOutOfRangeException.ThrowIfNegative(start, nameof(range));
                ArgumentOutOfRangeException.ThrowIfNegative(end, nameof(range));

                if (end < start)
                {
                    throw new ArgumentOutOfRangeException(nameof(range));
                }

                if (end == start)
                {
                    yield break;
                }

                int index = 0;
                await foreach (TSource item in src.WithCancellation(cancellationToken).ConfigureAwait(false))
                {
                    if (index >= start && index < end)
                    {
                        yield return item;
                    }

                    index++;

                    if (index >= end)
                    {
                        break;
                    }
                }

                yield break;
            }

            TSource[] array = await src.ToArrayAsync().ConfigureAwait(false);
            (int offset, int length) = range.GetOffsetAndLength(array.Length);

            for (int i = 0; i < length; i++)
            {
                cancellationToken.ThrowIfCancellationRequested();
                yield return array[offset + i];
            }
        }

        /// <summary>
        /// Retrieves elements from the source at the specified indices.
        /// </summary>
        /// <param name="startIndex">The zero-based start index to retrieve elements from</param>
        /// <param name="count">The number of elements to retrieve</param>
        /// <returns>An asynchronous sequence containing the elements at the specified positions.</returns>
        public IAsyncEnumerable<TSource> ElementsAt(int startIndex, int count)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentOutOfRangeException.ThrowIfNegative(startIndex);
            ArgumentOutOfRangeException.ThrowIfNegative(count);

            if (count == 0)
            {
                return Array.Empty<TSource>().ToAsyncEnumerable();
            }

            IAsyncEnumerable<int> sequence = startIndex.GenerateNumberRange(count, 1);

            return source.ElementsAt(sequence);
        }
    }
}
