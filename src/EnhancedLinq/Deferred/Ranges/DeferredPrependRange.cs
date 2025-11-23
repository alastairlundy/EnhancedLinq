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
using AlastairLundy.EnhancedLinq.Deferred.Enumerables;

namespace AlastairLundy.EnhancedLinq.Deferred.Ranges;

public static partial class EnhancedLinqDeferredRange
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="source">The sequence to prepend items to.</param>
    /// <typeparam name="TSource">The type of element in the sequence and elements being prepended.</typeparam>
    extension<TSource>(IEnumerable<TSource> source)
    {
        /// <summary>
        /// Prepends one sequence of elements to another specified sequence.
        /// </summary>
        /// <param name="toBePrepended">The elements to prepended to the sequence.</param>
        /// <returns>A new sequence made up of the prepended sequence and the source sequence.</returns>
        public IEnumerable<TSource> PrependRange(IEnumerable<TSource> toBePrepended)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(toBePrepended);
        
            return new PrependRangeEnumerable<TSource>(source, toBePrepended);
        }
    }
}