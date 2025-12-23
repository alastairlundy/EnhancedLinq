/*
    EnhancedLinq.Memory
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
    */

namespace EnhancedLinq.Memory.Immediate;

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
            InvalidOperationException.ThrowIfSpanIsEmpty(span);

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
            InvalidOperationException.ThrowIfSpanIsEmpty(span);

            if (span.Length > 0)
                return span.Length - 1;

            return -1;
        }
    }

    /// <param name="memory">The <see cref="Memory{T}"/> to search</param>
    /// <typeparam name="T">The type of elements within the <see cref="Memory{T}"/>.</typeparam>
    extension<T>(Memory<T> memory)
    {
        /// <summary>
        /// Retrieves the last index of elements within a memory.
        /// </summary>
        /// <returns>The index of the last element in the memory.</returns>
        public int LastIndex()
        {
            InvalidOperationException.ThrowIfMemoryIsEmpty(memory);

            if (memory.Length > 0)
                return memory.Length - 1;

            return -1;
        }
    }

    /// <param name="memory">The <see cref="ReadOnlyMemory{T}"/> to search</param>
    /// <typeparam name="T">The type of elements within the <see cref="ReadOnlyMemory{T}"/>.</typeparam>
    extension<T>(ReadOnlyMemory<T> memory)
    {
        /// <summary>
        /// Retrieves the last index of elements within a <see cref="ReadOnlyMemory{T}"/>.
        /// </summary>
        /// <returns>The index of the last element in the <see cref="ReadOnlyMemory{T}"/>.</returns>
        public int LastIndex()
        {
            InvalidOperationException.ThrowIfMemoryIsEmpty(memory);

            if (memory.Length > 0)
                return memory.Length - 1;

            return -1;
        }
    }
}