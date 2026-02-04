/*
    EnhancedLinq 
    Copyright (c) 2025-2026 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
    */

using System.Linq;
using EnhancedLinq.MsExtensions.Deferred;

namespace EnhancedLinq.MsExtensions.Immediate;

public static partial class EnhancedLinqSegmentImmediate
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
            ArgumentException.ThrowIfNullOrWhitespace(target);
        
            IEnumerable<bool> groups = target.GroupBy(predicate)
                .Select(g => g.Any());
      
            return groups.Distinct().Count() == 1;
        }
    }
}