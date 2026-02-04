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
/// Provides deferred execution mode asynchronous operations using Async methods.
/// </summary>
public static partial class EnhancedLinqAsyncDeferred
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
        /// 
        /// </summary>
        /// <param name="selector"></param>
        /// <returns></returns>
        public IAsyncEnumerable<int> IndicesOfAsync(Func<TSource, bool> selector)
        {
            ArgumentNullException.ThrowIfNull(source);
            
            return new CustomAsyncEnumerable<int>(
                new GenericIndicesAsyncEnumerator<TSource>(source, selector));
        }
    }
}