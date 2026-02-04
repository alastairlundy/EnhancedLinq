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
    /// <param name="this">The StringSegment to be searched.</param>
    extension(StringSegment @this)
    {
        /// <summary>
        /// Finds the index of a specified StringSegment within another StringSegment.
        /// </summary>
        /// <param name="segment">The StringSegment to search for.</param>
        /// <returns>The index at which the specified StringSegment can be found, or -1 if not found.</returns>
        public int IndexOf(StringSegment segment)
        {
            if (@this.Length < segment.Length || segment.Length == 0)
                return -1;

            for (int i = 0; i <= @this.Length - segment.Length; i++)
            {
                if (@this[i] == segment[0])
                {
                    StringSegment candidate = @this.Subsegment(i, segment.Length);
                    if (candidate.Equals(segment))
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
                    if (candidate.Equals(segment))
                    {
                        return i;
                    }
                }
            }

            return -1;
        }
    }
}