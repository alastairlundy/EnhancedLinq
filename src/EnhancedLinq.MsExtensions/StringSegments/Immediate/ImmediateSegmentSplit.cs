/*
      EnhancedLinq 
      Copyright (c) 2025 Alastair Lundy
      
     Licensed under the Apache License, Version 2.0 (the "License");
     you may not use this file except in compliance with the License.
     You may obtain a copy of the License at

         http://www.apache.org/licenses/LICENSE-2.0

     Unless required by applicable law or agreed to in writing, software
     distributed under the License is distributed on an "AS IS" BASIS,
     WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
     See the License for the specific language governing permissions and
     limitations under the License.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
                    segments.Add(new StringSegment(current.ToString()));
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
        IEnumerable<int> indices = source.IndicesOf(separator)
            .Where(x => x != -1);

        List<StringSegment> output = new();

        int start = 0;

        foreach(int index in indices)
        {
            int end = index > 0 ? index - 1 : 0;

            StringSegment newSegment = source.Subsegment(start, Math.Abs(end - start));

            output.Add(newSegment);
            start = index;
        }
        
        return output.ToArray();
    }
}