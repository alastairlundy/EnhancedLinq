/*
    EnhancedLinq.Memory
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
    */

namespace EnhancedLinq.Memory.Immediate;

public static partial class EnhancedLinqMemoryImmediate
{
    /// <param name="source">The <see cref="Span{T}"/> to search.</param>
    /// <typeparam name="T">The type of elements within the <see cref="Span{T}"/>.</typeparam>
    extension<T>(Span<T> source) where T : notnull
    {
        /// <summary>
        /// Gets a collection of indices within the given <see cref="Span{T}"/> where the specified value occurs.
        /// </summary>
        /// <param name="item">The value to find in the <see cref="Span{T}"/>.</param>
        /// <returns>A collection of indices that represent the occurrences of item in <see cref="Span{T}"/></returns>
        public ICollection<int> IndicesOf(T item)
        {
            InvalidOperationException.ThrowIfSpanIsEmpty(source);

            List<int> indices = new();

            for (int index = 0; index < source.Length; index++)
            {
                if (item is not null && item.Equals(source[index]))
                {
                    indices.Add(index);
                }
            }
        
            return indices;
        }

        /// <summary>
        /// Gets a collection of indices within the given <see cref="Span{T}"/> where items match the predicate condition.
        /// </summary>
        /// <param name="predicate">The predicate to use to select items to retrieve the index of.</param>
        /// <returns>A collection of indices that represent all items in the <see cref="Span{T}"/> that match the predicate.</returns>
        public ICollection<int> IndicesOf(Func<T, bool> predicate)
        {            
            InvalidOperationException.ThrowIfSpanIsEmpty(source);
            ArgumentNullException.ThrowIfNull(predicate);
            
            List<int> indices = new();
        
            for (int index = 0; index < source.Length; index++)
            {
                if(predicate(source[index]))
                    indices.Add(index);
            }
        
            return indices;
        }
    }

    /// <param name="source">The <see cref="ReadOnlySpan{T}"/> to search.</param>
    /// <typeparam name="T">The type of elements within the <see cref="ReadOnlySpan{T}"/>.</typeparam>
    extension<T>(ReadOnlySpan<T> source) where T : notnull
    {
        /// <summary>
        /// Gets a collection of indices within the given <see cref="ReadOnlySpan{T}"/> where the specified value occurs.
        /// </summary>
        /// <param name="item">The value to find in the <see cref="ReadOnlySpan{T}"/>.</param>
        /// <returns>A collection of indices that represent the occurrences of item in <see cref="ReadOnlySpan{T}"/>.</returns>
        public ICollection<int> IndicesOf(T item)
        {
            InvalidOperationException.ThrowIfSpanIsEmpty(source);

            List<int> indices = new();

            for (int index = 0; index < source.Length; index++)
            {
                if (item is not null && item.Equals(source[index]))
                {
                    indices.Add(index);
                }
            }
        
            return indices;
        }

        /// <summary>
        /// Gets a collection of indices within the given <see cref="ReadOnlySpan{T}"/> where items match the predicate condition.
        /// </summary>
        /// <param name="predicate">The predicate to use to select items to retrieve the index of.</param>
        /// <returns>A collection of indices that represent all items in the <see cref="ReadOnlySpan{T}"/> that match the predicate.</returns>
        public ICollection<int> IndicesOf(Func<T, bool> predicate)
        {            
            InvalidOperationException.ThrowIfSpanIsEmpty(source);
            ArgumentNullException.ThrowIfNull(predicate);
            
            List<int> indices = new();
        
            for (int index = 0; index < source.Length; index++)
            {
                if(predicate(source[index]))
                    indices.Add(index);
            }
        
            return indices;
        }
    }
}