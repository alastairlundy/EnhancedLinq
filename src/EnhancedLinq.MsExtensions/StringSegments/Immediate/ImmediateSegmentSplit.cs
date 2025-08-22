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

using EnhancedLinq.MsExtensions.StringSegments.Deferred;

using Microsoft.Extensions.Primitives;

namespace EnhancedLinq.MsExtensions.StringSegments.Immediate;

public static class SegmentSplitExtensions
{
    
    /// <summary>
    /// Splits a StringSegment into StringSegment substrings using a specified separator.
    /// </summary>
    /// <param name="segment">The input StringSegment.</param>
    /// <param name="separator">The separator to delimit the StringSegment substrings in the StringSegment.</param>
    /// <returns>An array of StringSegment substrings, from this StringSegment instance that is delimited by the separator.</returns>
    public static StringSegment[] Split(this StringSegment segment, StringSegment separator)
    {
        if (segment.Contains(separator) == false)
            return [];
        
        int[] indices = segment.IndicesOf(separator).ToArray();
        
        StringSegment[] output = new StringSegment[indices.Length];
        
        if (indices.First().Equals(-1))
            return [segment];

        int outputIndex = 0;
        int start = 0;

        for (int i = 0; i < indices.Length; i++)
        {
            if (indices.Any(x => x == i))
            {
                int end = i > 0 ? i - 1 : 0;

                StringSegment newSegment = segment.Subsegment(start, Math.Abs(end - start));

                output[outputIndex] = newSegment;
                outputIndex++;
                start = i;
            }
        }
        
        if(outputIndex < output.Length)
            Array.Resize(ref output, outputIndex);

        return output;
    }
}