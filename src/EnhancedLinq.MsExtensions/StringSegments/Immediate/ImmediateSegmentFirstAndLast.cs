/*
    EnhancedLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/.
 */

using System;

using AlastairLundy.EnhancedLinq.MsExtensions.Internals.Localizations;

using Microsoft.Extensions.Primitives;

namespace AlastairLundy.EnhancedLinq.MsExtensions.StringSegments.Immediate;

public static partial class EnhancedLinqSegmentImmediate
{

    /// <summary>
    /// Returns the first char in the StringSegment.
    /// </summary>
    /// <param name="target">The StringSegment to be searched.</param>
    /// <returns>The first char in the StringSegment.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the StringSegment contains zero chars.</exception>
    public static char First(this StringSegment target)
    {
        if (StringSegment.IsNullOrEmpty(target))
            throw new InvalidOperationException(Resources.Exceptions_Segments_InvalidOperation_EmptySequence);

        return target[0];
    }

    /// <summary>
    /// Returns the first character of the specified <see cref="StringSegment"/> or null if the segment is empty.
    /// </summary>
    /// <param name="target">The <see cref="StringSegment"/> from which to retrieve the first character.</param>
    /// <returns>The first character of the segment if it exists; otherwise, null.</returns>
    public static char? FirstOrDefault(this StringSegment target)
        => StringSegment.IsNullOrEmpty(target) ? null : target[0];

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