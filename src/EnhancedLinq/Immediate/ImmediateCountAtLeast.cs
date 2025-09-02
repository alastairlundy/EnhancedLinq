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
    /// Determines whether there are at least a specified number of elements in the sequence.
    /// </summary>
    /// <param name="source">The source sequence.</param>
    /// <param name="countToLookFor">The minimum count to look for.</param>
    /// <typeparam name="TNumber">The type of numeric value used for counting.</typeparam>
    /// <typeparam name="T">The element type in the source sequence.</typeparam>
    /// <returns><c>true</c> if there are at least the specified number of elements in the sequence; otherwise, <c>false</c>.</returns>
    public static bool CountAtLeast<TNumber, T>(this IEnumerable<T> source, TNumber countToLookFor)
        where TNumber : INumber<TNumber>
    {
        if (source is ICollection<T> collection)
        {
            return collection.Count.ToNumber<TNumber>() >= countToLookFor;
        }
        
        ArgumentNullException.ThrowIfNull(source);
        
        TNumber currentCount = TNumber.Zero;

        foreach (T obj in source)
        {
            if(currentCount >= countToLookFor)
                return true;
            
            currentCount += TNumber.One;
        }

        return false;
    }

    /// <summary>
    /// Determines whether there are at least a specified number of elements in the sequence that meet a given condition.
    /// </summary>
    /// <param name="source">The source sequence.</param>
    /// <param name="selector">The predicate condition to check elements against.</param>
    /// <param name="countToLookFor">The minimum count to look for.</param>
    /// <typeparam name="TNumber">The type of numeric value used for counting.</typeparam>
    /// <typeparam name="T">The element type in the source sequence.</typeparam>
    /// <returns><c>true</c> if there are at least the specified number of elements that meet the condition; otherwise, <c>false</c>.</returns>
    public static bool CountAtLeast<TNumber, T>(this IEnumerable<T> source, Func<T, bool> selector,
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
                return true;
        }

        return false;
    }
}