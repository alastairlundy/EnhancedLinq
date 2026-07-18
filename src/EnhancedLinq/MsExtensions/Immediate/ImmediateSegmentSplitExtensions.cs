/*
    EnhancedLinq 
    Copyright (c) 2025-2026 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
    */

using EnhancedLinq.MsExtensions.Internals;

namespace EnhancedLinq.MsExtensions.Immediate;

/// <summary>
/// Provides extension methods for performing immediate splitting operations on StringSegment objects.
/// </summary>
public static class ImmediateSegmentSplitExtensions
{
    /// <param name="source">The source StringSegment.</param>
    extension(StringSegment source)
    {
        /// <summary>
        /// Splits a StringSegment into StringSegment subsegments using a specified <see cref="char"/> separator.
        /// </summary>
        /// <param name="separator">The separator to delimit the char in the source StringSegment.</param>
        /// <returns>An array of StringSegment subsegments from the source StringSegment that is delimited by the separator if the separator character is found.</returns>
        public StringSegment[] Split(char separator)
        {
            if (StringSegment.IsNullOrEmpty(source))
                return [];

            List<StringSegment> segments = [];
            int start = 0;

            for (int index = 0; index < source.Length; index++)
            {
                if (source[index] == separator)
                {
                    segments.Add(source.Subsegment(start, index - start));
                    start = index + 1;
                }
            }

            segments.Add(source.Subsegment(start, source.Length - start));

            return segments.ToArray();
        }

        /// <summary>
        /// Splits a StringSegment into StringSegment subsegments using a specified <see cref="StringSegment"/> separator.
        /// </summary>
        /// <param name="separator">The separator to delimit the StringSegment subsegments in the source StringSegment.</param>
        /// <returns>An array of StringSegment subsegments from the source StringSegment that is delimited by the separator.</returns>
        public StringSegment[] Split(StringSegment separator)
        {
            StringSegmentGuard.ThrowIfNullOrEmpty(separator);

            IList<int> indices = source.IndicesOf(separator);

            List<StringSegment> output = [];

            int start = 0;

            for (int i = 0; i < indices.Count; i++)
            {
                int index = indices[i];
                int length = index - start;
                output.Add(source.Subsegment(start, length));
                start = index + separator.Length;
            }

            output.Add(source.Subsegment(start, source.Length - start));

            return output.ToArray();
        }
    }
}