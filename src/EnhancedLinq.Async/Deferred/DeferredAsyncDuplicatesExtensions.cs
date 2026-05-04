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
/// Provides extension methods for processing asynchronous sequences
/// to identify and manage duplicate elements in a deferred manner.
/// </summary>
public static class DeferredAsyncDuplicatesExtensions
{
    /// <param name="source">The sequence to find duplicates in.</param>
    /// <typeparam name="TSource">The type of elements in the source sequence.</typeparam>
    extension<TSource>(IAsyncEnumerable<TSource> source) where TSource : IEquatable<TSource>
    {
        /// <summary>
        /// Identifies duplicate elements in an asynchronous sequence and returns them
        /// as an asynchronous sequence.
        /// </summary>
        /// <returns> An asynchronous sequence containing duplicate elements from the source sequence. </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <paramref name="source"/> is <see langword="null"/>.</exception>
        public IAsyncEnumerable<TSource> FindDuplicates() => source.FindDuplicates(EqualityComparer<TSource>.Default);

        /// <summary>
        /// Identifies duplicate elements in an asynchronous sequence and returns them
        /// as an asynchronous sequence, using the specified equality comparer.
        /// </summary>
        /// <param name="comparer">The equality comparer to use for comparing elements.</param>
        /// <returns>An asynchronous sequence containing duplicate elements from the source sequence.</returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <paramref name="source"/> or <paramref name="comparer"/> is <see langword="null"/>.
        /// </exception>
        public IAsyncEnumerable<TSource> FindDuplicates(IEqualityComparer<TSource> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(comparer);
        
            return new CustomAsyncEnumerable<TSource>(
                new AsyncDuplicatesEnumerator<TSource>(source, comparer));
        }
    }
}