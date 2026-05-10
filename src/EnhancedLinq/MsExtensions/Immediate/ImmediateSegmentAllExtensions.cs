/*
    EnhancedLinq 
    Copyright (c) 2025-2026 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
    */

namespace EnhancedLinq.MsExtensions.Immediate;

/// <summary>
/// Provides a set of extension methods for immediate segment processing in LINQ operations.
/// </summary>
public static class ImmediateSegmentAllExtensions
{
    /// <param name="target">The StringSegment to be searched.</param>
    extension(StringSegment target)
    {
        /// <summary>
        /// Returns whether all chars in a StringSegment match the predicate condition.
        /// </summary>
        /// <param name="predicate">The predicate func to be invoked on each item in the StringSegment.</param>
        /// <returns>True if all chars in the StringSegment match the predicate; false otherwise.</returns>
        public bool All(Func<char, bool> predicate)
        {
            bool previousValue = predicate(target.First());
            
            ArgumentNullException.ThrowIfNull(predicate);
            ArgumentException.ThrowIfNullOrWhitespace(target);

            for (int index = 0; index < target.Length; index++)
            {
                char current = target[index];
                
                bool result = predicate(current);
                
                if(previousValue != result)
                    return false;
            }

            return true;
        }
    }
}