/*
    EnhancedLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/.
 */

using System;
using Microsoft.Extensions.Primitives;

namespace AlastairLundy.EnhancedLinq.MsExtensions.StringSegments.Immediate;

public static partial class EnhancedLinqSegmentImmediate
{
    /// <summary>
    /// Counts the number of chars in the StringSegment that match the predicate.
    /// </summary>
    /// <param name="target">The StringSegment to search.</param>
    /// <param name="predicate">The predicate to check each char against.</param>
    /// <returns>The number of chars matching the predicate condition as an integer.</returns>
    public static int Count(this StringSegment target,  Func<char, bool> predicate)
    {
        int output = 0;

        for (int i =  0; i < target.Length; i++)
        {
            if (predicate(target[i])) 
                output++;
        }
            
        return output;
    }
}