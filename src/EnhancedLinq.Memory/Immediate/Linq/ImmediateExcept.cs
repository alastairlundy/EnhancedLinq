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
    /// <param name="first">The first Span to search.</param>
    /// <typeparam name="T">The type of items stored in the span.</typeparam>
    extension<T>(Span<T> first) where T : IEquatable<T>
    {
        /// <summary>
        /// Returns a new Span with all the elements of two Spans that are only in one Span and not the other.
        /// </summary>
        /// <param name="second">The second Span to search.</param>
        /// <returns>A new Span with all the elements of Span One and Span Two that were not in the other Span.</returns>
        public Span<T> Except(Span<T> second)
        {
            T[] output = new  T[first.Length + second.Length];
            int index = 0;

            foreach (T item in first)
            {
                if (second.Contains(item) == false)
                {
                    output[index] = item;
                    index++;
                }
            }

            foreach (T item in second)
            {
                if(first.Contains(item) == false)
                {
                    output[index] = item;
                    index++;
                }
            }
        
            Array.Resize(ref output, index);

            return new Span<T>(output);
        }
    }
}