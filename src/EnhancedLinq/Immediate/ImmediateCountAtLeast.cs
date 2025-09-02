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
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="countToLookFor"></param>
    /// <typeparam name="TNumber"></typeparam>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
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
            {
                return true;
            }
            
            currentCount += TNumber.One;
        }

        return false;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="selector"></param>
    /// <param name="countToLookFor"></param>
    /// <typeparam name="TNumber"></typeparam>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static bool CountAtLeast<TNumber, T>(this IEnumerable<T> source, Func<T, bool> selector,
        TNumber countToLookFor)
        where TNumber : INumber<TNumber>
    {
        ArgumentNullException.ThrowIfNull(source);
        
        TNumber currentCount = TNumber.Zero;

        foreach (T obj in source)
        {
            if(currentCount >= countToLookFor)
                return true;
            
            if(selector(obj))
                currentCount += TNumber.One;
        }

        return false;
    }
}