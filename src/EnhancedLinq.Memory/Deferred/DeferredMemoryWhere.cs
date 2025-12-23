/*
    EnhancedLinq.Memory
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
*/

using EnhancedLinq.Memory.Deferred.Enumerables;

namespace EnhancedLinq.Memory.Deferred;

public static partial class EnhancedLinqMemoryDeferred
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <typeparam name="T"></typeparam>
    extension<T>(Memory<T> source)
    {
        /// <summary>
        /// Filters elements from the specified <see cref="Memory{T}"/> segment based on a specified predicate.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the <see cref="Memory{T}"/> segment.</typeparam>
        /// <param name="predicate">A function that evaluates each element to determine if it should be included in the returned sequence.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> that contains elements satisfying the condition specified by the predicate.</returns>
        public IEnumerable<T> Where(Func<T, bool> predicate)
        {
            ArgumentNullException.ThrowIfNull(predicate);
            
            return new MemoryWhereEnumerable<T>(source, predicate);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <typeparam name="T"></typeparam>
    extension<T>(ReadOnlyMemory<T> source)
    {
        /// <summary>
        /// Filters a sequence of elements from the provided <see cref="ReadOnlyMemory{T}"/> segment based on a predicate.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the <see cref="ReadOnlyMemory{T}"/> segment.</typeparam>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> that contains elements from the <see cref="ReadOnlyMemory{T}"/> segment that satisfy the condition specified by the predicate.</returns>
        public IEnumerable<T> Where(Func<T, bool> predicate)
        {
            ArgumentNullException.ThrowIfNull(predicate);
            
            return new MemoryWhereEnumerable<T>(source, predicate);
        }
    }
}