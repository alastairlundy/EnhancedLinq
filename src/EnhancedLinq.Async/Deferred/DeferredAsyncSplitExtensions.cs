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
    /// <summary>
    /// 
    /// </summary>
    /// <param name="source">The sequence to split.</param>
    /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
    extension<TSource>(IAsyncEnumerable<TSource> source)
        where TSource : notnull
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="maximumCount"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public IAsyncEnumerable<IAsyncEnumerable<TSource>> SplitByItemCount(int maximumCount)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(maximumCount);

            return new CustomAsyncEnumerable<IAsyncEnumerable<TSource>>(
                new SplitByItemCountAsyncEnumerator<TSource>(source, maximumCount));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public IAsyncEnumerable<IAsyncEnumerable<TSource>> SplitByProcessorCount()
        {
            ArgumentNullException.ThrowIfNull(source);

            return new CustomAsyncEnumerable<IAsyncEnumerable<TSource>>(
                new SplitByEnumerableCountAsyncEnumerator<TSource>(source, Environment.ProcessorCount));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="separator"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public IAsyncEnumerable<IAsyncEnumerable<TSource>> SplitBy(TSource separator)
        {
            ArgumentNullException.ThrowIfNull(source);

            return source.SplitBy(x => x.Equals(separator));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public IAsyncEnumerable<IAsyncEnumerable<TSource>> SplitBy(Func<TSource, bool> predicate)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(predicate);

            return new CustomAsyncEnumerable<IAsyncEnumerable<TSource>>(
                new SplitByPredicateAsyncEnumerator<TSource>(source, predicate));
        }
    }
}