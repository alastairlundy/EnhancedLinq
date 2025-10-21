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

using AlastairLundy.EnhancedLinq.Deferred.Enumerators.Indices;

namespace AlastairLundy.EnhancedLinq.Deferred;

public static partial class EnhancedLinqDeferred
{
    /// <summary>
    /// Gets all the indices of the specified item within a sequence.
    /// </summary>
    /// <param name="source">The sequence to be searched.</param>
    /// <param name="target">The item to search for.</param>
    /// <typeparam name="T">The type of the elements in the source sequence.</typeparam>
    /// <returns>The indices if the object is found; an empty sequence otherwise.</returns>
    public static IEnumerable<int> IndicesOf<T>(this IEnumerable<T> source, T target) where T : notnull
    {
#if NET8_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(source);
#endif
        
        return new CustomEnumeratorEnumerable<int>(new IndicesEnumerator<T>(source, x => x.Equals(target)));
    }

    /// <summary>
    /// Gets all the indices of the elements that match the predicate within a sequence.
    /// </summary>
    /// <param name="source">The sequence to be searched.</param>
    /// <param name="predicate"></param>
    /// <typeparam name="T">The type of the elements in the source sequence.</typeparam>
    /// <returns>The indices if one or more elements matching the predicate is found; an empty sequence otherwise.</returns>
    public static IEnumerable<int> IndicesOf<T>(this IEnumerable<T> source, Func<T, bool> predicate)
    {
#if NET8_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(source);
#endif
        
        return new CustomEnumeratorEnumerable<int>(new IndicesEnumerator<T>(source, predicate));
    }
    
    /// <summary>
    /// Finds all occurrences of a specified char within a string, starting from the beginning of the string.
    /// </summary>
    /// <param name="str">The input string.</param>
    /// <param name="c">The character to find in the string.</param>
    /// <returns>
    /// A sequence of indices where the character is found; an empty sequence if the character could not be found.
    /// </returns>
    public static IEnumerable<int> IndicesOf(this string str, char c)
    {
#if NET8_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(str);
#endif
        
        return new CustomEnumeratorEnumerable<int>(new IndicesEnumerator<char>(str, x => x.Equals(c)));
    }
    
    /// <summary>
    /// Finds all occurrences of a specified substring within a string, starting from the beginning of the string.
    /// </summary>
    /// <param name="str">The input string.</param>
    /// <param name="substring">The substring to look for.</param>
    /// <returns>A sequence of indices where the character is found; an empty sequence if the character could not be found.</returns>
    public static IEnumerable<int> IndicesOf(this string str, string substring)
    {
#if NET8_0_OR_GREATER
        ArgumentException.ThrowIfNullOrEmpty(str);
        ArgumentException.ThrowIfNullOrEmpty(substring);
#endif
        
        return new CustomEnumeratorEnumerable<int>(new StringIndicesEnumerator(str, substring));
    }

}