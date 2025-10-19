/*
    EnhancedLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/.
 */


using System;
using System.Collections.Generic;
using System.Linq;

namespace AlastairLundy.EnhancedLinq.Deferred;

public static partial class EnhancedLinqDeferred
{
    
    /// <summary>
    /// Excludes items in one sequence from another sequence.
    /// </summary>
    /// <param name="source">The sequence to be searched.</param>
    /// <param name="exclude">The sequence to exclude from the resulting sequence.</param>
    /// <typeparam name="TSource">The type of elements in the source sequence.</typeparam>
    /// <returns>A new sequence containing the source sequence minus any elements present in the sequence of elements to be excluded.</returns>
    public static IEnumerable<TSource> Exclude<TSource>(this IEnumerable<TSource> source,
        IEnumerable<TSource> exclude)
    {
#if NET8_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(source, nameof(source));
        ArgumentNullException.ThrowIfNull(exclude, nameof(exclude));
#endif
        
        return (from item in source where exclude.Contains(item) == false
            select item);
    }

    /// <summary>
    /// Excludes items from a sequence that match a predicate condition.
    /// </summary>
    /// <param name="source">The sequence to be searched.</param>
    /// <param name="predicate">The predicate to use to determine whether to exclude each item or not.</param>
    /// <typeparam name="TSource">The type of elements in the source sequence.</typeparam>
    /// <returns>A new sequence containing the source sequence minus any elements that matched the predicate condition.</returns>
    public static IEnumerable<TSource> Exclude<TSource>(this IEnumerable<TSource> source,
        Func<TSource, bool> predicate)
    {
#if NET8_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(source, nameof(source));
        ArgumentNullException.ThrowIfNull(predicate, nameof(predicate));
#endif
        
        return (from item in source where predicate(item) == false
            select item);
    }
}