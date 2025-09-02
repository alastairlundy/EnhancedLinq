/*
    EnhancedLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/.
 */

using System;
using System.Collections.Generic;
using System.Numerics;

using AlastairLundy.DotExtensions.Numbers;

namespace AlastairLundy.EnhancedLinq.Immediate;

public static partial class EnhancedLinqImmediate
{
    /// <summary>
    /// Determines whether there are at most a maximum number elements in the source sequence.
    /// </summary>
    /// <param name="source">The source sequence to search through.</param>
    /// <param name="countToLookFor">The maximum number of elements that can meet the condition.</param>
    /// <typeparam name="TNumber">The numeric type for counting the elements in the sequence.</typeparam>
    /// <typeparam name="T">The element type of the source sequence.</typeparam>
    /// <returns>True if there are at most <paramref name="countToLookFor"/> number of elements, false otherwise.</returns>
    public static bool CountAtMost<TNumber, T>(this IEnumerable<T> source, TNumber countToLookFor)
        where TNumber : INumber<TNumber>
    {
        if (source is ICollection<T> collection)
        {
            return collection.Count.ToNumber<TNumber>() <= countToLookFor;
        }
        
        ArgumentNullException.ThrowIfNull(source);
        
        TNumber currentCount = TNumber.Zero;
        
        foreach (T obj in source)
        {
            if(currentCount >= countToLookFor)
                return false;
            
            currentCount += TNumber.One;
        }

        return true;
    }


    /// <summary>
    /// Determines whether there are at most a maximum number elements in the source sequence that satisfy the given condition.
    /// </summary>
    /// <param name="source">The source sequence to search through.</param>
    /// <param name="selector">The predicate condition to check elements against.</param>
    /// <param name="countToLookFor">The maximum number of elements that can meet the condition.</param>
    /// <typeparam name="TNumber">The numeric type for counting the elements in the sequence.</typeparam>
    /// <typeparam name="T">The element type of the source sequence.</typeparam>
    /// <returns>True if there are at most <paramref name="countToLookFor"/> number of elements that satisfy the condition, false otherwise.</returns>
    public static bool CountAtMost<TNumber, T>(this IEnumerable<T> source, Func<T, bool> selector,
        TNumber countToLookFor)
        where TNumber : INumber<TNumber>
    {
        ArgumentNullException.ThrowIfNull(source);

        TNumber currentCount = TNumber.Zero;

        foreach (T obj in source)
        {
            if(selector(obj))
                currentCount += TNumber.One;
            
            if(currentCount >= countToLookFor)
                return false;
        }

        return true;
    }
}