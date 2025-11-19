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
    /// 
    /// </summary>
    /// <param name="source">The sequence to split.</param>
    /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
    extension<TSource>(IEnumerable<TSource> source)
    {
        /// <summary>
        /// Splits the source sequence into multiple subsequences, each containing up to a specified maximum number of elements.
        /// </summary>
        /// <param name="maximumCount">The maximum number of elements in each subsequence. Must be greater than zero.</param>
        /// <returns>A sequence of sequences, each containing up to <paramref name="maximumCount"/> elements from the source sequence.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="source"/> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="maximumCount"/> is less than or equal to zero.</exception>
        public IEnumerable<IEnumerable<TSource>> SplitByItemCount(int maximumCount)
        {
#if NET8_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(source);
#endif
        
            if(maximumCount <= 0)
                throw new ArgumentOutOfRangeException(nameof(maximumCount));

            return new Internals.Infra.CustomEnumeratorEnumerable<IEnumerable<TSource>>(
                new SplitByItemCountEnumerator<TSource>(source, maximumCount));
        }
        
        /// <summary>
        /// Splits the source sequence into a number of subsequences equal to the number of available logical processors.
        /// </summary>
        /// <returns>A sequence of sequences, where the number of subsequences equals the number of logical processors on the current machine.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="source"/> is null.</exception>
        public IEnumerable<IEnumerable<TSource>> SplitByProcessorCount()
        {
#if NET8_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(source);
#endif
        
            if(source == null)
                throw new ArgumentNullException(nameof(source));
        
            return new Internals.Infra.CustomEnumeratorEnumerable<IEnumerable<TSource>>(
                new SplitByEnumerableCountEnumerator<TSource>(source, Environment.ProcessorCount)); 
        }
        
        /// <summary>
        /// Splits a sequence by a separator, into a sequence of sequences.
        /// </summary>
        /// <param name="separator">The separator to split by.</param>
        /// <returns>A sequence of sequences, each containing the elements before the separator was found.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="source"/> is null.</exception>
        public IEnumerable<IEnumerable<TSource>> SplitBy(TSource separator)
        {
#if NET8_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(source);
#endif

            return source != null ? SplitBy(source, x => x is not null && x.Equals(separator)) :
                throw new ArgumentNullException(nameof(source));
        }
        
        /// <summary>
        /// Splits a sequence when a predicate evaluates to true.
        /// </summary>
        /// <param name="predicate">The predicate to split the sequence on when it evaluates to true.</param>
        /// <returns>A sequence of sequences, each containing the elements before the predicate evaluated to true.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="source"/> is null.</exception>
        public IEnumerable<IEnumerable<TSource>> SplitBy(Func<TSource, bool> predicate)
        {
#if NET8_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(source);
#endif
        
            if(source == null)
                throw new ArgumentNullException(nameof(source));

            return new Internals.Infra.CustomEnumeratorEnumerable<IEnumerable<TSource>>(
                new SplitByPredicateEnumerator<TSource>(source, predicate));
        }
    }
}