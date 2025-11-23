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
using AlastairLundy.EnhancedLinq.Internals.Localizations;

namespace AlastairLundy.EnhancedLinq.Immediate.Ranges;

/// <summary>
/// Provides extended functionality for working with ranges of elements in lists,
/// including retrieving specified ranges of elements by indices or by count.
/// </summary>
public static partial class EnhancedLinqImmediateRange
{
    /// <param name="list">The source list from which to extract the range.</param>
    /// <typeparam name="T">The type of elements in this list and the returned list.</typeparam>
    extension<T>(IList<T> list)
    {
        /// <summary>
        /// Returns a new list containing elements from this list at the specified start index to a distance of 'count' elements.
        /// </summary>
        /// <param name="startIndex">The starting index (inclusive) of the range in the original list.</param>
        /// <param name="count">The number of elements to include in the returned range.</param>
        /// <returns>A new list containing the specified range of elements from the source list.</returns>
        /// <exception cref="IndexOutOfRangeException">Thrown if the start index is out of range for the original list.</exception>
        public IList<T> GetRange(int startIndex, int count)
        {
            ArgumentNullException.ThrowIfNull(list);
            ArgumentOutOfRangeException.ThrowIfNegative(startIndex);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(count);
            
            if (list.Count >= startIndex + count)
            {
                throw new IndexOutOfRangeException(Resources.Exceptions_IndexOutOfRange
                    .Replace("{x}", $"{count}")
                    .Replace("{y}", "0")
                    .Replace("{z}", $"{list.Count}"));
            }
        
            List<T> output = new List<T>();
        
            int limit = startIndex + count;

            for (int index = startIndex; index < limit; index++)
            {
                output.Add(list[index]);
            }
                
            return output;
        }

        /// <summary>
        /// Retrieves a specified range of elements from the source list.
        /// 
        /// The indices are 0-based, meaning the first element is at index 0 and the last element is at index Count - 1.
        /// </summary>
        /// <param name="indices">A collection of 0-based indices specifying the range of elements to retrieve.</param>
        /// <returns>A list containing the specified range of elements.</returns>
        /// <exception cref="IndexOutOfRangeException">Thrown if any index is out of range
        /// (less than 0 or greater than or equal to Count).</exception>
        public IList<T> GetRange(ICollection<int> indices)
        {
            ArgumentNullException.ThrowIfNull(list);
            ArgumentNullException.ThrowIfNull(indices);

            List<T> output = new();

            foreach (int index in indices)
            {
                ArgumentOutOfRangeException.ThrowIfNegative(index);
                ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index, list.Count);

                output.Add(list[index]);
            }

            return output;
        }
    }
}