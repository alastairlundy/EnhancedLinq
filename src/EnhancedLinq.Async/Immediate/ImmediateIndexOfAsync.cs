/*
    EnhancedLinq.Async
    Copyright (c) 2025-2026 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/.
*/

namespace EnhancedLinq.Async.Immediate;

public static partial class EnhancedLinqAsyncImmediate
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="source">The <see cref="IAsyncEnumerable{T}"/> to be searched.</param>
    /// <typeparam name="T">The type of elements in the sequence.</typeparam>
    extension<T>(IAsyncEnumerable<T> source)
    {
        /// <summary>
        /// Gets the first index of the first element that matches the predicate condition.
        /// </summary>
        /// <param name="predicate">The predicate condition to check elements of the sequence against.</param>
        /// <returns>The first index of the first element in the sequence to match the predicate condition,
        /// if the sequence contains any elements that match the predicate condition, returns -1 otherwise.
        /// </returns>
        public async Task<int> IndexOfAsync(Func<T, bool> predicate)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(predicate);
        
            int index = 0;

            await foreach (T item in source)
            {
                if (predicate(item))
                    return index;

                index++;
            }

            return -1;
        }
        
        /// <summary>
        /// Gets the first index of an element in a sequence.
        /// </summary>
        /// <param name="obj">The element to get the index of.</param>
        /// <returns>The first index of an element in a sequence, if the sequence contains the element, returns -1 otherwise.</returns>
        public async Task<int> IndexOfAsync(T obj)
        {
            ArgumentNullException.ThrowIfNull(source);
        
            int index = 0;
                
            await foreach (T item in source)
            {
                if (item is not null && item.Equals(obj))
                {
                    return index;
                }
                    
                index++;
            }
        
            return -1;
        }
    }
}