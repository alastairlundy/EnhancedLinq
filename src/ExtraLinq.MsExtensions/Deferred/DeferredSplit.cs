using System.Collections.Generic;

using AlastairLundy.DotExtensions.MsExtensions.System.StringSegments;

using ExtraLinq.MsExtensions.Deferred.Enumerables;

using Microsoft.Extensions.Primitives;

namespace ExtraLinq.MsExtensions.Deferred;

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
    
}