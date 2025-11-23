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

using AlastairLundy.EnhancedLinq.Deferred.Enumerators.Ranges;

namespace AlastairLundy.EnhancedLinq.Deferred.Ranges;

public static partial class EnhancedLinqDeferredRange
{
    /// <param name="source">The sequence to insert items into.</param>
    /// <typeparam name="TSource">The type of elements stored in the sequence.</typeparam>
    extension<TSource>(IEnumerable<TSource> source)
    {
        /// <summary>
        /// Inserts an element into a sequence at the specified index.
        /// </summary>
        /// <param name="indexToInsertAt">The index at which to insert the element into the sequence</param>
        /// <param name="toBeInserted">The element to be inserted.</param>
        /// <returns>A new sequence with the elements of the original sequence, and the specified element inserted at the specified index. </returns>
        public IEnumerable<TSource> Insert(int indexToInsertAt,
            TSource toBeInserted)
            => InsertRange(source, indexToInsertAt, [toBeInserted]);
        
        /// <summary>
        /// Inserts a sequence of elements into a sequence at a specified index.
        /// </summary>
        /// <param name="indexToInsertAt">The index at which to insert the elements into the sequence</param>
        /// <param name="toBeInserted">The sequence of elements to be inserted.</param>
        /// <typeparam name="TSource">The type of elements stored in the sequence.</typeparam>
        /// <returns>A new sequence with the elements of the original sequence, and the elements of the second sequence inserted at the specified index. </returns>
        public IEnumerable<TSource> InsertRange(int indexToInsertAt, IEnumerable<TSource> toBeInserted)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(toBeInserted);
            ArgumentOutOfRangeException.ThrowIfNegative(indexToInsertAt);
            
            return new Internals.Infra.CustomEnumeratorEnumerable<TSource>(
                new InsertRangeEnumerator<TSource>(source, indexToInsertAt, toBeInserted));
        }
    }
    

}