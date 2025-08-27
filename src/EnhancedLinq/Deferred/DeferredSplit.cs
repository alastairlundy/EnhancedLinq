/*
    EnhancedLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/.
 */

using System;
using System.Collections.Generic;

using AlastairLundy.EnhancedLinq.Deferred.Enumerables;

namespace AlastairLundy.EnhancedLinq.Deferred;

public static partial class EnhancedLinqDeferred
{
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="maximumCount"></param>
    /// <typeparam name="TSource"></typeparam>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public static IEnumerable<IEnumerable<TSource>> SplitByCount<TSource>(this IEnumerable<TSource> source, int maximumCount)
    {
        ArgumentNullException.ThrowIfNull(source, nameof(source));
        
        if(maximumCount <= 0)
            throw new ArgumentOutOfRangeException(nameof(maximumCount));

        return new SplitByItemCountEnumerable<TSource>(source, maximumItemCount: maximumCount);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <typeparam name="TSource"></typeparam>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IEnumerable<IEnumerable<TSource>> SplitByProcessorCount<TSource>(this IEnumerable<TSource> source)
    {
        ArgumentNullException.ThrowIfNull(source, nameof(source));

        if(source == null)
            throw new ArgumentNullException(nameof(source));
        
        return new SplitByEnumerableCountEnumerable<TSource>(source, Environment.ProcessorCount);
    }
}