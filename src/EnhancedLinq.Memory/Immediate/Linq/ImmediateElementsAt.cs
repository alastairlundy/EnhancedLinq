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
    /// <param name="source">The source <see cref="Memory{T}"/> .</param>
    /// <typeparam name="T">The type of items stored in the Memory.</typeparam>
    extension<T>(Memory<T> source)
    {
        /// <summary>
        /// Returns the element at the specified index in the source <see cref="Memory{T}"/>.
        /// </summary>
        /// <param name="index">The zero-based index of the element to be retrieved.</param>
        /// <returns>A new source <see cref="Memory{T}"/> containing a single element starting at the specified index in the Memory.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the source <see cref="Memory{T}"/> has no elements or the index is out of range.</exception>
        public T ElementAt(int index)
        {
            if (source.Length == 0)
                throw new ArgumentOutOfRangeException(nameof(index));

            Memory<T> items = ElementsAt(source, index, 1);

            return First(items.Span);
        }
    }
        
    /// <summary>
    /// Returns a new <see cref="Memory{T}"/> containing the specified number of elements starting at the specified index.
    /// </summary>
    /// <param name="source">The source <see cref="Memory{T}"/>.</param>
    /// <param name="index">The zero-based index of the element to be retrieved.</param>
    /// <param name="count">The number of elements to include in the returned Memory.</param>
    /// <typeparam name="T">The type of items stored in the Memory.</typeparam>
    /// <returns>A new <see cref="Memory{T}"/> containing the specified number of elements starting at the specified index.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the source <see cref="Memory{T}"/> has no elements or the index is out of range.</exception>
    public static Memory<T> ElementsAt<T>(this Memory<T> source, int index, int count)
    {
        if (source.Length == 0)
            throw new ArgumentOutOfRangeException(nameof(index));

#if NET8_0_OR_GREATER
        return source[new Range(index, index + count)];
#else
        return source.Slice(index, index + count);
#endif
    }
}