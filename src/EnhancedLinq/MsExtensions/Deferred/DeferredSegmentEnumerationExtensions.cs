/*
    EnhancedLinq 
    Copyright (c) 2025-2026 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
    */

using EnhancedLinq.MsExtensions.Internals;

namespace EnhancedLinq.MsExtensions.Deferred;

/// <summary>
/// Provides extension methods for deferred enumeration of string segments.
/// </summary>
public static class DeferredSegmentEnumerationExtensions
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
            StringSegmentGuard.ThrowIfNullOrWhitespace(segment);

            return new CustomEnumeratorEnumerable<char>(new SegmentEnumerator(segment));
        }
    }
}