/*
    EnhancedLinq.Memory
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
    */

using System.Linq;

namespace EnhancedLinq.Memory.Immediate;

public static partial class EnhancedLinqMemoryImmediate
{
    /// <param name="target">The span to be searched.</param>
    /// <typeparam name="T">The type of items stored in the span.</typeparam>
    extension<T>(Span<T> target)
    {
        /// <summary>
        /// Returns whether all items in a Span match the predicate condition.
        /// </summary>
        /// <param name="predicate">The predicate func to be invoked on each item in the Span.</param>
        /// <returns>True if all items in the span match the predicate; false otherwise.</returns>
        public bool All(Func<T, bool> predicate)
        {   
            InvalidOperationException.ThrowIfSpanIsEmpty(target);
            ArgumentNullException.ThrowIfNull(predicate);
            
            Span<bool> groups = (from c in target
                group c by predicate.Invoke(c)
                into g
                where g.Key
                select g.Any());

            return groups.Distinct().Length ==  1;
        }
    }
    
    /// <param name="target">The <see cref="ReadOnlySpan{T}"/> to be searched.</param>
    /// <typeparam name="T">The type of items stored in the <see cref="ReadOnlySpan{T}"/>.</typeparam>
    extension<T>(ReadOnlySpan<T> target)
    {
        /// <summary>
        /// Returns whether all items in a <see cref="ReadOnlySpan{T}"/> match the predicate condition.
        /// </summary>
        /// <param name="predicate">The predicate func to be invoked on each item in the <see cref="ReadOnlySpan{T}"/>.</param>
        /// <returns>True if all items in the <see cref="ReadOnlySpan{T}"/> match the predicate; false otherwise.</returns>
        public bool All(Func<T, bool> predicate)
        {   
            InvalidOperationException.ThrowIfSpanIsEmpty(target);
            ArgumentNullException.ThrowIfNull(predicate);
            
            ReadOnlySpan<bool> groups = (from c in target
                group c by predicate.Invoke(c)
                into g
                where g.Key
                select g.Any());

            return groups.Distinct().Length ==  1;
        }
    }
}