/*
        EnhancedLinq 
        Copyright (c) 2025 Alastair Lundy
        
       Licensed under the Apache License, Version 2.0 (the "License");
       you may not use this file except in compliance with the License.
       You may obtain a copy of the License at

           http://www.apache.org/licenses/LICENSE-2.0

       Unless required by applicable law or agreed to in writing, software
       distributed under the License is distributed on an "AS IS" BASIS,
       WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
       See the License for the specific language governing permissions and
       limitations under the License.
   */

using System;
using System.Collections.Generic;

namespace AlastairLundy.EnhancedLinq.Immediate;

/// <summary>
/// 
/// </summary>
public static partial class EnhancedLinqImmediate
{
    /// <param name="source">The list to de-duplicate.</param>
    /// <typeparam name="T">The type of elements in the list.</typeparam>
    extension<T>(List<T> source)
    {
        /// <summary>
        /// Creates a new <see cref="List{T}"/> with distinct elements from the source list.
        /// </summary>
        /// <returns>The new list with distinct elements from the source list.</returns>
        public List<T> Distinct()
            => Distinct(source, EqualityComparer<T>.Default);
        
        /// <summary>
        /// Creates a new <see cref="List{T}"/> with distinct elements from the source list.
        /// </summary>
        /// <param name="equalityComparer">The equality comparer to use.</param>
        /// <returns>The new list with distinct elements from the source list.</returns>
        public List<T> Distinct(IEqualityComparer<T> equalityComparer)
        {
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
            => Distinct(source, EqualityComparer<T>.Default);
        
        /// <summary>
        /// Creates a new array with distinct elements from the source array.
        /// </summary>
        /// <param name="equalityComparer">The equality comparer to use.</param>
        /// <returns>The new array with distinct elements from the source array.</returns>
        public T[] Distinct(IEqualityComparer<T> equalityComparer)
        {
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