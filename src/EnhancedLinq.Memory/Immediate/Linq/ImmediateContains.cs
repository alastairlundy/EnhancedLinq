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
    /// Determines whether a <see cref="Span{T}"/> contains a specified item.
    /// </summary>
    /// <param name="source">The  <see cref="Span{T}"/> to be searched.</param>
    /// <param name="item">The item to search for in the  <see cref="Span{T}"/></param>
    /// <typeparam name="T">The type of element in the  <see cref="Span{T}"/>.</typeparam>
    /// <returns>True if the item was found, false otherwise.</returns>
    public static bool Contains<T>(this Span<T> source, T item)
    {
        if (source.IsEmpty)
            return false;
        
        for (int i = 0; i < source.Length; i++)
        {
            T obj = source[i];
            
            if (obj is not null && obj.Equals(item))
            {
                return true;
            }
        }
        
        return false;
    }   
    
    /// <summary>
    /// Determines whether a <see cref="ReadOnlySpan{T}"/> contains a specified item.
    /// </summary>
    /// <param name="source">The <see cref="ReadOnlySpan{T}"/> to be searched.</param>
    /// <param name="item">The item to search for in the <see cref="ReadOnlySpan{T}"/>.</param>
    /// <typeparam name="T">The type of element in the <see cref="ReadOnlySpan{T}"/>.</typeparam>
    /// <returns>True if the item was found, false otherwise.</returns>
    public static bool Contains<T>(this ReadOnlySpan<T> source, T item)
    {
        if (source.IsEmpty)
            return false;
        
        for (int i = 0; i < source.Length; i++)
        {
            T obj = source[i];
            
            if (obj is not null && obj.Equals(item))
            {
                return true;
            }
        }
        
        return false;
    }   
}