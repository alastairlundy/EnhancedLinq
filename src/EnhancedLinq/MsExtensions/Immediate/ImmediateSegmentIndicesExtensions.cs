/*
    EnhancedLinq 
    Copyright (c) 2025-2026 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
    */

namespace EnhancedLinq.MsExtensions.Immediate;

/// <summary>
/// Provides extension methods for working with <see cref="StringSegment"/> indices
/// </summary>
public static class ImmediateSegmentIndicesExtensions
{
    /// <param name="segment">The StringSegment to search through.</param>
    extension(StringSegment segment)
    {
        /// <summary>
        /// Returns the indices of all occurrences of a specified character within the given StringSegment.
        /// </summary>
        /// <param name="c">The character to find.</param>
        /// <returns>A list containing the zero-based index positions where the specified character was found in the segment.</returns>
        public IList<int> IndicesOf(char c)
        {
            List<int> output = new();

            for (int i = 0; i < segment.Length; i++)
            {
                if(segment[i] == c)
                    output.Add(i);
            }
            
            return output;
        }

        /// <summary>
        /// Searches for all occurrences of a specified <see cref="StringSegment"/> within the given <see cref="StringSegment"/> and returns their zero-based indices.
        /// </summary>
        /// <param name="other">The <see cref="StringSegment"/> to find.</param>
        /// <returns>A list containing the zero-based index positions where the specified <see cref="StringSegment"/> was found in the <see cref="StringSegment"/>.</returns>
        public IList<int> IndicesOf(StringSegment other)
        {
            List<int> output = new List<int>();

            IList<int> firstLetterIndices = segment.IndicesOf(other.First());

            foreach (int index in firstLetterIndices)
            {
                if (index == -1)
                    continue;
                
                if (index + other.Length < segment.Length)
                {
                    StringSegment slice = segment.Subsegment(index, other.Length);
                
                    if(slice.Equals(other, StringComparison.CurrentCulture))
                        output.Add(index);
                }
            }
            
            return output;  
        }
    }
}