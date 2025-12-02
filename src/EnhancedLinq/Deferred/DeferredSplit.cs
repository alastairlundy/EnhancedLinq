/*
    EnhancedLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
    */

using System;
using System.Collections.Generic;
using EnhancedLinq.Deferred.Enumerators;

namespace EnhancedLinq.Deferred;

public static partial class EnhancedLinqDeferred
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="source">The sequence to split.</param>
    /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
    extension<TSource>(IEnumerable<TSource> source)
        where TSource : notnull
    {
        /// <summary>
        /// Splits the source sequence into multiple subsequences, each containing up to a specified maximum number of elements.
        /// </summary>
        /// <param name="maximumCount">The maximum number of elements in each subsequence. Must be greater than zero.</param>
        /// <returns>A sequence of sequences, each containing up to <paramref name="maximumCount"/> elements from the source sequence.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the source sequence is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="maximumCount"/> is less than or equal to zero.</exception>
        public IEnumerable<IEnumerable<TSource>> SplitByItemCount(int maximumCount)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(maximumCount);

            return new Internals.Infra.CustomEnumeratorEnumerable<IEnumerable<TSource>>(
                new SplitByItemCountEnumerator<TSource>(source, maximumCount));
        }

        /// <summary>
        /// Splits the source sequence into a number of subsequences equal to the number of available logical processors.
        /// </summary>
        /// <returns>A sequence of sequences, where the number of subsequences equals the number of logical processors on the current machine.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the source sequence is null.</exception>
        public IEnumerable<IEnumerable<TSource>> SplitByProcessorCount()
        {
            ArgumentNullException.ThrowIfNull(source);

            return new Internals.Infra.CustomEnumeratorEnumerable<IEnumerable<TSource>>(
                new SplitByEnumerableCountEnumerator<TSource>(source, Environment.ProcessorCount));
        }

        /// <summary>
        /// Splits a sequence by a separator, into a sequence of sequences.
        /// </summary>
        /// <param name="separator">The separator to split by.</param>
        /// <returns>A sequence of sequences, each containing the elements before the separator was found.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the source sequence is null.</exception>
        public IEnumerable<IEnumerable<TSource>> SplitBy(TSource separator)
        {
            ArgumentNullException.ThrowIfNull(source);

            return SplitBy(source, x => x.Equals(separator));
        }

        /// <summary>
        /// Splits a sequence when a predicate evaluates to true.
        /// </summary>
        /// <param name="predicate">The predicate to split the sequence on when it evaluates to true.</param>
        /// <returns>A sequence of sequences, each containing the elements before the predicate evaluated to true.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the source sequence is null.</exception>
        public IEnumerable<IEnumerable<TSource>> SplitBy(Func<TSource, bool> predicate)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(predicate);

            return new Internals.Infra.CustomEnumeratorEnumerable<IEnumerable<TSource>>(
                new SplitByPredicateEnumerator<TSource>(source, predicate));
        }
    }
}