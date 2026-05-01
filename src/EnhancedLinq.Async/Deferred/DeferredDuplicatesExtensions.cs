/*
    EnhancedLinq 
    Copyright (c) 2025-2026 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
    */

using EnhancedLinq.Async.Deferred.Enumerators;

namespace EnhancedLinq.Async.Deferred;

/// <summary>
/// Provides a set of extension methods aimed at identifying and processing duplicate elements in a sequence.
/// </summary>
public static class DeferredDuplicatesExtensions
{
    /// <param name="source">The sequence to find duplicates in.</param>
    /// <typeparam name="TSource">The type of elements in the source sequence.</typeparam>
    extension<TSource>(IAsyncEnumerable<TSource> source) where TSource : IEquatable<TSource>
    {
        /// <summary>
        /// Returns a sequence of duplicate elements from the source sequence using the default equality comparer.
        /// </summary>
        /// <returns>A sequence that contains only duplicate elements from the source sequence.</returns>
        public IAsyncEnumerable<TSource> FindDuplicates() => source.FindDuplicates(EqualityComparer<TSource>.Default);
        
        /// <summary>
        /// Returns a sequence of duplicate elements from the source sequence using the specified equality comparer.
        /// </summary>
        /// <param name="comparer">The equality comparer to use for determining duplicates.</param>
        /// <returns>A sequence that contains only duplicate elements from the source sequence.</returns>
        public IAsyncEnumerable<TSource> FindDuplicates(IEqualityComparer<TSource> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(comparer);
        
            return new CustomAsyncEnumerable<TSource>(
                new AsyncDuplicatesEnumerator<TSource>(source, comparer));
        }
    }
}