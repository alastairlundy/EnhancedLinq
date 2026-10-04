using System.Runtime.CompilerServices;
using System.Threading;

namespace EnhancedLinq.Async.Deferred;

/// <summary>
/// Provides a set of extension methods for performing deferred filtering operations
/// on asynchronous enumerable sequences.
/// </summary>
public static class DeferredAsyncWhereExtensions
{
    extension<T>(IAsyncEnumerable<T> source)
        where T : notnull
    {
        /// <summary>
        /// Filters the elements of an asynchronous sequence based on an asynchronous predicate.
        /// </summary>
        /// <param name="selector"> A function that represents the asynchronous predicate to test each element for a condition.
        /// </param>
        /// <returns> An asynchronous sequence that contains elements from the input sequence that satisfy the condition
        /// specified by the predicate. </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when the <paramref name="selector"/> argument is null.
        /// </exception>
        public IAsyncEnumerable<T> WhereAsync(Func<T, Task<bool>> selector)
        {
            ArgumentNullException.ThrowIfNull(selector);

            return WhereInternalAsync(selector);

            async IAsyncEnumerable<T> WhereInternalAsync(Func<T, Task<bool>> selectorInternal,
                [EnumeratorCancellation] CancellationToken cancellationToken = default)
            {
                await foreach (T item in source.WithCancellation(cancellationToken).ConfigureAwait(false))
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    bool result = await selectorInternal(item).ConfigureAwait(false);

                    if (result)
                        yield return item;
                }
            }
        }
    }
}
