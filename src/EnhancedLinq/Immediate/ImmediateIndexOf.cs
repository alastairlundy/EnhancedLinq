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
    /// 
    /// </summary>
    /// <param name="source">The IEnumerable to be searched.</param>
    /// <param name="selector"></param>
    /// <typeparam name="T">The type of object in the IEnumerable.</typeparam>
    /// <returns></returns>
    public static int IndexOf<T>(this IEnumerable<T> source, Func<T, bool> selector)
    {
        ArgumentNullException.ThrowIfNull(source, nameof(source));
        ArgumentNullException.ThrowIfNull(selector, nameof(selector));
     
        int index = 0;

        foreach (T item in source)
        {
            if (selector(item))
                return index;

            index++;
        }

        return -1;
    }
    
    /// <summary>
    /// Returns the index of an object in an IEnumerable.
    /// </summary>
    /// <param name="source">The IEnumerable to be searched.</param>
    /// <param name="obj">The object to get the index of.</param>
    /// <typeparam name="T">The type of object in the IEnumerable.</typeparam>
    /// <returns>The index of an object in an IEnumerable, if the IEnumerable contains the object, returns -1 otherwise.</returns>
    public static int IndexOf<T>(this IEnumerable<T> source, T obj)
    {
        ArgumentNullException.ThrowIfNull(source, nameof(source));

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