/*
    EnhancedLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/.
 */

using System;
using System.Collections.Generic;

namespace AlastairLundy.EnhancedLinq.Immediate;

public static partial class EnhancedLinqImmediate
{
    
    /// <summary>
    /// Gets the first index of the first element that matches the predicate condition.
    /// </summary>
    /// <param name="source">The <see cref="IEnumerable{T}"/> to be searched.</param>
    /// <param name="predicate">The predicate condition to check elements of the sequence against.</param>
    /// <typeparam name="T">The type of elements in the sequence.</typeparam>
    /// <returns>The first index of the first element in the sequence to match the predicate condition,
    /// if the sequence contains any elements that match the predicate condition, returns -1 otherwise.
    /// </returns>
    public static int IndexOf<T>(this IEnumerable<T> source, Func<T, bool> predicate)
    {
#if NET8_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(source, nameof(source));
        ArgumentNullException.ThrowIfNull(predicate, nameof(predicate));
     #endif
        
        int index = 0;

        foreach (T item in source)
        {
            if (predicate(item))
                return index;

            index++;
        }

        return -1;
    }
    
    /// <summary>
    /// Gets the first index of an element in a sequence.
    /// </summary>
    /// <param name="source">The sequence to be searched.</param>
    /// <param name="obj">The element to get the index of.</param>
    /// <typeparam name="T">The type of elements in the sequence.</typeparam>
    /// <returns>The first index of an element in a sequence, if the sequence contains the element, returns -1 otherwise.</returns>
    public static int IndexOf<T>(this IEnumerable<T> source, T obj)
    {
#if NET8_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(source, nameof(source));
#endif
        
        if (source is IList<T> list)
        {
            return list.IndexOf(obj);
        }
        
        int index = 0;
                
        foreach (T item in source)
        {
            if (item is not null && item.Equals(obj))
            {
                return index;
            }
                    
            index++;
        }
        
        return -1;
    }
}