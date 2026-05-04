/*
    EnhancedLinq 
    Copyright (c) 2025-2026 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
    */

using System.Linq;

namespace EnhancedLinq.Async.Deferred;

/// <summary>
/// Provides extension methods for deferred exclusion operations on asynchronous sequences.
/// </summary>
public static class DeferredExcludesExtensions
{
    /// <param name="source">The sequence to be searched.</param>
    /// <typeparam name="TSource">The type of elements in the source asynchronous sequence.</typeparam>
    extension<TSource>(IAsyncEnumerable<TSource> source) where TSource : notnull
    {
        /// <summary>
        /// Excludes items in one asynchronous sequence from another asynchronous sequence.
        /// </summary>
        /// <param name="exclude">The asynchronous sequence to exclude from the resulting asynchronous sequence.</param>
        /// <returns>A new asynchronous sequence containing the source asynchronous sequence minus any elements present in the asynchronous sequence of elements to be excluded.</returns>
        public IAsyncEnumerable<TSource> Exclude(IAsyncEnumerable<TSource> exclude)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(exclude);

            return source.WhereAsync(async item => await exclude.ContainsAsync(item).ConfigureAwait(false));
        }
        
        /// <summary>
        /// Excludes items from an asynchronous sequence that match a predicate condition.
        /// </summary>
        /// <param name="predicate">The predicate to use to determine whether to exclude each item or not.</param>
        /// <returns>A new asynchronous sequence containing the source asynchronous sequence minus any elements that matched the predicate condition.</returns>
        public IAsyncEnumerable<TSource> Exclude(Func<TSource, bool> predicate)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(predicate);
        
            return (from item in source where !predicate(item)
                select item);
        }
    }
}