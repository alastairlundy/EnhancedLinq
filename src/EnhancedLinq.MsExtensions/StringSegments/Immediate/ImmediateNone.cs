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
    /// <summary>
    /// Determines if none of the characters in the <see cref="StringSegment"/> match a predicate condition.
    /// </summary>
    /// <param name="segment">The <see cref="StringSegment"/> to be searched.</param>
    /// <param name="predicate">The predicate to check characters against.</param>
    /// <returns>True if none of the characters matched the predicate, false otherwise.</returns>
    /// <exception cref="ArgumentNullException">Thrown if the source <see cref="StringSegment"/> or predicate are null.</exception>
    public static bool None(this StringSegment segment, Func<char, bool> predicate)
        => CountAtMost(segment, predicate, 0);
}