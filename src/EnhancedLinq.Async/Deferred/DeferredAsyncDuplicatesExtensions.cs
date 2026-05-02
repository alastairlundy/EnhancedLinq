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
/// 
/// </summary>
public static class DeferredAsyncDuplicatesExtensions
{
    /// <param name="source">The sequence to find duplicates in.</param>
    /// <typeparam name="TSource">The type of elements in the source sequence.</typeparam>
    extension<TSource>(IAsyncEnumerable<TSource> source) where TSource : IEquatable<TSource>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IAsyncEnumerable<TSource> FindDuplicates() => source.FindDuplicates(EqualityComparer<TSource>.Default);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="comparer"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public IAsyncEnumerable<TSource> FindDuplicates(IEqualityComparer<TSource> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(comparer);
        
            return new CustomAsyncEnumerable<TSource>(
                new AsyncDuplicatesEnumerator<TSource>(source, comparer));
        }
    }
}