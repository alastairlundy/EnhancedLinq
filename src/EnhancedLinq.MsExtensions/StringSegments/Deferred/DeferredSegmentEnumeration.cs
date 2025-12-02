/*
    EnhancedLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
    */

using EnhancedLinq.MsExtensions.Internals.Infra;

namespace EnhancedLinq.MsExtensions.Deferred;

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
            ArgumentException.ThrowIfNullOrWhitespace(segment);

            return new CustomEnumeratorEnumerable<char>(new SegmentEnumerator(segment));
        }
    }
}