/*
    EnhancedLinq 
    Copyright (c) 2025-2026 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
    */

namespace EnhancedLinq.Immediate;

public static partial class EnhancedLinqImmediate
{
    /// <param name="source">The sequence to be searched.</param>
    /// <typeparam name="TSource">The type of elements in the sequence.</typeparam>
    extension<TSource>(IEnumerable<TSource> source)
    {
        /// <summary>
        /// Determines if none of the elements in the sequence match a predicate condition.
        /// </summary>
        /// <param name="predicate">The predicate to check elements against.</param>
        /// <returns>True if none of the elements matched the predicate, false otherwise.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the source sequence or predicate are null.</exception>
        public bool None(Func<TSource, bool> predicate)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(predicate);
            
            return source.CountAtMost(predicate, 0);
        }
    }
}