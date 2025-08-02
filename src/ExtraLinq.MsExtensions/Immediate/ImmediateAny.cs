using System;
using System.Collections.Generic;
using System.Linq;
using ExtendedLinq.MsExtensions.Deferred;
using Microsoft.Extensions.Primitives;

namespace ExtendedLinq.MsExtensions.Immediate;

public static class ImmediateAny
{
    /// <summary>
    /// Returns whether any char in a StringSegment matches the predicate condition.
    /// </summary>
    /// <param name="target">The StringSegment to be searched.</param>
    /// <param name="predicate">The predicate func to be invoked on each char in the StringSegment.</param>
    /// <returns>True if any char in the StringSegment matches the predicate; false otherwise.</returns>
    public static bool Any(this StringSegment target, Func<char, bool> predicate)
    {
        IEnumerable<bool> groups = (from c in target
                group c by predicate(c)
                into g
                where g.Key
                select g.Any()
            );

        bool? result = groups.FirstOrDefault();

        return result ?? false;
    }
}