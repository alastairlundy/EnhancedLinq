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