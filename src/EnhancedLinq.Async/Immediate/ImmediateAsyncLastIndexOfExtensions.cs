/*
    EnhancedLinq.Async
    Copyright (c) 2025-2026 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
*/

using System.Linq;

namespace EnhancedLinq.Async.Immediate;

/// <summary>
/// Extension methods for asynchronous operations to find the last index of an element in a sequence.
/// </summary>
public static class ImmediateAsyncLastIndexOfExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="source">The <see cref="IAsyncEnumerable{T}"/> to be searched.</param>
    /// <typeparam name="T">The type of elements in the sequence.</typeparam>
    extension<T>(IAsyncEnumerable<T> source)
    {
        /// <summary>
        /// Gets the index of the last element that matches the predicate condition.
        /// </summary>
        /// <remarks>
        /// This method performs a single forward pass over the sequence, tracking the index of the most recent element
        /// that satisfies the predicate, without buffering the sequence.
        /// </remarks>
        /// <param name="predicate">The predicate condition to check elements of the <see cref="IAsyncEnumerable{T}"/> against.</param>
        /// <returns>The index of the last element in the sequence to match the predicate condition,
        /// if the <see cref="IAsyncEnumerable{T}"/> contains any elements that match the predicate condition, returns -1 otherwise.
        /// </returns>
        public async Task<int> LastIndexOfAsync(Func<T, bool> predicate)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(predicate);

            int index = 0;
            int matchIndex = -1;

            await foreach (T item in source.ConfigureAwait(false))
            {
                if (predicate(item))
                {
                    matchIndex = index;
                }

                index++;
            }

            return matchIndex;
        }

        /// <summary>
        /// Gets the last index of an element in a <see cref="IAsyncEnumerable{T}"/>.
        /// </summary>
        /// <remarks>
        /// This method performs a single forward pass over the <see cref="IAsyncEnumerable{T}"/>, tracking the index of the most recent element
        /// that equals obj, without buffering the sequence.
        /// </remarks>
        /// <param name="obj">The element to get the last index of.</param>
        /// <returns>The last index of an element in a <see cref="IAsyncEnumerable{T}"/>, if the sequence contains the element, returns -1 otherwise.</returns>
        public async Task<int> LastIndexOfAsync(T obj)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(obj);

            int index = 0;
            int matchIndex = -1;

            await foreach (T item in source.ConfigureAwait(false))
            {
                if (item is not null && item.Equals(obj))
                {
                    matchIndex = index;
                }

                index++;
            }

            return matchIndex;
        }
    }
}