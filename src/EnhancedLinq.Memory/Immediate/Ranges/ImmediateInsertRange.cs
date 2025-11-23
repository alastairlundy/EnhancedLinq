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

namespace AlastairLundy.EnhancedLinq.Memory.Immediate.Ranges;

public static partial class EnhancedLinqMemoryImmediateRange
{
    /// <summary>
    /// Inserts a collection of elements at the specified start index into the span.
    /// </summary>
    /// <param name="span">The original span to insert the range of items into.</param>
    /// <param name="elements">The collection of elements to be inserted.</param>
    /// <param name="startIndex">The zero-based starting index of the insertion.</param>
    /// <typeparam name="T">The type of elements in the span.</typeparam>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the start or end indices are out of range for the span.</exception>
    public static void InsertRange<T>(this ref Span<T> span, ICollection<T> elements, int startIndex)
    {
        if (startIndex >= 0 == false && startIndex < span.Length == false)
            throw new ArgumentOutOfRangeException(nameof(startIndex));

        int newLength = span.Length + elements.Count;
        
        span.Resize(newLength);
        
        int i = startIndex;
        
        foreach (T element in elements)
        {
            if (i > span.Length)
                throw new ArgumentOutOfRangeException(nameof(startIndex));
            
            span[i] = element;
            
            i++;
        }
    }
}