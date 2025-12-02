/*
    EnhancedLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
    */

using System;
using Microsoft.Extensions.Primitives;

namespace EnhancedLinq.MsExtensions.StringSegments.Immediate;

public static partial class EnhancedLinqSegmentImmediate
{
    /// <param name="segment">The <see cref="StringSegment"/> to be searched.</param>
    extension(StringSegment segment)
    {
        /// <summary>
        /// Determines if none of the characters in the <see cref="StringSegment"/> match a predicate condition.
        /// </summary>
        /// <param name="predicate">The predicate to check characters against.</param>
        /// <returns>True if none of the characters matched the predicate, false otherwise.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the source <see cref="StringSegment"/> or predicate are null.</exception>
        public bool None(Func<char, bool> predicate)
            => CountAtMost(segment, predicate, 0);
    }
}