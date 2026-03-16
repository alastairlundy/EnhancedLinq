/*
    EnhancedLinq 
    Copyright (c) 2025-2026 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
    */

namespace EnhancedLinq.MsExtensions.Immediate;

/// <summary>
/// 
/// </summary>
public static class ImmediateSegmentAnyExtensions
{
    /// <param name="target">The StringSegment to be searched.</param>
    extension(StringSegment target)
    {
        /// <summary>
        /// Returns whether a StringSegment has any chars.
        /// </summary>
        /// <returns>True if any char exists in the StringSegment; false otherwise.</returns>
        public bool Any()
            => target.Length > 0;

        /// <summary>
        /// Returns whether any char in a StringSegment matches the predicate condition.
        /// </summary>
        /// <param name="predicate">The predicate func to be invoked on each char in the StringSegment.</param>
        /// <returns>True if any char in the StringSegment matches the predicate; false otherwise.</returns>
        public bool Any(Func<char, bool> predicate)
        {
            ArgumentException.ThrowIfNullOrWhitespace(target);
            ArgumentNullException.ThrowIfNull(predicate);

            for (int i = 0; i < target.Length; i++)
            {
                bool isMatch = predicate(target[i]);
                
                if (isMatch)
                    return true;
            }

            return false;
        }
    }
}