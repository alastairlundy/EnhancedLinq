/*
    EnhancedLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/.
 */

using System;
using AlastairLundy.DotExtensions.Memory.Spans;

namespace AlastairLundy.EnhancedLinq.Memory.Immediate;

public static partial class EnhancedLinqMemoryImmediate
{

    /// <summary>
    /// Returns the element at the specified index in the source <see cref="Memory{T}"/>.
    /// </summary>
    /// <param name="source">The source <see cref="Memory{T}"/> .</param>
    /// <param name="index">The zero-based index of the element to be retrieved.</param>
    /// <typeparam name="T">The type of items stored in the Memory.</typeparam>
    /// <returns>A new source <see cref="Memory{T}"/> containing a single element starting at the specified index in the Memory.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the source <see cref="Memory{T}"/> has no elements or the index is out of range.</exception>
    public static T ElementAt<T>(this Memory<T> source, int index)
    {
        if (source.Length == 0)
            throw new ArgumentOutOfRangeException(nameof(index));

        Memory<T> items = ElementsAt(source, index, 1);

        return First(items.Span);
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