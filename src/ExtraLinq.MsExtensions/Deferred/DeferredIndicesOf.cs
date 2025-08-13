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

}