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

using AlastairLundy.EnhancedLinq.MsExtensions.Internals.Localizations;

using Microsoft.Extensions.Primitives;

namespace AlastairLundy.EnhancedLinq.MsExtensions.StringSegments.Immediate;

public static partial class EnhancedLinqSegmentImmediate
{
    /// <param name="target">The StringSegment to be searched.</param>
    extension(StringSegment target)
    {
        /// <summary>
        /// Returns the first char in the StringSegment.
        /// </summary>
        /// <returns>The first char in the StringSegment.</returns>
        /// <exception cref="InvalidOperationException">Thrown if the StringSegment contains zero chars.</exception>
        public char First()
        {
            if (StringSegment.IsNullOrEmpty(target))
                throw new InvalidOperationException(Resources.Exceptions_Segments_InvalidOperation_EmptySequence);

            return target[0];
        }

        /// <summary>
        /// Returns the first character of the specified <see cref="StringSegment"/> or null if the segment is empty.
        /// </summary>
        /// <returns>The first character of the segment if it exists; otherwise, null.</returns>
        public char? FirstOrDefault()
            => StringSegment.IsNullOrEmpty(target) ? null : target[0];
    }

    /// <summary>
    /// Returns the last char in the StringSegment.
    /// </summary>
    /// <param name="target">The StringSegment to be searched.</param>
    /// <returns>The last char in the StringSegment.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the StringSegment contains zero chars.</exception>
    public static char Last(this StringSegment target)
    {
        if (StringSegment.IsNullOrEmpty(target))
            throw new InvalidOperationException(Resources.Exceptions_Segments_InvalidOperation_EmptySequence);

#if NET8_0_OR_GREATER
        return target[^1];
#else
        return target[target.Length - 1];
#endif
    }

    /// <summary>
    /// Returns the last character of the specified <see cref="StringSegment"/> that meets the predicate condition or a null if the segment is empty.
    /// </summary>
    /// <param name="target">The <see cref="StringSegment"/> from which to retrieve the last character.</param>
    /// <returns>The last character of the segment if it contains any characters; otherwise, null.</returns>
    public static char? LastOrDefault(this StringSegment target)
    {
#if NET8_0_OR_GREATER
        char last = target[^1];
#else
        char last = target[target.Length - 1];
#endif

        return StringSegment.IsNullOrEmpty(target) ? null : last;
    }
}