/*
    EnhancedLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/.
 */

using System;
using System.Collections.Generic;
using EnhancedLinq.MsExtensions.Internals.Localizations;
using EnhancedLinq.MsExtensions.Deferred;

using Microsoft.Extensions.Primitives;

namespace EnhancedLinq.MsExtensions.Immediate.StringSegments;

public static class ImmediateSegmentFirstPredicate
{

    /// <summary>
    /// Returns the first char in the StringSegment that matches the predicate condition.
    /// </summary>
    /// <param name="target">The StringSegment to be searched.</param>
    /// <param name="predicate">The predicate func condition to be checked against each char in the StringSegment.</param>
    /// <returns>The first char in the StringSegment that matches the predicate condition.</returns>
    /// <exception cref="ArgumentException">Thrown if no characters in the StringSegment meet the predicate condition.</exception>
    public static char First(this StringSegment target, Func<char, bool> predicate)
    {
        IEnumerable<char> results = (from c in target
            where predicate(c)
            select c);

        foreach (char result in results)
        {
            return result;
        }
        
        throw new ArgumentException(Resources.Exceptions_Segments_InvalidOperation_EmptySequence);
    }

    /// <summary>
    /// Returns the first character of the specified <see cref="StringSegment"/> that meets the predicate condition or null if the segment is empty.
    /// </summary>
    /// <param name="target">The StringSegment to be searched.</param>
    /// <param name="predicate">The predicate func condition to be checked against each char in the StringSegment.</param>
    /// <returns>The first character of the segment that meets the predicate condition if any match; otherwise, null.</returns>
    public static char? FirstOrDefault(this StringSegment target, Func<char, bool> predicate)
    {
        IEnumerable<char> results = (from c in target
            where predicate(c)
            select c);

        foreach (char result in results)
        {
            return result;
        }
        
        return null;
    }
}