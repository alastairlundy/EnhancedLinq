using System;
using System.Collections.Generic;

using ExtraLinq.MsExtensions.Deferred.Enumerables;

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
        if(StringSegment.IsNullOrEmpty(target)) 
            throw new ArgumentNullException(nameof(target));

        return new WhereSegmentEnumerable(target, selector);
    }
}