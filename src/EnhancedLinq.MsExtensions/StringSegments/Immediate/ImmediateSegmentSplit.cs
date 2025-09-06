/*
    EnhancedLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/.
 */

using System;
using System.Linq;
using AlastairLundy.DotExtensions.MsExtensions.StringSegments;
using AlastairLundy.EnhancedLinq.MsExtensions.StringSegments.Deferred;
using Microsoft.Extensions.Primitives;

namespace AlastairLundy.EnhancedLinq.MsExtensions.StringSegments.Immediate;

public static partial class EnhancedLinqSegmentImmediate
{
    
    /// <summary>
    /// Splits a StringSegment into StringSegment subsegments using a specified <see cref="char"/> separator.
    /// </summary>
    {
        if (segment.Contains(separator) == false)
            return [];
        
        int[] indices = segment.IndicesOf(separator).ToArray();
        
        StringSegment[] output = new StringSegment[indices.Length];
        
        if (indices.First().Equals(-1))
            return [segment];
        IEnumerable<int> indices = source.IndicesOf(separator);

        List<StringSegment> output = new();

        int start = 0;

        foreach(int index in indices)
        {
                int end = i > 0 ? i - 1 : 0;
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