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
    /// <param name="source">The source <see cref="StringSegment"/>.</param>
    extension(StringSegment source)
    {
        /// <summary>
        /// Determines whether there are at least a specified number of elements in the <see cref="StringSegment"/>./>.
        /// </summary>
        /// <param name="countToLookFor">The minimum count to look for.</param>
        /// <returns><c>true</c> if there are at least the specified number of elements in the <see cref="StringSegment"/>; otherwise, <c>false</c>.</returns>
        public bool CountAtLeast(int countToLookFor)
        {
            ArgumentException.ThrowIfNullOrWhitespace(source);
            ArgumentOutOfRangeException.ThrowIfNegative(countToLookFor);
            
            return source.Length >= countToLookFor;
        }

        /// <summary>
        /// Determines whether there are at least a specified number of elements in the <see cref="StringSegment"/> that meet a given condition.
        /// </summary>
        /// <param name="predicate">The predicate condition to check elements against.</param>
        /// <param name="countToLookFor">The minimum count to look for.</param>
        /// <returns><c>true</c> if there are at least the specified number of elements that meet the condition; otherwise, <c>false</c>.</returns>
        public bool CountAtLeast(Func<char, bool> predicate,
            int countToLookFor)
        {
            ArgumentException.ThrowIfNullOrWhitespace(source);
            ArgumentNullException.ThrowIfNull(predicate);
            ArgumentOutOfRangeException.ThrowIfNegative(countToLookFor);
        
            int currentCount = 0;

            for (int index = 0; index < source.Length; index++ )
            {
                char c = source[index];

                if (predicate(c))
                    currentCount += 1;
            
                if(currentCount >= countToLookFor)
                    return true;
            }

            return false;
        }
    }
}