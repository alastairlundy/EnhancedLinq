/*
    EnhancedLinq 
    Copyright (c) 2025-2026 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
    */

namespace EnhancedLinq.MsExtensions.Immediate;

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
            ArgumentException.ThrowIfNullOrEmpty(target);

            return target[0];
        }

        /// <summary>
        /// Returns the first character of the specified <see cref="StringSegment"/> or null if the segment is empty.
        /// </summary>
        /// <returns>The first character of the segment if it exists; otherwise, null.</returns>
        public char? FirstOrDefault()
            => StringSegment.IsNullOrEmpty(target) ? null : target[0];
    }

    /// <param name="target">The StringSegment to be searched.</param>
    extension(StringSegment target)
    {
        /// <summary>
        /// Returns the last char in the StringSegment.
        /// </summary>
        /// <returns>The last char in the StringSegment.</returns>
        /// <exception cref="InvalidOperationException">Thrown if the StringSegment contains zero chars.</exception>
        public char Last()
        {
            ArgumentException.ThrowIfNullOrEmpty(target);

            return target[^1];
        }

        /// <summary>
        /// Returns the last character of the specified <see cref="StringSegment"/> that meets the predicate condition or a null if the segment is empty.
        /// </summary>
        /// <returns>The last character of the segment if it contains any characters; otherwise, null.</returns>
        public char? LastOrDefault()
        {
            char last = target[^1];

            return StringSegment.IsNullOrEmpty(target) ? null : last;
        }
    }
}