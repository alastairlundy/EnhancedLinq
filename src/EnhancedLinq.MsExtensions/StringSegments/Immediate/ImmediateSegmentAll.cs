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

using AlastairLundy.EnhancedLinq.MsExtensions.StringSegments.Deferred;

using Microsoft.Extensions.Primitives;

namespace AlastairLundy.EnhancedLinq.MsExtensions.StringSegments.Immediate;

public static partial class EnhancedLinqImmediateSegment
{
    /// <summary>
    /// Returns whether all chars in a StringSegment match the predicate condition.
    /// </summary>
    /// <param name="target">The StringSegment to be searched.</param>
    /// <param name="selector">The predicate func to be invoked on each item in the StringSegment.</param>
    /// <returns>True if all chars in the StringSegment match the predicate; false otherwise.</returns>
    public static bool All(this StringSegment target, Func<char, bool> selector)
    {
        if(StringSegment.IsNullOrEmpty(target))
            throw new ArgumentNullException(nameof(target));
        
        IEnumerable<bool> groups = target.GroupBy(selector)
            .Select(g => g.Any());
      
        return groups.Distinct().Count() == 1;
    }
}