/*
    EnhancedLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/.
 */

using System;

namespace AlastairLundy.EnhancedLinq.Memory.Immediate;

public static class ImmediateLastIndex
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="span"></param>
    /// <typeparam name="T">The type of elements within the span.</typeparam>
    /// <returns></returns>
    public static int LastIndex<T>(this Span<T> span)
    {
        if(span.Length > 0)
            return span.Length - 1;

        return -1;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="span"></param>
    /// <typeparam name="T">The type of elements within the span.</typeparam>
    /// <returns></returns>
    public static int LastIndex<T>(this ReadOnlySpan<T> span)
    {
        if(span.Length > 0)
            return span.Length - 1;

        return -1;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="memory"></param>
    /// <typeparam name="T">The type of elements within the memory.</typeparam>
    /// <returns></returns>
    public static int LastIndex<T>(this Memory<T> memory)
    {
        if(memory.Length > 0)
            return memory.Length - 1;

        return -1;
    }
}