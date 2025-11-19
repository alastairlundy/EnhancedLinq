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

namespace AlastairLundy.EnhancedLinq.Memory.Immediate;

public static partial class EnhancedLinqMemoryImmediate
{
    /// <param name="span">The <see cref="Span{T}"/> to search</param>
    /// <typeparam name="T">The type of elements within the <see cref="Span{T}"/>.</typeparam>
    extension<T>(Span<T> span)
    {
        /// <summary>
        /// Retrieves the last index of elements within a <see cref="Span{T}"/>.
        /// </summary>
        /// <returns>The index of the last element in the <see cref="Span{T}"/>.</returns>
        public int LastIndex()
        {
            if (span.Length > 0)
                return span.Length - 1;

            return -1;
        }
    }

    /// <param name="span">The <see cref="ReadOnlySpan{T}"/> to search</param>
    /// <typeparam name="T">The type of elements within the <see cref="ReadOnlySpan{T}"/>.</typeparam>
    extension<T>(ReadOnlySpan<T> span)
    {
        /// <summary>
        /// Retrieves the last index of elements within a <see cref="ReadOnlySpan{T}"/>.
        /// </summary>
        /// <returns>The index of the last element in the <see cref="ReadOnlySpan{T}"/>.</returns>
        public int LastIndex()
        {
            if (span.Length > 0)
                return span.Length - 1;

            return -1;
        }
    }

    /// <param name="memory">The memory to search</param>
    /// <typeparam name="T">The type of elements within the memory.</typeparam>
    extension<T>(Memory<T> memory)
    {
        /// <summary>
        /// Retrieves the last index of elements within a memory.
        /// </summary>
        /// <returns>The index of the last element in the memory.</returns>
        public int LastIndex()
        {
            if (memory.Length > 0)
                return memory.Length - 1;

            return -1;
        }
    }
}