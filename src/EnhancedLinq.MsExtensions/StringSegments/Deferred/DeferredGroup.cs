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
    /// Groups the characters in the specified <see cref="StringSegment"/> according to a specified key predicate function.
    /// </summary>
    /// <typeparam name="TKey">The type of the key returned by <paramref name="predicate"/>.</typeparam>
    /// <param name="target">The <see cref="StringSegment"/> whose characters to group.</param>
    /// <param name="predicate">A function to extract the key for each character.</param>
    /// <returns>A sequence where each <see cref="IGrouping{TKey,TElement}"/> contains a sequence of characters that share the same key.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="target"/> is null or empty.</exception>
    public static IEnumerable<IGrouping<TKey, char>> GroupBy<TKey>(this StringSegment target, 
        Func<char, TKey> predicate)
    {
        if(StringSegment.IsNullOrEmpty(target))
            throw new ArgumentNullException(nameof(target));
        
        return new CustomEnumeratorEnumerable<IGrouping<TKey, char>>(
            new GroupStringSegmentEnumerator<TKey>(target, predicate));
    }
}