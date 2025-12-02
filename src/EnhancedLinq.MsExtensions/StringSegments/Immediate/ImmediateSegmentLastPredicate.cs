/*
    EnhancedLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
    */

using System;
using EnhancedLinq.MsExtensions.Internals.Localizations;
using Microsoft.Extensions.Primitives;

namespace EnhancedLinq.MsExtensions.StringSegments.Immediate;

public static partial class EnhancedLinqSegmentImmediate
{
    /// <param name="target">The StringSegment to be searched.</param>
    extension(StringSegment target)
    {
        /// <summary>
        /// Returns the last character of the specified <see cref="StringSegment"/> that meets the predicate condition.
        /// </summary>
        /// <param name="predicate">The predicate func condition to be checked against each char in the StringSegment.</param>
        /// <returns>The last character of the segment that meets the predicate condition if any match.</returns>
        /// <exception cref="ArgumentException">Thrown if no characters in the StringSegment meet the predicate condition.</exception>
        public char Last(Func<char, bool> predicate)
        {
            if(StringSegment.IsNullOrEmpty(target))
                throw new InvalidOperationException(Resources.Exceptions_Segments_InvalidOperation_EmptySequence);
        
            for (int i = target.Length - 1; i >= 0; i--)
            {
                if(predicate(target[i]))
                    return target[i];
            }

            throw new ArgumentException();
        }

        /// <summary>
        /// Returns the last character of the specified <see cref="StringSegment"/> that matches the predicate condition or a default value if the segment is empty.
        /// </summary>
        /// <param name="predicate">The predicate func condition to be checked against each char in the StringSegment.</param>
        /// <returns>The last character of the segment that meets the predicate condition if any match; otherwise, null.</returns>
        public char? LastOrDefault(Func<char, bool> predicate)
        {
            if(StringSegment.IsNullOrEmpty(target))
                throw new InvalidOperationException(Resources.Exceptions_Segments_InvalidOperation_EmptySequence);
        
            for (int i = target.Length - 1; i >= 0; i--)
            {
                if(predicate(target[i]))
                    return target[i];
            }

            return null;
        }
    }
}