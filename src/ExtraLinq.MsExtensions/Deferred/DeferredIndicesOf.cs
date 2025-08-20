/*
    ExtraLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/.
 */

using System;
using System.Collections.Generic;
using ExtraLinq.MsExtensions.Deferred.Enumerables;
using Microsoft.Extensions.Primitives;

namespace ExtraLinq.MsExtensions.Deferred;

public static class DeferredIndicesOf
{
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="c"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static IEnumerable<int> IndicesOf(this StringSegment source, char c)
    {
        if (StringSegment.IsNullOrEmpty(source))
            throw new ArgumentException();

        return new SegmentIndicesEnumerable(source, c);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="segment"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static IEnumerable<int> IndicesOf(this StringSegment source, StringSegment segment)
    {
        if (StringSegment.IsNullOrEmpty(source))
            throw new ArgumentException();

        return new SegmentIndicesEnumerable(source, segment);
    }
}