/*
    EnhancedLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/.
 */

using System;
using System.Collections.Generic;
using AlastairLundy.DotPrimitives.Collections.Enumerables;
using AlastairLundy.EnhancedLinq.MsExtensions.StringSegments.Deferred.Enumerables;
using AlastairLundy.EnhancedLinq.MsExtensions.StringSegments.Deferred.Enumerators;
using Microsoft.Extensions.Primitives;

namespace AlastairLundy.EnhancedLinq.MsExtensions.StringSegments.Deferred;

/// <summary>
/// 
/// </summary>
public static partial class EnhancedLinqSegmentDeferred
{
    /// <summary>
    /// Returns an IEnumerable of chars that match the predicate. 
    /// </summary>
    /// <param name="target">The StringSegment to search.</param>
    /// <param name="predicate">The predicate to check each char against.</param>
    /// <returns>An IEnumerable of chars that matches the predicate.</returns>
    public static IEnumerable<char> Where(this StringSegment target, Func<char, bool> predicate)
    {
        if(StringSegment.IsNullOrEmpty(target)) 
            throw new ArgumentNullException(nameof(target));

        return new CustomEnumeratorEnumerable<char>(new WhereSegmentEnumerator(target, predicate));
    }
}