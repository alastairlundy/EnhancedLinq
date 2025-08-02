using System;
using System.Collections.Generic;
using Microsoft.Extensions.Primitives;

namespace ExtraLinq.MsExtensions.Deferred;

public static partial class MsExtensionsDeferred
{
    /// <summary>
    /// Returns an IEnumerable of chars that match the predicate. 
    /// </summary>
    /// <param name="target">The StringSegment to search.</param>
    /// <param name="selector">The predicate to check each char against.</param>
    /// <returns>An IEnumerable of chars that matches the predicate.</returns>
    public static IEnumerable<char> Where(this StringSegment target, Func<char, bool> selector)
    {
        for (int i = 0; i < target.Length; i++)
        {
            if(selector(target[i]))
                yield return target[i];
        }
    }
    
}