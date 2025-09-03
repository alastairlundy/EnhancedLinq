/*
    EnhancedLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/.
 */

using System;
using System.Collections.Generic;
using System.Linq;

using AlastairLundy.DotExtensions.MsExtensions.StringSegments;

using Microsoft.Extensions.Primitives;

namespace AlastairLundy.EnhancedLinq.MsExtensions.StringSegments.Immediate;

public static class ImmediateSegmentAny
{
    /// <summary>
    /// Returns whether any char in a StringSegment matches the predicate condition.
    /// </summary>
    /// <param name="target">The StringSegment to be searched.</param>
    /// <param name="selector">The predicate func to be invoked on each char in the StringSegment.</param>
    /// <returns>True if any char in the StringSegment matches the predicate; false otherwise.</returns>
    public static bool Any(this StringSegment target, Func<char, bool> selector)
    {
        if(StringSegment.IsNullOrEmpty(target))
            throw new ArgumentNullException(nameof(target));
        
        IEnumerable<bool> groups = (from c in target.ToCharArray()
                group c by predicate(c)
                into g
                where g.Key
                select g.Any());

        bool? result = groups.FirstOrDefault();

        return result ?? false;
    }
}