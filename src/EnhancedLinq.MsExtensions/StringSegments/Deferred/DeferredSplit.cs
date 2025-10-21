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

using AlastairLundy.DotExtensions.MsExtensions.StringSegments;

using AlastairLundy.DotPrimitives.Collections.Enumerables;

using AlastairLundy.EnhancedLinq.MsExtensions.StringSegments.Deferred.Enumerables;
using AlastairLundy.EnhancedLinq.MsExtensions.StringSegments.Deferred.Enumerators;

using Microsoft.Extensions.Primitives;

namespace AlastairLundy.EnhancedLinq.MsExtensions.StringSegments.Deferred;

public static partial class EnhancedLinqSegmentDeferred
{
    /// <summary>
    /// Splits the given <see cref="StringSegment"/> into segments separated by the specified character.
    /// </summary>
    /// <param name="source">The <see cref="StringSegment"/> to split.</param>
    /// <param name="separator">The character separator to split by.</param>
    /// <returns>An <see cref="IEnumerable{StringSegment}"/> containing the split segments. Returns an empty sequence if the separator is not found.</returns>
    public static IEnumerable<StringSegment> SplitBy(this StringSegment source, char separator)
    {
        if (StringSegment.IsNullOrEmpty(source))
            throw new ArgumentException();
        
        if (source.Contains(separator) == false)
            return [source];

        return SplitBy(source, x => x == separator);
    }
    
    /// <summary>
    /// Splits the given <see cref="StringSegment"/> into segments separated by the specified <see cref="StringSegment"/> separator.
    /// </summary>
    /// <param name="source">The <see cref="StringSegment"/> to split.</param>
    /// <param name="separator">The <see cref="StringSegment"/> separator to split by.</param>
    /// <returns>An <see cref="IEnumerable{StringSegment}"/> containing the split segments. Returns an empty sequence if the separator is not found.</returns>
    /// <exception cref="ArgumentException">Thrown if <paramref name="separator"/> or <paramref name="source"/> is null or empty.</exception>
    public static IEnumerable<StringSegment> SplitBy(this StringSegment source, StringSegment separator)
    {
        if (StringSegment.IsNullOrEmpty(separator) || StringSegment.IsNullOrEmpty(source))
            throw new ArgumentException();
            
        if (source.Contains(separator) == false)
            return [source];
        
        return new SegmentSplitEnumerable(source, separator);
    }

    /// <summary>
    /// Splits the given <see cref="StringSegment"/> into segments when the predicate evaluates to true.
    /// </summary>
    /// <param name="source">The <see cref="StringSegment"/> to split.</param>
    /// <param name="predicate">The predicate to split on when true.</param>
    /// <returns>An <see cref="IEnumerable{StringSegment}"/> containing the split segments.</returns>
    public static IEnumerable<StringSegment> SplitBy(this StringSegment source, Func<char, bool> predicate)
    {
        if (StringSegment.IsNullOrEmpty(source))
            throw new ArgumentException();

        return new CustomEnumeratorEnumerable<StringSegment>
            (new SegmentSplitPredicateEnumerator(source, predicate));
    }
}