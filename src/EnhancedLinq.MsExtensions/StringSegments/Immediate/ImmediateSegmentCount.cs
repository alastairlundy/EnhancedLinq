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
using Microsoft.Extensions.Primitives;

namespace AlastairLundy.EnhancedLinq.MsExtensions.StringSegments.Immediate;

public static partial class EnhancedLinqSegmentImmediate
{
    /// <param name="target">The StringSegment to search.</param>
    extension(StringSegment target)
    {
        /// <summary>
        /// Counts the number of chars in the StringSegment that match the predicate.
        /// </summary>
        /// <param name="predicate">The predicate to check each char against.</param>
        /// <returns>The number of chars matching the predicate condition as an integer.</returns>
        public int Count(Func<char, bool> predicate)
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
}