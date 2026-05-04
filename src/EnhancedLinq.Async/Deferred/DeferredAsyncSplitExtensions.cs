/*
    EnhancedLinq.Async
    Copyright (c) 2025-2026 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/.
*/

using EnhancedLinq.Async.Deferred.Enumerators.SplitBy;

namespace EnhancedLinq.Async.Deferred;

/// <summary>
/// Provides extension methods for performing asynchronous splits with deferred execution.
/// </summary>
public static class DeferredAsyncSplitExtensions
{
    /// <param name="source">The sequence to split.</param>
    /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
    extension<TSource>(IAsyncEnumerable<TSource> source)
        where TSource : notnull
    {
        /// <summary>Asynchronously splits the source into inner sequences, yielding each chunk according to item‑ count-based counting logic.</summary>
        /// <returns>A deferred asynchronous collection that yields inner collections of items grouped by their count up to the specified maximum.</returns>
        /// <param name="maximumCount">The maximum number of items allowed in each inner sequence; must be positive.</param>
        /// <exception cref="ArgumentNullException">Thrown if the source is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the maximumCount is non‑positive.</exception>
        public IAsyncEnumerable<IAsyncEnumerable<TSource>> SplitByItemCount(int maximumCount)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(maximumCount);

            return new CustomAsyncEnumerable<IAsyncEnumerable<TSource>>(
                new SplitByItemCountAsyncEnumerator<TSource>(source, maximumCount));
        }

        /// <summary>Asynchronously splits the source into inner sequences, yielding each chunk according to processor‑based counting logic.</summary>
        /// <returns>A deferred asynchronous collection that yields inner collections of items grouped by processor count.</returns>
        public IAsyncEnumerable<IAsyncEnumerable<TSource>> SplitByProcessorCount()
        {
            ArgumentNullException.ThrowIfNull(source);

            return new CustomAsyncEnumerable<IAsyncEnumerable<TSource>>(
                new SplitByEnumerableCountAsyncEnumerator<TSource>(source, Environment.ProcessorCount));
        }

        /// <summary>Returns an asynchronous split of the sequence based on whether each element equals the specified separator.</summary>
        /// <param name="separator">The value to compare against each element in the source sequence.</param>
        /// <returns>A deferred asynchronous collection where each inner sequence contains elements equal to the separator, yielded lazily.</returns>
        public IAsyncEnumerable<IAsyncEnumerable<TSource>> SplitBy(TSource separator)
        {
            ArgumentNullException.ThrowIfNull(source);

            return source.SplitBy(x => x.Equals(separator));
        }

        /// <summary>Asynchronously splits the source into inner sequences, grouping consecutive elements based on the outcome of the provided predicate.</summary>
        /// <returns>A deferred asynchronous collection that yields inner collections of items grouped according to whether each element satisfies the predicate.</returns>
        /// <param name="predicate">A function that evaluates each element to determine grouping behavior; must not be null.</param>
        /// <exception cref="ArgumentNullException">Thrown if the source is null.</exception>
        /// <exception cref="ArgumentNullException">Thrown if predicate is null.</exception>
        public IAsyncEnumerable<IAsyncEnumerable<TSource>> SplitBy(Func<TSource, bool> predicate)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(predicate);

            return new CustomAsyncEnumerable<IAsyncEnumerable<TSource>>(
                new SplitByPredicateAsyncEnumerator<TSource>(source, predicate));
        }
    }
}