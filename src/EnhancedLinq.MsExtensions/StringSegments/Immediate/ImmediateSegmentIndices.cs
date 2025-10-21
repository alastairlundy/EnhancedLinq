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

using AlastairLundy.EnhancedLinq.MsExtensions.Internals.Localizations;

using Microsoft.Extensions.Primitives;

namespace AlastairLundy.EnhancedLinq.MsExtensions.StringSegments.Immediate;

public static partial class EnhancedLinqSegmentImmediate
{
    /// <summary>
    /// Retrieves a list of indices where the specified character can be found within the given <see cref="StringSegment"/>.
    /// </summary>
    /// <param name="source">The <see cref="StringSegment"/> to search.</param>
    /// <param name="c">The character to find and return its indices for.</param>
    /// <returns>A list containing the indices where the specified character can be found, or empty if not found.</returns>
    public static IList<int> IndicesOf(this StringSegment source, char c)
    {
        if(StringSegment.IsNullOrEmpty(source))
            throw new InvalidOperationException(Resources.Exceptions_Segments_InvalidOperation_EmptySequence);
        
        List<int> indices = new List<int>();
        
        for(int index = 0; index < source.Length; index++)
        {
            if (source[index] == c)
            {
                indices.Add(index);
            }
        }

        return indices;
    }
}