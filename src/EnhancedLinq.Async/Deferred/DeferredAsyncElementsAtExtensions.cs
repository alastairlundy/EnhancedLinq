/*
    EnhancedLinq.Async
    Copyright (c) 2025-2026 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/.
*/

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
        
            return new CustomAsyncEnumerable<TSource>(
                new AsyncElementsAtEnumerator<TSource>(source, indices));
        }

        /// <summary>
        /// Retrieves elements from the source at the given range of indices.
        /// </summary>
        /// <param name="range">The <see cref="Range"/> from which to retrieve elements.</param>
        /// <typeparam name="TSource">The type of the elements in the source and returned <see cref="IAsyncEnumerable{T}"/>.</typeparam>
        /// <returns>An async enumerable containing the elements at the specified positions.</returns>
        public IAsyncEnumerable<TSource> ElementsAt(Range range)
            => source.ElementsAt(range.Start.Value, Math.Abs(range.Start.Value - range.End.Value));

        /// <summary>
        /// Retrieves elements from the source at the specified indices.
        /// </summary>
        /// <param name="startIndex">The zero-based start index to retrieve elements from</param>
        /// <param name="count">The number of elements to retrieve</param>
        /// <returns>An asynchronous sequence containing the elements at the specified positions.</returns>
        public IAsyncEnumerable<TSource> ElementsAt(int startIndex, int count)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(count);

            IAsyncEnumerable<int> sequence = startIndex.GenerateNumberRange(count, 1);
       
            return source.ElementsAt(sequence);
        }
    }
}