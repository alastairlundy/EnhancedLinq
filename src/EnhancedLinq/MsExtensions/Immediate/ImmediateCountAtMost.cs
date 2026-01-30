/*
    EnhancedLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
    */

namespace EnhancedLinq.MsExtensions.Immediate;

public static partial class EnhancedLinqSegmentImmediate
{
    /// <param name="source">The source <see cref="StringSegment"/> to search through.</param>
    extension(StringSegment source)
    {
        /// <summary>
        /// Determines whether there is at most a maximum number elements in the source <see cref="StringSegment"/>.
        /// </summary>
        /// <param name="countToLookFor">The maximum number of elements that can meet the condition.</param>
        /// <returns>True if there are at most <paramref name="countToLookFor"/> number of elements, false otherwise.</returns>
        public bool CountAtMost(int countToLookFor)
        {
            ArgumentException.ThrowIfNullOrWhitespace(source);
            ArgumentOutOfRangeException.ThrowIfNegative(countToLookFor);

            return source.Length <= countToLookFor;
        }

        /// <summary>
        /// Determines whether there are at most a maximum number elements in the source <see cref="StringSegment"/> that satisfy the given condition.
        /// </summary>
        /// <param name="predicate">The predicate condition to check elements against.</param>
        /// <param name="countToLookFor">The maximum number of elements that can meet the condition.</param>
        /// <returns>True if there are at most <paramref name="countToLookFor"/> number of elements that satisfy the condition, false otherwise.</returns>
        public bool CountAtMost(Func<char, bool> predicate,
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
                    return false;
            }

            return true;
        }
    }
}