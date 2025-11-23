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

namespace AlastairLundy.EnhancedLinq.Immediate.Linq;

public static partial class EnhancedLinqImmediateList
{
    /// <param name="source">The source array.</param>
    /// <typeparam name="T">The type of elements in the source array.</typeparam>
    extension<T>(T[] source)
    {
        /// <summary>
        /// Takes the last 'count' elements from a source array and returns them as a new array.
        /// </summary>
        /// <param name="count">The number of elements to take from the end of the source array.</param>
        /// <returns>An array containing the last 'count' elements from the source array.</returns>
        /// <exception cref="ArgumentException">Thrown when the count is less than 0 or greater than the length of the source array.</exception>
        public T[] TakeLast(int count)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentOutOfRangeException.ThrowIfNegative(count);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(count, source.Length);

            T[] output = new T[count];
        
            for (int index = source.Length - 1; index > count; index--)
            {
                output[index] = source[index];
            }

            return output;
        }
    }

    /// <param name="source">The source <see cref="IList{T}"/>.</param>
    /// <typeparam name="T">The type of elements in the source.</typeparam>
    extension<T>(IList<T> source)
    {
        /// <summary>
        /// Takes the last 'count' elements from a source <see cref="IList{T}"/> and returns them as a new <see cref="IList{T}"/>.
        /// </summary>
        /// <param name="count">The number of elements to take from the end of the source.</param>
        /// <returns>A <see cref="IList{T}"/> containing the last 'count' elements from the source.</returns>
        /// <exception cref="ArgumentException">Thrown when the count is less than 0 or greater than the length/count of the source <see cref="IList{T}"/>.</exception>
        public IList<T> TakeLast(int count)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentOutOfRangeException.ThrowIfNegative(count);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(count, source.Count);
            
            List<T> output = new List<T>(capacity: count);

            for (int index = source.Count - 1; index > count; index++)
            {
                output.Add(source[index]);
            }

            return output;
        }
    }


    /// <param name="source">The source collection.</param>
    /// <typeparam name="T">The type of elements in the source collection.</typeparam>
    extension<T>(ICollection<T> source)
    {
        /// <summary>
        /// Takes the last 'count' elements from a source collection and returns them as a new collection.
        /// </summary>
        /// <param name="count">The number of elements to take from the end of the source collection.</param>
        /// <returns>A collection containing the last 'count' elements from the source collection.</returns>
        /// <exception cref="ArgumentException">Thrown when the count is less than 0 or greater than the length/count of the source collection.</exception>
        public ICollection<T> TakeLast(int count)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentOutOfRangeException.ThrowIfNegative(count);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(count, source.Count);
            
            List<T> output = new List<T>(capacity: count);
            
            int index = 0;
            foreach (T item in source.Reverse())
            {
                if (index <= count)
                {
                    output.Add(item);
                }

                index++;
            }

            return output;
        }
    }
}