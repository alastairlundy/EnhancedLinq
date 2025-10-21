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

using AlastairLundy.EnhancedLinq.MsExtensions.StringSegments.Deferred.Enumerables;

using Microsoft.Extensions.Primitives;

namespace AlastairLundy.EnhancedLinq.MsExtensions.StringSegments.Deferred;

public static partial class EnhancedLinqSegmentDeferred
{
    
    /// <summary>
    /// Finds all occurrences of a specified char within a <see cref="StringSegment"/>
    /// </summary>
    /// <param name="source">The <see cref="StringSegment"/> to be searched.</param>
    /// <param name="c">The <see cref="char"/> to search for.</param>
    /// <returns>A sequence of Indices for all occurrences of the specified char within the StringSegment; empty if not found within the String Segment.</returns>
    /// <exception cref="ArgumentException">Thrown if the source is null or empty.</exception>
    public static IEnumerable<int> IndicesOf(this StringSegment source, char c)
    {
        if (StringSegment.IsNullOrEmpty(source))
            throw new ArgumentException();

        return new SegmentIndicesEnumerable(source, c);
    }

    /// <summary>
    /// Finds all occurrences of a specified StringSegment within a StringSegment.
    /// </summary>
    /// <param name="source">The <see cref="StringSegment"/> to be searched.</param>
    /// <param name="segment">The StringSegment to search for.</param>
    /// <returns>A sequence of Indices for all occurrences of the specified StringSegment within the StringSegment; empty if not found within the String Segment.</returns>
    /// <exception cref="ArgumentException">Thrown if the source is null or empty.</exception>
    public static IEnumerable<int> IndicesOf(this StringSegment source, StringSegment segment)
    {
        if (StringSegment.IsNullOrEmpty(source))
            throw new ArgumentException();

        return new SegmentIndicesEnumerable(source, segment);
    }
}