/*
    EnhancedLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/.
 */

using System;
using System.Collections.Generic;

using AlastairLundy.DotExtensions.MsExtensions.StringSegments;
using EnhancedLinq.MsExtensions.Deferred.Enumerables;
using Microsoft.Extensions.Primitives;

namespace EnhancedLinq.MsExtensions.Deferred;

public static class DeferredSplit
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="separator"></param>
    /// <returns></returns>
    public static IEnumerable<StringSegment> Split(this StringSegment source, char separator)
    {
        if (source.Contains(separator) == false)
            return [];
        
        return new SegmentSplitCharEnumerable(source, separator);
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="separator"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static IEnumerable<StringSegment> Split(this StringSegment source, StringSegment separator)
    {
        if (StringSegment.IsNullOrEmpty(separator) || StringSegment.IsNullOrEmpty(source))
            throw new ArgumentException();
            
        if (source.Contains(separator) == false)
            return [];
        
        return new SegmentSplitEnumerable(source, separator);
    }
}