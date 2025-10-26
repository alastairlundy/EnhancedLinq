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

using AlastairLundy.DotPrimitives.Collections.Enumerables;

using AlastairLundy.EnhancedLinq.Deferred.Enumerators;

namespace AlastairLundy.EnhancedLinq.Deferred;

public static partial class EnhancedLinqDeferred
{
    /// <summary>
    /// Returns a sequence of duplicate elements from the source sequence using the default equality comparer.
    /// </summary>
    /// <typeparam name="TSource">The type of elements in the source sequence.</typeparam>
    /// <param name="source">The sequence to find duplicates in.</param>
    /// <returns>A sequence that contains only duplicate elements from the source sequence.</returns>
    public static IEnumerable<TSource> GetDuplicates<TSource>(this IEnumerable<TSource> source)
        where TSource : IEquatable<TSource>
        => GetDuplicates(source, EqualityComparer<TSource>.Default);

    /// <summary>
    /// Returns a sequence of duplicate elements from the source sequence using the specified equality comparer.
    /// </summary>
    /// <typeparam name="TSource">The type of elements in the source sequence.</typeparam>
    /// <param name="source">The sequence to find duplicates in.</param>
    /// <param name="comparer">The equality comparer to use for determining duplicates.</param>
    /// <returns>A sequence that contains only duplicate elements from the source sequence.</returns>
    public static IEnumerable<TSource> GetDuplicates<TSource>(this IEnumerable<TSource> source,
        IEqualityComparer<TSource> comparer)
    {
#if NET8_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(source, nameof(source));
#endif
        
        return new CustomEnumeratorEnumerable<TSource>(
            new DuplicatesEnumerator<TSource>(source, comparer));
    }
}