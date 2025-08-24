/*
    EnhancedLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/.
 */

using System;
using System.Collections.Generic;
using System.Linq;

namespace AlastairLundy.EnhancedLinq.Immediate;

public static partial class EnhancedLinqImmediate
{
    
    /// <summary>
    /// Gets the first index of the last element that matches the predicate condition.
    /// </summary>
    /// <param name="source">The <see cref="IEnumerable{T}"/> to be searched.</param>
    /// <param name="selector">The predicate condition to check elements of the sequence against.</param>
    /// <typeparam name="T">The type of elements in the IEnumerable.</typeparam>
    /// <returns>The first index of the last element in the sequence to match the predicate condition,
    /// if the sequence contains any elements that match the predicate condition, returns -1 otherwise.
    /// </returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static int LastIndexOf<T>(this IEnumerable<T> source, Func<T, bool> selector)
    {
        ArgumentNullException.ThrowIfNull(source, nameof(source));

        IList<T> list = source as IList<T> ?? source.ToArray();
        
        if(list is null)
            throw new  ArgumentNullException(nameof(source));

        int index = list.Count - 1;
        
        foreach (T item in list.Reverse())
        {
            if(selector(item))
                return index;

            index--;
        }

        return -1;
    }

    /// <summary>
    /// Gets the last index of an element in a sequence.
    /// </summary>
    /// <param name="source">The sequence to be searched.</param>
    /// <param name="obj">The element to get the index of.</param>
    /// <typeparam name="T">The type of elements in the sequence.</typeparam>
    /// <returns>The last index of an element in a sequence, if the sequence contains the element, returns -1 otherwise.</returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static int LastIndexOf<T>(this IEnumerable<T> source, T obj)
    {
        ArgumentNullException.ThrowIfNull(source, nameof(source));

        IList<T> list = source as IList<T> ?? source.ToArray();
        
        if(list is null)
            throw new  ArgumentNullException(nameof(source));
        
        int index = list.Count -1;
                
        foreach (T item in list.Reverse())
        {
            if (item is not null && item.Equals(obj))
                return index;
                    
            index--;
        }
        
        return -1;
    }
}