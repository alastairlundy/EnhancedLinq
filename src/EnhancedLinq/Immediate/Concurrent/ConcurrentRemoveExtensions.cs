/*
       EnhancedLinq 
       Copyright (c) 2025-2026 Alastair Lundy
       
       This Source Code Form is subject to the terms of the Mozilla Public
       License, v. 2.0. If a copy of the MPL was not distributed with this
       file, You can obtain one at https://mozilla.org/MPL/2.0/. 
   */

using System.Collections.Concurrent;
using System.Linq;

namespace EnhancedLinq.Immediate.Concurrent;

/// <summary>
/// Provides a set of static methods for immediate, concurrent processing and manipulation of collections
/// within the System.Collections.Concurrent namespace.
/// These methods are designed to enhance LINQ operability for concurrent collections.
/// </summary>
public static partial class EnhancedLinqImmediateConcurrent
{
    /// <param name="concurrentBag">The concurrent bag to remove objects from.</param>
    /// <typeparam name="T">The type of elements contained within the concurrent bag.</typeparam>
    extension<T>(ConcurrentBag<T> concurrentBag)
    where T : notnull
    {
        /// <summary>
        /// Removes all occurrences of a specified object from a concurrent bag.
        /// </summary>
        /// <param name="obj">The object to be removed from the concurrent bag.</param>
        /// <returns>A new concurrent bag with all occurrences of the specified object removed.</returns>
        public ConcurrentBag<T> Remove(T obj)
        {
            ArgumentNullException.ThrowIfNull(concurrentBag);
            
            IEnumerable<T> newCollection = from item in concurrentBag
                where !item.Equals(obj)
                select item;
        
            return new ConcurrentBag<T>(newCollection);
        }
    }
}