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
using System.Text;

using AlastairLundy.DotExtensions.MsExtensions.StringSegments;
using AlastairLundy.EnhancedLinq.MsExtensions.StringSegments.Deferred;

using Microsoft.Extensions.Primitives;

namespace AlastairLundy.EnhancedLinq.MsExtensions.StringSegments.Immediate;

public static partial class EnhancedLinqSegmentImmediate
{
    
    /// <summary>
    /// Splits a StringSegment into StringSegment subsegments using a specified <see cref="char"/> separator.
    /// </summary>
    /// <param name="source">The source StringSegment.</param>
    /// <param name="separator">The separator to delimit the char in the source StringSegment.</param>
    /// <returns>An array of StringSegment subsegments from the source StringSegment that is delimited by the separator, if the separator character is found.</returns>
    public static StringSegment[] Split(this StringSegment source, char separator)
    {
        if (StringSegment.IsNullOrEmpty(source))
            return [];

        List<StringSegment> segments = new();
        
        StringBuilder current = new StringBuilder();
        
        for (int index = 0; index < source.Length; index++)
        {
            if (source[index] == separator)
            {
                if (current.Length > 0)
                {
                    segments.Add(current.ToString());
                    current.Clear();
                }
            }
            else
            {
                current.Append(source[index]);
            }
        }
        
        return segments.ToArray();
    }

    
    /// <summary>
    /// Splits a StringSegment into StringSegment subsegments using a specified <see cref="StringSegment"/> separator.
    /// </summary>
    /// <param name="source">The source StringSegment.</param>
    /// <param name="separator">The separator to delimit the StringSegment subsegments in the source StringSegment.</param>
    /// <returns>An array of StringSegment subsegments, from the source StringSegment that is delimited by the separator.</returns>
    public static StringSegment[] Split(this StringSegment source, StringSegment separator)
    {
        IEnumerable<int> indices = source.IndicesOf(separator);

        List<StringSegment> output = new();

        int start = 0;

        foreach(int index in indices)
        {
            if (index == -1)
                break;
            
            int end = index > 0 ? index - 1 : 0;

            StringSegment newSegment = source.Subsegment(start, Math.Abs(end - start));

            output.Add(newSegment);
            start = index;
        }
        
        return output.ToArray();
    }
}