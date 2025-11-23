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

public static partial class EnhancedLinqMemoryImmediate
{
    /// <param name="span">The span whose indices will be generated.</param>
    /// <typeparam name="T">The type of the elements in the span.</typeparam>
    extension<T>(Span<T> span)
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ICollection<int> Index()
            => Index(span, 0);

        /// <summary>
        /// Returns a collection of indices of elements within the given span starting from the specified start index.
        /// </summary>
        /// <param name="startIndex">The starting index from which to generate the indices.</param>
        /// <returns>A collection of integers representing the indices of the elements in the span starting from the provided start index.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the startIndex is negative or exceeds the span length.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the span is empty.</exception>
        public ICollection<int> Index(int startIndex)
        {
            InvalidOperationException.ThrowIfSpanIsEmpty(span);
            ArgumentOutOfRangeException.ThrowIfNegative(startIndex);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(startIndex, span.Length);

            List<int> output = new();
        
            for (int i = 0; i < span.Length; i++)
            {
                if(i >= startIndex)
                    output.Add(i);
            }
        
            return output;
        }
    }
}