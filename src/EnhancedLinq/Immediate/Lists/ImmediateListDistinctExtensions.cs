/*
    EnhancedLinq 
    Copyright (c) 2025-2026 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
*/

namespace EnhancedLinq.Immediate.Lists;

/// <summary>
/// 
/// </summary>
public static partial class EnhancedLinqListImmediate
{
    /// <param name="source">The list to deduplicate.</param>
    /// <typeparam name="T">The type of elements in the list.</typeparam>
    extension<T>(List<T> source)
    {
        /// <summary>
        /// Creates a new <see cref="List{T}"/> with distinct elements from the source list.
        /// </summary>
        /// <returns>The new list with distinct elements from the source list.</returns>
        public List<T> Distinct()
            =>
                source.Distinct(EqualityComparer<T>.Default);
        
        /// <summary>
        /// Creates a new <see cref="List{T}"/> with distinct elements from the source list.
        /// </summary>
        /// <param name="equalityComparer">The equality comparer to use.</param>
        /// <returns>The new list with distinct elements from the source list.</returns>
        public List<T> Distinct(IEqualityComparer<T> equalityComparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(equalityComparer);

#if NET8_0_OR_GREATER
            HashSet<T> hash = new(capacity: source.Count / 10, comparer: equalityComparer);
#else
            HashSet<T> hash = new(comparer: equalityComparer);
#endif
            List<T> output = new(capacity: source.Count / 10);

            for (int index = 0; index < source.Count; index++)
            {
                T item = source[index];
                bool result = hash.Add(item);

                if (result == false)
                    output.Add(item);
            }

            return output;
        }
    }


    /// <param name="source">The array to de-duplicate.</param>
    /// <typeparam name="T">The type of elements in the array.</typeparam>
    extension<T>(T[] source)
    {
        /// <summary>
        /// Creates a new array with distinct elements from the source array.
        /// </summary>
        /// <returns>The new array with distinct elements from the source array.</returns>
        public T[] Distinct()
            =>
                source.Distinct(EqualityComparer<T>.Default);
        
        /// <summary>
        /// Creates a new array with distinct elements from the source array.
        /// </summary>
        /// <param name="equalityComparer">The equality comparer to use.</param>
        /// <returns>The new array with distinct elements from the source array.</returns>
        public T[] Distinct(IEqualityComparer<T> equalityComparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(equalityComparer);

#if NET8_0_OR_GREATER
            HashSet<T> hash = new(capacity: source.Length / 10, comparer: equalityComparer);
#else
            HashSet<T> hash = new(comparer: equalityComparer);
#endif
        
            T[] output = new T[source.Length];

            int count = 0;

            for (int index = 0; index < source.Length; index++)
            {
                T item = source[index];
            
                bool result = hash.Add(item);

                if (result == false)
                {
                    output[count] = source[index];
                    count++;
                }
            }
        
            Array.Resize(ref output, count);
        
            return output;
        }
    }
}