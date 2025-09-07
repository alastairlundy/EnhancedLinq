/*
    EnhancedLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/.
 */

using System;
using System.Collections.Generic;
using AlastairLundy.EnhancedLinq.Internals.Localizations;

namespace AlastairLundy.EnhancedLinq.Immediate;

public static partial class EnhancedLinqImmediate
{
    /// <summary>
    /// Determines whether there are at most a maximum number elements in the source sequence.
    /// </summary>
    /// <param name="source">The source sequence to search through.</param>
    /// <param name="countToLookFor">The maximum number of elements that can meet the condition.</param>
    /// <typeparam name="T">The element type of the source sequence.</typeparam>
    /// <returns>True if there are at most <paramref name="countToLookFor"/> number of elements, false otherwise.</returns>
    public static bool CountAtMost<T>(this IEnumerable<T> source, int countToLookFor)
    {
        ArgumentNullException.ThrowIfNull(source);
        
        if (source is ICollection<T> collection)
        {
            return collection.Count <= countToLookFor;
        }
        
        if (countToLookFor < 0)
            throw new ArgumentException(Resources.Exceptions_Count_LessThanZero);
        
        int currentCount = 0;
        
        foreach (T obj in source)
        {
            if(currentCount >= countToLookFor)
                return false;

            currentCount += 1;
        }

        return true;
    }


    /// <summary>
    /// Determines whether there are at most a maximum number elements in the source sequence that satisfy the given condition.
    /// </summary>
    /// <param name="source">The source sequence to search through.</param>
    /// <param name="predicate">The predicate condition to check elements against.</param>
    /// <param name="countToLookFor">The maximum number of elements that can meet the condition.</param>
    /// <typeparam name="T">The element type of the source sequence.</typeparam>
    /// <returns>True if there are at most <paramref name="countToLookFor"/> number of elements that satisfy the condition, false otherwise.</returns>
    public static bool CountAtMost<T>(this IEnumerable<T> source, Func<T, bool> predicate,
        int countToLookFor)
    {
        ArgumentNullException.ThrowIfNull(source);

        if (countToLookFor < 0)
            throw new ArgumentException(Resources.Exceptions_Count_LessThanZero.Replace("{x}", countToLookFor.ToString()));
        
        int currentCount = 0;

        foreach (T obj in source)
        {
            if (predicate(obj))
                currentCount += 1;
            
            if(currentCount >= countToLookFor)
                return false;
        }

        return true;
    }
}