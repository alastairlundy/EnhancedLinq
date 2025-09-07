/*
    EnhancedLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/.
 */

using System;
using System.Collections.Generic;

using AlastairLundy.EnhancedLinq.Deferred.Enumerables;

namespace AlastairLundy.EnhancedLinq.Deferred;

public static partial class EnhancedLinqDeferred
{
    
    /// <summary>
    /// Splits the source sequence into multiple subsequences, each containing up to a specified maximum number of elements.
    /// </summary>
    /// <param name="source">The sequence to split.</param>
    /// <param name="maximumCount">The maximum number of elements in each subsequence. Must be greater than zero.</param>
    /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
    /// <returns>A sequence of sequences, each containing up to <paramref name="maximumCount"/> elements from the source sequence.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="source"/> is null.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="maximumCount"/> is less than or equal to zero.</exception>
    public static IEnumerable<IEnumerable<TSource>> SplitByCount<TSource>(this IEnumerable<TSource> source, int maximumCount)
    {
        ArgumentNullException.ThrowIfNull(source, nameof(source));
        
        if(maximumCount <= 0)
            throw new ArgumentOutOfRangeException(nameof(maximumCount));

        return new SplitByItemCountEnumerable<TSource>(source, maximumItemCount: maximumCount);
    }

    /// <summary>
    /// Splits the source sequence into a number of subsequences equal to the number of available logical processors.
    /// </summary>
    /// <param name="source">The sequence to split.</param>
    /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
    /// <returns>A sequence of sequences, where the number of subsequences equals the number of logical processors on the current machine.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="source"/> is null.</exception>
    public static IEnumerable<IEnumerable<TSource>> SplitByProcessorCount<TSource>(this IEnumerable<TSource> source)
    {
        ArgumentNullException.ThrowIfNull(source, nameof(source));

        if(source == null)
            throw new ArgumentNullException(nameof(source));
        
        return new SplitByEnumerableCountEnumerable<TSource>(source, Environment.ProcessorCount);
    }

    /// <summary>
    /// Splits a sequence by a separator, into a sequence of sequences.
    /// </summary>
    /// <param name="source">The sequence to split.</param>
    /// <param name="separator">The separator to split by.</param>
    /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
    /// <returns>A sequence of sequences, each containing the elements before the separator was found.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="source"/> is null.</exception>
    public static IEnumerable<IEnumerable<TSource>> SplitBy<TSource>(this IEnumerable<TSource> source,
        TSource separator)
    {
        ArgumentNullException.ThrowIfNull(source, nameof(source));

        if(source == null)
            throw new ArgumentNullException(nameof(source));
        
        return new SplitBySeparatorEnumerable<TSource>(source, separator);
    }
}