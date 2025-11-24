/*
    EnhancedLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
    */

using System;
using System.Collections.Generic;
using AlastairLundy.EnhancedLinq.MsExtensions.Internals.Infra;
using AlastairLundy.EnhancedLinq.MsExtensions.Internals.Localizations;
using AlastairLundy.EnhancedLinq.MsExtensions.StringSegments.Deferred.Enumerators;

using Microsoft.Extensions.Primitives;

namespace AlastairLundy.EnhancedLinq.MsExtensions.StringSegments.Deferred;

public static partial class EnhancedLinqSegmentDeferred
{
    /// <param name="segment">The string segment to enumerate.</param>
    extension(StringSegment segment)
    {
        /// <summary>
        /// Enumerates the specified StringSegment.
        /// </summary>
        /// <returns>The <see cref="IEnumerable{T}"/> of chars from the <see cref="StringSegment"/>.</returns>
        /// <exception cref="ArgumentException">Thrown if the StringSegment is null or empty.</exception>
        public IEnumerable<char> AsEnumerable()
        {
            if (StringSegment.IsNullOrEmpty(segment))
                throw new ArgumentException(Resources.Exceptions_Segments_InvalidOperation_EmptySequence);

            return new CustomEnumeratorEnumerable<char>(new SegmentEnumerator(segment));
        }
    }
}