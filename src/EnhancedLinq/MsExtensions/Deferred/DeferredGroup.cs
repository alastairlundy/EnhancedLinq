/*
    EnhancedLinq 
    Copyright (c) 2025-2026 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
    */

using System.Linq;

namespace EnhancedLinq.MsExtensions.Deferred;

/// <summary>
/// 
/// </summary>
public static partial class EnhancedLinqSegmentDeferred
{
    /// <param name="target">The <see cref="StringSegment"/> whose characters to group.</param>
    extension(StringSegment target)
    {
        /// <summary>
        /// Groups the characters in the specified <see cref="StringSegment"/> according to a specified key predicate function.
        /// </summary>
        /// <typeparam name="TKey">The type of the key returned by <paramref name="predicate"/>.</typeparam>
        /// <param name="predicate">A function to extract the key for each character.</param>
        /// <returns>A sequence where each <see cref="IGrouping{TKey,TElement}"/> contains a sequence of characters that share the same key.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="target"/> is null or empty.</exception>
        public IEnumerable<IGrouping<TKey, char>> GroupBy<TKey>(Func<char, TKey> predicate)
        {
            ArgumentException.ThrowIfNullOrWhitespace(target);
            ArgumentNullException.ThrowIfNull(predicate);
        
            return new CustomEnumeratorEnumerable<IGrouping<TKey, char>>(
                new GroupStringSegmentEnumerator<TKey>(target, predicate));
        }
    }
}