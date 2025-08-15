/*
    ExtraLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/.
 */

using System;
using System.Collections.Generic;
using System.Linq;

using ExtraLinq.MsExtensions.Deferred.Enumerables;

using Microsoft.Extensions.Primitives;

namespace ExtraLinq.MsExtensions.Deferred;

public static class DeferredGroup
{
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="target"></param>
    /// <param name="selector"></param>
    /// <typeparam name="TKey"></typeparam>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IEnumerable<IGrouping<TKey, char>> GroupBy<TKey>(this StringSegment target, 
        Func<char, TKey> selector)
    {
        if(StringSegment.IsNullOrEmpty(target))
            throw new ArgumentNullException(nameof(target));
        
        return new GroupStringSegmentEnumerable<TKey>(target, selector);
    }
}