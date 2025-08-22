/*
    EnhancedLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/.
 */

using System.Collections.Generic;
using System.Linq;

using AlastairLundy.DotExtensions.MsExtensions.StringSegments;

using EnhancedLinq.MsExtensions.StringSegments.Deferred;

using Microsoft.Extensions.Primitives;

namespace EnhancedLinq.MsExtensions.StringSegments.Immediate;

public static class ImmediateIndexOf
{
    
    /// <summary>
    /// Finds the index of a specified StringSegment within another StringSegment.
    /// </summary>
    /// <param name="this">The StringSegment to be searched.</param>
    /// <param name="segment">The StringSegment to search for.</param>
    /// <returns>The index at which the specified StringSegment can be found, or -1 if not found.</returns>
    public static int IndexOf(this StringSegment @this, StringSegment segment)
    {
        if (@this.Length < segment.Length || segment.Length == 0)
            return -1;
        
        IEnumerable<int> indexes = @this.IndicesOf(segment.First())
            .Where(x  => x != -1);

        foreach (int index in indexes)
        {
            StringSegment indexSegment = @this.Subsegment(index, segment.Length);

            if (indexSegment.Equals(segment))
            {
                return index;
            }
        }

        return -1;
    }
    
    /// <summary>
    /// Finds the index of a specified StringSegment within a string.
    /// </summary>
    /// <param name="str">The string to be searched.</param>
    /// <param name="segment">The StringSegment to search for.</param>
    /// <returns>The index at which the specified StringSegment can be found, or -1 if not found.</returns>
    public static int IndexOf(this string str, StringSegment segment)
    {
        if (str.Length < segment.Length || segment.Length == 0)
            return -1;

        int index = 0;
        
        for (int i = 0; i < str.Length; i++)
        {
            if (str[i] == segment[0])
            {
                StringSegment indexSegment = str.Substring(i, segment.Length);

                if (indexSegment.Equals(segment))
                {
                    return index;
                }
            }
        }

        return -1;
    }
}