/*
    EnhancedLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/.
 */

using System;

using AlastairLundy.EnhancedLinq.MsExtensions.Internals.Localizations;

using Microsoft.Extensions.Primitives;

namespace AlastairLundy.EnhancedLinq.MsExtensions.StringSegments.Immediate;

public static partial class EnhancedLinqSegmentImmediate
{
    /// <summary>
    /// Determines whether there are at most a maximum number elements in the source <see cref="StringSegment"/>.
    /// </summary>
    /// <param name="source">The source <see cref="StringSegment"/> to search through.</param>
    /// <param name="countToLookFor">The maximum number of elements that can meet the condition.</param>
    /// <returns>True if there are at most <paramref name="countToLookFor"/> number of elements, false otherwise.</returns>
    public static bool CountAtMost<TNumber>(this StringSegment source, int countToLookFor)
    {
        if(StringSegment.IsNullOrEmpty(source))
            throw new InvalidOperationException(Resources.Exceptions_Segments_InvalidOperation_EmptySequence);

        if (countToLookFor < 0)
            throw new ArgumentException(Resources.Exceptions_Count_LessThanZero.Replace("{x}", countToLookFor.ToString()));
        
        return source.Length <= countToLookFor;
    }


    /// <summary>
    /// Determines whether there are at most a maximum number elements in the source <see cref="StringSegment"/> that satisfy the given condition.
    /// </summary>
    /// <param name="source">The source <see cref="StringSegment"/> to search through.</param>
    /// <param name="predicate">The predicate condition to check elements against.</param>
    /// <param name="countToLookFor">The maximum number of elements that can meet the condition.</param>
    /// <returns>True if there are at most <paramref name="countToLookFor"/> number of elements that satisfy the condition, false otherwise.</returns>
    public static bool CountAtMost(this StringSegment source, Func<char, bool> predicate,
        int countToLookFor)
    {
        if(StringSegment.IsNullOrEmpty(source))
            throw new InvalidOperationException(Resources.Exceptions_Segments_InvalidOperation_EmptySequence);

        if (countToLookFor < 0)
            throw new ArgumentException(Resources.Exceptions_Count_LessThanZero.Replace("{x}", countToLookFor.ToString()));

        int currentCount = 0;

        for (int index = 0; index < source.Length; index++ )
        {
            char c = source[index];
            
            if (predicate(c))
                currentCount += 1;
            
            if(currentCount >= countToLookFor)
                return false;
        }

        return true;
    }
}