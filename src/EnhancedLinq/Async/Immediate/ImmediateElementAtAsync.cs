/*
    EnhancedLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
    */

using System.Threading.Tasks;

namespace EnhancedLinq.Async.Immediate;

public static partial class EnhancedLinqAsyncImmediate
{
    /// <param name="source">The sequence to retrieve the element from.</param>
    /// <typeparam name="T">The type of elements in the sequence.</typeparam>
    extension<T>(IAsyncEnumerable<T> source)
    {
        /// <summary>
        /// Retrieves the element at a specified index from the sequence.
        /// </summary>
        /// <param name="index">The zero-based index of the element to retrieve.</param>
        /// <returns>The element at the specified index in the sequence, or throws an exception if no such element exists.</returns>
        /// <exception cref="ArgumentException">Thrown when no element is found at the specified index.</exception>
        public async Task<T> ElementAtAsync(int index)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentOutOfRangeException.ThrowIfNegative(index);
            
            int i = 0;

            await foreach (T item in source)
            {
                if (i == index)
                {
                    return item;
                }

                ++i;
            }

            throw new ArgumentException(Resources.Exceptions_ValueNotFound_AtIndex.Replace("{y}", nameof(source))
                .Replace("{x}",$"{index}"));
        }
    }
}