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

#if NET8_0_OR_GREATER
using System;
#endif
using System.Collections.Generic;

using AlastairLundy.EnhancedLinq.Deferred.Enumerables;

namespace AlastairLundy.EnhancedLinq.Deferred.Ranges;

/// <summary>
/// This static partial class contains Deferred Execution Range related extension methods (such as <see cref="AppendRange{TSource}"/>
/// or <see cref="RemoveRange{TSource}"/>)
/// </summary>
public static partial class EnhancedLinqDeferredRange
{
    
    /// <summary>
    /// Appends one sequence of elements to the end of another specified sequence.
    /// </summary>
    /// <param name="source">The sequence to append items to.</param>
    /// <param name="toBeAppended">The elements to append to the sequence.</param>
    /// <typeparam name="TSource">The type of element in the sequence and elements being appended.</typeparam>
    /// <returns>A new sequence made up of the source sequence and the appended sequence.</returns>
    public static IEnumerable<TSource> AppendRange<TSource>(this IEnumerable<TSource> source,
        IEnumerable<TSource> toBeAppended)
    { 
#if NET8_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(source, nameof(source));
        ArgumentNullException.ThrowIfNull(toBeAppended, nameof(toBeAppended));
#endif
        
        return new AppendRangeEnumerable<TSource>(source, toBeAppended);
    }
}