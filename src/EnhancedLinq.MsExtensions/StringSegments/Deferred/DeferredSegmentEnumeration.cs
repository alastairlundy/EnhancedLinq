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
using AlastairLundy.DotPrimitives.Collections.Enumerables;
using AlastairLundy.EnhancedLinq.MsExtensions.Internals.Localizations;
using AlastairLundy.EnhancedLinq.MsExtensions.StringSegments.Deferred.Enumerators;
using Microsoft.Extensions.Primitives;

namespace AlastairLundy.EnhancedLinq.MsExtensions.StringSegments.Deferred;

public static partial class EnhancedLinqSegmentDeferred
{
    /// <summary>
    /// Enumerates the specified StringSegment.
    /// </summary>
    /// <param name="segment">The string segment to enumerate.</param>
    /// <returns>The <see cref="IEnumerable{T}"/> of chars from the <see cref="StringSegment"/>.</returns>
    /// <exception cref="ArgumentException">Thrown if the StringSegment is null or empty.</exception>
    public static IEnumerable<char> AsEnumerable(this StringSegment segment)
    {
        if (StringSegment.IsNullOrEmpty(segment))
            throw new ArgumentException(Resources.Exceptions_Segments_InvalidOperation_EmptySequence);

        return new CustomEnumeratorEnumerable<char>(new SegmentEnumerator(segment));
    }
}