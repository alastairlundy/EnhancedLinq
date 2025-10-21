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
using System.Linq;

namespace AlastairLundy.EnhancedLinq.Deferred;

public static partial class EnhancedLinqDeferred
{
    
    /// <summary>
    /// Excludes items in one sequence from another sequence.
    /// </summary>
    /// <param name="source">The sequence to be searched.</param>
    /// <param name="exclude">The sequence to exclude from the resulting sequence.</param>
    /// <typeparam name="TSource">The type of elements in the source sequence.</typeparam>
    /// <returns>A new sequence containing the source sequence minus any elements present in the sequence of elements to be excluded.</returns>
    public static IEnumerable<TSource> Exclude<TSource>(this IEnumerable<TSource> source,
        IEnumerable<TSource> exclude)
    {
#if NET8_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(source, nameof(source));
        ArgumentNullException.ThrowIfNull(exclude, nameof(exclude));
#endif
        
        return (from item in source where exclude.Contains(item) == false
            select item);
    }

    /// <summary>
    /// Excludes items from a sequence that match a predicate condition.
    /// </summary>
    /// <param name="source">The sequence to be searched.</param>
    /// <param name="predicate">The predicate to use to determine whether to exclude each item or not.</param>
    /// <typeparam name="TSource">The type of elements in the source sequence.</typeparam>
    /// <returns>A new sequence containing the source sequence minus any elements that matched the predicate condition.</returns>
    public static IEnumerable<TSource> Exclude<TSource>(this IEnumerable<TSource> source,
        Func<TSource, bool> predicate)
    {
#if NET8_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(source, nameof(source));
        ArgumentNullException.ThrowIfNull(predicate, nameof(predicate));
#endif
        
        return (from item in source where predicate(item) == false
            select item);
    }
}