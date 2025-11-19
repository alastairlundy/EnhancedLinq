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

using AlastairLundy.EnhancedLinq.MsExtensions.StringSegments.Deferred;

using Microsoft.Extensions.Primitives;

namespace AlastairLundy.EnhancedLinq.MsExtensions.StringSegments.Immediate;

public static partial class EnhancedLinqSegmentImmediate
{
    /// <param name="target">The StringSegment to be searched.</param>
    extension(StringSegment target)
    {
        /// <summary>
        /// Returns whether all chars in a StringSegment match the predicate condition.
        /// </summary>
        /// <param name="predicate">The predicate func to be invoked on each item in the StringSegment.</param>
        /// <returns>True if all chars in the StringSegment match the predicate; false otherwise.</returns>
        public bool All(Func<char, bool> predicate)
        {
            if(StringSegment.IsNullOrEmpty(target))
                throw new ArgumentNullException(nameof(target));
        
            IEnumerable<bool> groups = target.GroupBy(predicate)
                .Select(g => g.Any());
      
            return groups.Distinct().Count() == 1;
        }
    }
}