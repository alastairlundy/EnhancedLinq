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

using System.Collections.Generic;
using System.Linq;

using Microsoft.Extensions.Primitives;

namespace AlastairLundy.EnhancedLinq.MsExtensions.StringSegments.Immediate;

public static partial class EnhancedLinqSegmentImmediate
{
    /// <param name="this">The StringSegment to be searched.</param>
    extension(StringSegment @this)
    {
        /// <summary>
        /// Finds the index of a specified StringSegment within another StringSegment.
        /// </summary>
        /// <param name="segment">The StringSegment to search for.</param>
        /// <returns>The index at which the specified StringSegment can be found, or -1 if not found.</returns>
        public int IndexOf(StringSegment segment)
        {
            if (@this.Length < segment.Length || segment.Length == 0)
                return -1;
        
            IEnumerable<int> indexes = @this.IndicesOf(segment[0])
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
    }

    /// <param name="str">The string to be searched.</param>
    extension(string str)
    {
        /// <summary>
        /// Finds the index of a specified StringSegment within a string.
        /// </summary>
        /// <param name="segment">The StringSegment to search for.</param>
        /// <returns>The index at which the specified StringSegment can be found, or -1 if not found.</returns>
        public int IndexOf(StringSegment segment)
        {
            if (str.Length < segment.Length || segment.Length == 0)
                return -1;
        
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == segment[0])
                {
                    StringSegment indexSegment = str.Substring(i, segment.Length);

                    if (indexSegment.Equals(segment))
                    {
                        return i;
                    }
                }
            }

            return -1;
        }
    }
}