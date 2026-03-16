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
public static class ImmediateSegmentIndexOfExtensions
{
    /// <param name="segment">The StringSegment to be searched.</param>
    extension(StringSegment segment)
    {
        /// <summary>
        /// Finds the index of a specified StringSegment within another StringSegment.
        /// </summary>
        /// <param name="other">The StringSegment to search for.</param>
        /// <returns>The index at which the specified StringSegment can be found, or -1 if not found.</returns>
        public int IndexOf(StringSegment other)
        {
            if (segment.Length < other.Length || other.Length == 0)
                return -1;

            for (int i = 0; i <= segment.Length - other.Length; i++)
            {
                if (segment[i] == other[0])
                {
                    StringSegment candidate = segment.Subsegment(i, other.Length);
                    if (candidate.Equals(other, StringComparison.CurrentCulture))
                    {
                        return i;
                    }
                }
            }

            return -1;
        }
    }

    /// <param name="str">The string to be searched.</param>
    extension(string str)
    {
        /// <summary>
        /// Finds the index of a specified StringSegment within a string.
        /// </summary>
        /// <param name="segment">The StringSegment to search for.</param>
        /// <returns>The index at which the specified StringSegment can be found, or -1 if not found.</returns>
        public int IndexOf(StringSegment segment)
        {
            ArgumentException.ThrowIfNullOrEmpty(str);

            if (segment.Length == 0 || str.Length < segment.Length)
                return -1;

            for (int i = 0; i <= str.Length - segment.Length; i++)
            {
                if (str[i] == segment[0])
                {
                    StringSegment candidate = new(str, i, segment.Length);
                    if (candidate.Equals(segment,  StringComparison.CurrentCulture))
                    {
                        return i;
                    }
                }
            }

            return -1;
        }
    }
}