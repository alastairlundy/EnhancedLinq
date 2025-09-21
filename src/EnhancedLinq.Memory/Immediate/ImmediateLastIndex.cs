/*
    EnhancedLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/.
 */

using System;

namespace AlastairLundy.EnhancedLinq.Memory.Immediate;

public static partial class EnhancedLinqMemoryImmediate
{
    /// <summary>
    /// Retrieves the last index of elements within a <see cref="Span{T}"/>.
    /// </summary>
    /// <param name="span">The <see cref="Span{T}"/> to search</param>
    /// <typeparam name="T">The type of elements within the <see cref="Span{T}"/>.</typeparam>
    /// <returns>The index of the last element in the <see cref="Span{T}"/>.</returns>
    public static int LastIndex<T>(this Span<T> span)
    {
        if (span.Length > 0)
            return span.Length - 1;

        return -1;
    }

    /// <summary>
    /// Retrieves the last index of elements within a <see cref="ReadOnlySpan{T}"/>.
    /// </summary>
    /// <param name="span">The <see cref="ReadOnlySpan{T}"/> to search</param>
    /// <typeparam name="T">The type of elements within the <see cref="ReadOnlySpan{T}"/>.</typeparam>
    /// <returns>The index of the last element in the <see cref="ReadOnlySpan{T}"/>.</returns>
    public static int LastIndex<T>(this ReadOnlySpan<T> span)
    {
        if (span.Length > 0)
            return span.Length - 1;

        return -1;
    }

    /// <summary>
    /// Retrieves the last index of elements within a memory.
    /// </summary>
    /// <param name="memory">The memory to search</param>
    /// <typeparam name="T">The type of elements within the memory.</typeparam>
    /// <returns>The index of the last element in the memory.</returns>
    public static int LastIndex<T>(this Memory<T> memory)
    {
        if (memory.Length > 0)
            return memory.Length - 1;

        return -1;
    }
}