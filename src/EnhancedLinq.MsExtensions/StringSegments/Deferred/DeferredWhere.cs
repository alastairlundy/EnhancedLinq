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
using AlastairLundy.EnhancedLinq.MsExtensions.Internals.Infra;
using AlastairLundy.EnhancedLinq.MsExtensions.StringSegments.Deferred.Enumerators;
using Microsoft.Extensions.Primitives;

namespace AlastairLundy.EnhancedLinq.MsExtensions.StringSegments.Deferred;

/// <summary>
/// 
/// </summary>
public static partial class EnhancedLinqSegmentDeferred
{
    /// <summary>
    /// Returns an IEnumerable of chars that match the predicate. 
    /// </summary>
    /// <param name="target">The StringSegment to search.</param>
    /// <param name="predicate">The predicate to check each char against.</param>
    /// <returns>An IEnumerable of chars that matches the predicate.</returns>
    public static IEnumerable<char> Where(this StringSegment target, Func<char, bool> predicate)
    {
        if(StringSegment.IsNullOrEmpty(target)) 
            throw new ArgumentNullException(nameof(target));

        return new CustomEnumeratorEnumerable<char>(new WhereSegmentEnumerator(target, predicate));
    }
}