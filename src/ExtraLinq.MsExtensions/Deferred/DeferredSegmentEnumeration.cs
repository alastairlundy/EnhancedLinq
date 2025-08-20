/*
    ExtraLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/.
 */

using System;
using System.Collections.Generic;

using ExtraLinq.MsExtensions.Deferred.Enumerables;

using ExtraLinq.MsExtensions.Internals.Localizations;

using Microsoft.Extensions.Primitives;

namespace ExtraLinq.MsExtensions.Deferred;

public static class DeferredSegmentEnumeration
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

        return new SegmentEnumerable(segment);
    }
}