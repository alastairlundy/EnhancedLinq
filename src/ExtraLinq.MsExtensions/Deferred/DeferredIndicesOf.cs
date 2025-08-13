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
    /// <param name="str"></param>
    /// <param name="c"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static IEnumerable<int> IndicesOf(this StringSegment str, char c)
    {
        if (StringSegment.IsNullOrEmpty(str))
            throw new ArgumentException();

        return new SegmentIndicesEnumerable(str, c);
    }

}