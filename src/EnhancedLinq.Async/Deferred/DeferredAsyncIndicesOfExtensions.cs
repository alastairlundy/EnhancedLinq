/*
    EnhancedLinq.Async
    Copyright (c) 2025-2026 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/.
*/

using EnhancedLinq.Async.Deferred.Enumerators;

namespace EnhancedLinq.Async.Deferred;

/// <summary>
/// Provides a set of extension methods for asynchronously retrieving the indices
/// of elements in a sequence that match specified conditions.
/// </summary>
public static class DeferredAsyncIndicesOfExtensions
{
    extension<TSource>(IAsyncEnumerable<TSource> source)
    {
        /// <summary>
        /// Returns an asynchronous sequence of indices where the elements in the source async sequence match the specified target value.
        /// </summary>
        /// <param name="target">The target value to search for in the source async sequence.</param>
        /// <returns>An asynchronous sequence of indices where the elements in the source async sequence match the specified target value.</returns>
        public IAsyncEnumerable<int> IndicesOfAsync(TSource target)
        {
            ArgumentNullException.ThrowIfNull(source);
            
            return new CustomAsyncEnumerable<int>(
                new GenericIndicesAsyncEnumerator<TSource>(source, x => x is not null && x.Equals(target)));
        }

        /// <summary>
        /// Returns an asynchronous sequence of indices where the elements in the source async sequence match the specified predicate.
        /// </summary>
        /// <param name="selector">A function to test each element for a condition.</param>
        /// <returns>An asynchronous sequence of indices where the elements in the source async sequence match the specified predicate.</returns>
        public IAsyncEnumerable<int> IndicesOfAsync(Func<TSource, bool> selector)
        {
            ArgumentNullException.ThrowIfNull(source);
            
            return new CustomAsyncEnumerable<int>(
                new GenericIndicesAsyncEnumerator<TSource>(source, selector));
        }
    }
}