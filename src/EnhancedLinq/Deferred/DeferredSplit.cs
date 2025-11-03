/*
      EnhancedLinq 
      Copyright (c) 2025 Alastair Lundy
      
     Licensed under the Apache License, Version 2.0 (the "License");
     you may not use this file except in compliance with the License.
     You may obtain a copy of the License at

         http://www.apache.org/licenses/LICENSE-2.0

     Unless required by applicable law or agreed to in writing, software
     distributed under the License is distributed on an "AS IS" BASIS,
     WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
     See the License for the specific language governing permissions and
     limitations under the License.
 */

using System;
using System.Collections.Generic;

using AlastairLundy.EnhancedLinq.Deferred.Enumerators;

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
    public static IEnumerable<IEnumerable<TSource>> SplitByItemCount<TSource>(this IEnumerable<TSource> source, int maximumCount)
    {
#if NET8_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(source, nameof(source));
#endif
        
        if(maximumCount <= 0)
            throw new ArgumentOutOfRangeException(nameof(maximumCount));

        return new Internals.Infra.CustomEnumeratorEnumerable<IEnumerable<TSource>>(
            new SplitByItemCountEnumerator<TSource>(source, maximumCount));
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
#if NET8_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(source, nameof(source));
#endif
        
        if(source == null)
            throw new ArgumentNullException(nameof(source));
        
        return new Internals.Infra.CustomEnumeratorEnumerable<IEnumerable<TSource>>(
            new SplitByEnumerableCountEnumerator<TSource>(source, Environment.ProcessorCount)); 
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
#if NET8_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(source, nameof(source));
#endif
        
        if(source == null)
            throw new ArgumentNullException(nameof(source));
        
        return SplitBy(source, x => x is not null && x.Equals(separator));
    }
    
    /// <summary>
    /// Splits a sequence when a predicate evaluates to true.
    /// </summary>
    /// <param name="source">The sequence to split.</param>
    /// <param name="predicate">The predicate to split the sequence on when it evaluates to true.</param>
    /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
    /// <returns>A sequence of sequences, each containing the elements before the predicate evaluated to true.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="source"/> is null.</exception>
    public static IEnumerable<IEnumerable<TSource>> SplitBy<TSource>(this IEnumerable<TSource> source,
        Func<TSource, bool> predicate)
    {
#if NET8_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(source, nameof(source));
#endif
        
        if(source == null)
            throw new ArgumentNullException(nameof(source));

        return new Internals.Infra.CustomEnumeratorEnumerable<IEnumerable<TSource>>(
            new SplitByPredicateEnumerator<TSource>(source, predicate));
    }
}