/*
    EnhancedLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
    */

using System.Linq;

namespace EnhancedLinq.Deferred;

public static partial class EnhancedLinqDeferred
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="source">The sequence to be searched.</param>
    /// <typeparam name="TSource">The type of elements in the source sequence.</typeparam>
    extension<TSource>(IEnumerable<TSource> source)
    {
        /// <summary>
        /// Excludes items in one sequence from another sequence.
        /// </summary>
        /// <param name="exclude">The sequence to exclude from the resulting sequence.</param>
        /// <returns>A new sequence containing the source sequence minus any elements present in the sequence of elements to be excluded.</returns>
        public IEnumerable<TSource> Exclude(IEnumerable<TSource> exclude)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(exclude);
        
            return (from item in source where !exclude.Contains(item)
                select item);
        }
        
        /// <summary>
        /// Excludes items from a sequence that match a predicate condition.
        /// </summary>
        /// <param name="predicate">The predicate to use to determine whether to exclude each item or not.</param>
        /// <returns>A new sequence containing the source sequence minus any elements that matched the predicate condition.</returns>
        public IEnumerable<TSource> Exclude(Func<TSource, bool> predicate)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(predicate);
        
            return (from item in source where !predicate(item)
                select item);
        }
    }
}