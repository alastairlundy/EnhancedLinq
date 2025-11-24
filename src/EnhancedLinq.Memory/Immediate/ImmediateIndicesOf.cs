/*
    EnhancedLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
    */

namespace AlastairLundy.EnhancedLinq.Memory.Immediate;

/// <summary>
/// 
/// </summary>
public static partial class EnhancedLinqMemoryImmediate
{
    /// <param name="source">The initial span to search.</param>
    /// <typeparam name="T">The type of elements within the span.</typeparam>
    extension<T>(Span<T> source) where T : notnull
    {
        /// <summary>
        /// Gets a collection of indices within the given span where the specified value occurs.
        /// </summary>
        /// <param name="item">The value to find in the span.</param>
        /// <returns>A collection of indices that represent the occurrences of item in span.</returns>
        public ICollection<int> IndicesOf(T item)
        {
            InvalidOperationException.ThrowIfSpanIsEmpty(source);

            List<int> indices = new List<int>();

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
        /// Gets a collection of indices within the given span where items match the predicate condition.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>A collection of indices that represent all items in the span that match the predicate.</returns>
        public ICollection<int> IndicesOf(Func<T, bool> predicate)
        {            
            InvalidOperationException.ThrowIfSpanIsEmpty(source);
            ArgumentNullException.ThrowIfNull(predicate);
            
            List<int> indices = new List<int>();
        
            for (int index = 0; index < source.Length; index++)
            {
                if(predicate(source[index]))
                    indices.Add(index);
            }
        
            return indices;
        }
    }
}