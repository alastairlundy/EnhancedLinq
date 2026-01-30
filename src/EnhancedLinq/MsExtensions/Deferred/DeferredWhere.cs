/*
    EnhancedLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
    */

namespace EnhancedLinq.MsExtensions.Deferred;

/// <summary>
/// 
/// </summary>
public static partial class EnhancedLinqSegmentDeferred
{
    /// <param name="target">The StringSegment to search.</param>
    extension(StringSegment target)
    {
        /// <summary>
        /// Returns an IEnumerable of chars that match the predicate. 
        /// </summary>
        /// <param name="predicate">The predicate to check each char against.</param>
        /// <returns>An IEnumerable of chars that matches the predicate.</returns>
        public IEnumerable<char> Where(Func<char, bool> predicate)
        {
            ArgumentException.ThrowIfNullOrWhitespace(target);
            ArgumentNullException.ThrowIfNull(predicate);

            return new CustomEnumeratorEnumerable<char>(new WhereSegmentEnumerator(target, predicate));
        }
    }
}