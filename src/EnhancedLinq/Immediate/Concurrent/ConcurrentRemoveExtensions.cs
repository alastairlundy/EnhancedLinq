/*
    EnhancedLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/.
 */

using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace AlastairLundy.EnhancedLinq.Immediate.Concurrent;

/// <summary>
/// 
/// </summary>
public static partial class EnhancedLinqImmediateConcurrent
{
    
    /// <summary>
    /// Removes all occurrences of a specified object from a concurrent bag.
    /// </summary>
    /// <param name="concurrentBag">The concurrent bag to remove objects from.</param>
    /// <param name="obj">The object to be removed from the concurrent bag.</param>
    /// <typeparam name="T">The type of elements contained within the concurrent bag.</typeparam>
    /// <returns>A new concurrent bag with all occurrences of the specified object removed.</returns>
    public static ConcurrentBag<T> Remove<T>(this ConcurrentBag<T> concurrentBag, T obj)
    {
        IEnumerable<T> newCollection = from item in concurrentBag
            where item.Equals(obj) == false
            select item;
        
        return new ConcurrentBag<T>(newCollection);
    }
}