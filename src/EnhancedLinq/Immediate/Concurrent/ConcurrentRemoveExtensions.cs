﻿/*
        EnhancedLinq 
        Copyright (c) 2025 Alastair Lundy
        
       Licensed under the Apache License, Version 2.0 (the "License");
       you may not use this file except in compliance with the License.
       You may obtain a copy of the License at

           http://www.apache.org/licenses/LICENSE-2.0

       Unless required by applicable law or agreed to in writing, software
       distributed under the License is distributed on an "AS IS" BASIS,
       WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
       See the License for the specific language governing permissions and
       limitations under the License.
   */

using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace AlastairLundy.EnhancedLinq.Immediate.Concurrent;

/// <summary>
/// Provides a set of static methods for immediate, concurrent processing and manipulation of collections
/// within the System.Collections.Concurrent namespace.
/// These methods are designed to enhance LINQ operability for concurrent collections.
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
            // ReSharper disable once RedundantBoolCompare
            where item.Equals(obj) == false
            select item;
        
        return new ConcurrentBag<T>(newCollection);
    }
}