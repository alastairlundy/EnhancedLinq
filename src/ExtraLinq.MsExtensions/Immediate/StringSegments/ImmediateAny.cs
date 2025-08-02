using System;
using System.Collections.Generic;
using System.Linq;
using AlastairLundy.DotExtensions.MsExtensions.System.StringSegments;
using ExtraLinq.MsExtensions.Deferred;
using Microsoft.Extensions.Primitives;

namespace ExtraLinq.MsExtensions.Immediate.StringSegments;

public static class ImmediateSegmentAny
{
    /// <summary>
    /// Returns whether any char in a StringSegment matches the predicate condition.
    /// </summary>
    /// <param name="target">The StringSegment to be searched.</param>
    /// <param name="predicate">The predicate func to be invoked on each char in the StringSegment.</param>
    /// <returns>True if any char in the StringSegment matches the predicate; false otherwise.</returns>
    public static bool Any(this StringSegment target, Func<char, bool> predicate)
    {
        if(StringSegment.IsNullOrEmpty(target))
            throw new ArgumentNullException(nameof(target));
        
        IEnumerable<bool> groups = (from c in target.ToCharArray()
                group c by predicate(c)
                into g
                where g.Key
                select g.Any()
            );

        bool? result = groups.FirstOrDefault();

        return result ?? false;
    }
}