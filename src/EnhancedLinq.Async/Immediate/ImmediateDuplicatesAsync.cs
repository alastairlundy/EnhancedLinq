/*
    EnhancedLinq.Async
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/.
*/

namespace EnhancedLinq.Async.Immediate;

public static partial class EnhancedLinqAsyncImmediate
{
    /// <param name="source">The <see cref="IEnumerable{T}"/> to be searched.</param>
    /// <typeparam name="T">The type of objects in the <see cref="IEnumerable{T}"/>.</typeparam>
    extension<T>(IAsyncEnumerable<T> source) where T : notnull
    {
        /// <summary>
        /// Determines whether an <see cref="IEnumerable{T}"/> contains duplicate instances of an object.
        /// </summary>
        /// <returns>True if the <see cref="IEnumerable{T}"/> contains duplicate objects; false otherwise.</returns>
        public async Task<bool> ContainsDuplicates()
            => await ContainsDuplicates(source, EqualityComparer<T>.Default);

        /// <summary>
        /// Determines whether an <see cref="IAsyncEnumerable{T}"/> contains duplicate instances of an object.
        /// </summary>
        /// <param name="comparer">The <see cref="IEqualityComparer{T}"/> to be used to check for duplicates, uses the Default Equality Comparer if null.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation. True if duplicates are found; otherwise, false.</returns>
        public async Task<bool> ContainsDuplicates(IEqualityComparer<T>? comparer)
        {
            comparer ??= EqualityComparer<T>.Default;
            ArgumentNullException.ThrowIfNull(source);
            
            HashSet<T> hash = new(comparer: comparer);
        
            await foreach (T item in source)
            {
                bool result = hash.Add(item);

                if (!result)
                    return true;
            }

            return false;
        }
    }
}