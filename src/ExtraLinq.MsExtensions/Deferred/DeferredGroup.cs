using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Primitives;

namespace ExtendedLinq.MsExtensions.Deferred;

public static class DeferredGroup
{
    
    
    public static IGrouping<IEnumerable<char>, TKey> GroupBy<TKey>(this StringSegment target, Func<char, TKey> selector)
    {
        target.IsNullOrEmpty(nameof(target));
        
        
    }
}