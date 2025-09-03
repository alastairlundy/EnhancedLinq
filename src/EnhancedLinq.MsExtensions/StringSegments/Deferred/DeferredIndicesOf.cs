/*
    EnhancedLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/.
 */

using System;
using System.Collections.Generic;

using AlastairLundy.EnhancedLinq.MsExtensions.StringSegments.Deferred.Enumerables;

using Microsoft.Extensions.Primitives;

namespace AlastairLundy.EnhancedLinq.MsExtensions.StringSegments.Deferred;

public static partial class EnhancedLinqDeferredSegment
{
    
    /// <summary>
    /// Finds all occurrences of a specified char within a <see cref="StringSegment"/>
    /// </summary>
    /// <param name="source">The <see cref="StringSegment"/> to be searched.</param>
    /// <param name="c">The <see cref="char"/> to search for.</param>
    /// <returns>A sequence of Indices for all occurrences of the specified char within the StringSegment; empty if not found within the String Segment.</returns>
    /// <exception cref="ArgumentException">Thrown if the source is null or empty.</exception>
    public static IEnumerable<int> IndicesOf(this StringSegment source, char c)
    {
        if (StringSegment.IsNullOrEmpty(source))
            throw new ArgumentException();

        return new SegmentIndicesEnumerable(source, c);
    }

    /// <summary>
    /// Finds all occurrences of a specified StringSegment within a StringSegment.
    /// </summary>
    /// <param name="source">The <see cref="StringSegment"/> to be searched.</param>
    /// <param name="segment">The StringSegment to search for.</param>
    /// <returns>A sequence of Indices for all occurrences of the specified StringSegment within the StringSegment; empty if not found within the String Segment.</returns>
    /// <exception cref="ArgumentException">Thrown if the source is null or empty.</exception>
    public static IEnumerable<int> IndicesOf(this StringSegment source, StringSegment segment)
    {
        if (StringSegment.IsNullOrEmpty(source))
            throw new ArgumentException();

        return new SegmentIndicesEnumerable(source, segment);
    }
}