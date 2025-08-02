using System;
using System.Collections.Generic;
using System.Linq;
using ExtraLinq.MsExtensions.Deferred.Enumerables;
using Microsoft.Extensions.Primitives;

namespace ExtraLinq.MsExtensions.Deferred;

public static class DeferredGroup
{
    
    
    public static IGrouping<TKey, char> GroupBy<TKey>(this StringSegment target, 
        Func<char, TKey> selector)
    {
        StringSegment.IsNullOrEmpty(nameof(target));
        
        
        
        return new GroupStringSegmentEnumerable<TKey>(target, selector);
    }
}