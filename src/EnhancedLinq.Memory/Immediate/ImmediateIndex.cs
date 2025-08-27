/*
    EnhancedLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/.
 */

namespace AlastairLundy.EnhancedLinq.Memory.Immediate;

public static partial class EnhancedLinqMemoryImmediate
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="span"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static ICollection<int> Index<T>(this Span<T> span)
        => Index(span, 0);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="span"></param>
    /// <param name="startIndex"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public static ICollection<int> Index<T>(this Span<T> span, int startIndex)
    {
        if(startIndex < 0 || startIndex >= span.Length)
            throw new ArgumentOutOfRangeException(nameof(startIndex));
        
        List<int> output = new();
        
        for (int i = 0; i < span.Length; i++)
        {
            if(i >= startIndex)
                output.Add(i);
        }
        
        return output;
    }

}