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

using AlastairLundy.EnhancedLinq.Deferred.Enumerators.Indices;

namespace AlastairLundy.EnhancedLinq.Deferred;

public static partial class EnhancedLinqDeferred
{
    /// <summary>
    /// Gets the first <paramref name="count"/> indices of the specified item within a sequence.
    /// </summary>
    /// <param name="source">The sequence to be searched.</param>
    /// <param name="target">The item to search for.</param>
    /// <param name="count">The maximum number of indices to return.</param>
    /// <typeparam name="T">The type of the elements in the source sequence.</typeparam>
    /// <returns>A sequence of the first <paramref name="count"/> indices if the object is found;
    /// an empty sequence otherwise.</returns>
    public static IEnumerable<int> FirstNIndicesOf<T>(this IEnumerable<T> source, T target, int count) where T : notnull
    {
#if NET8_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(source);
#endif
        
        return new Internals.Infra.CustomEnumeratorEnumerable<int>(new IndicesEnumerator<T>(source,
                x => x.Equals(target)))
            .Take(count);
    }

    /// <summary>
    /// Gets the first <paramref name="count"/> indices of the elements that match the predicate
    /// within a sequence.
    /// </summary>
    /// <param name="source">The sequence to be searched.</param>
    /// <param name="predicate">The predicate to use when comparing elements in the source.</param>
    /// <param name="count">The maximum number of indices to return.</param>
    /// <typeparam name="T">The type of the elements in the source sequence.</typeparam>
    /// <returns>A sequence of the first <paramref name="count"/> indices if one or more elements
    /// matching the predicate is found; an empty sequence otherwise.</returns>
    public static IEnumerable<int> FirstNIndicesOf<T>(this IEnumerable<T> source, Func<T, bool> predicate, int count)
    {
#if NET8_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(source);
#endif
        
        return new Internals.Infra.CustomEnumeratorEnumerable<int>(new IndicesEnumerator<T>(source, predicate))
            .Take(count);
    }

    /// <summary>
    /// Finds the first <paramref name="count"/> occurrences of a specified char within a string,
    /// starting from the beginning of the string.
    /// </summary>
    /// <param name="str">The input string.</param>
    /// <param name="c">The character to find in the string.</param>
    /// <param name="count">The maximum number of indices to return.</param>
    /// <returns>
    /// A sequence of the first <paramref name="count"/> indices where the character is found;
    /// an empty sequence if the character could not be found.
    /// </returns>
    public static IEnumerable<int> FirstNIndicesOf(this string str, char c, int count)
    {
#if NET8_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(str);
#endif
        
        return new Internals.Infra.CustomEnumeratorEnumerable<int>(new IndicesEnumerator<char>(str, 
                x => x.Equals(c)))
            .Take(count);
    }

    /// <summary>
    /// Finds the first <paramref name="count"/> occurrences of a specified substring within a string,
    /// starting from the beginning of the string.
    /// </summary>
    /// <param name="str">The input string.</param>
    /// <param name="substring">The substring to look for.</param>
    /// <param name="count">The maximum number of indices to return.</param>
    /// <returns>A sequence of the first <paramref name="count"/> indices where the character is found;
    /// an empty sequence if the character could not be found.</returns>
    public static IEnumerable<int> FirstNIndicesOf(this string str, string substring, int count)
    {
#if NET8_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(str);
        ArgumentException.ThrowIfNullOrEmpty(substring);
#endif
        
        return new Internals.Infra.CustomEnumeratorEnumerable<int>(new StringIndicesEnumerator(str, substring))
            .Take(count);
    }
    
    /// <summary>
    /// Gets the last <paramref name="count"/> indices of the specified item within a sequence.
    /// </summary>
    /// <param name="source">The sequence to be searched.</param>
    /// <param name="target">The item to search for.</param>
    /// <param name="count">The maximum number of indices to return.</param>
    /// <typeparam name="T">The type of the elements in the source sequence.</typeparam>
    /// <returns>A sequence of the last <paramref name="count"/> indices if the object is found;
    /// an empty sequence otherwise.</returns>
    public static IEnumerable<int> LastNIndicesOf<T>(this IEnumerable<T> source, T target, int count) where T : notnull
       => FirstNIndicesOf(source.Reverse(), target, count);
       
    /// <summary>
    ///  Gets the last <paramref name="count"/> indices of the elements that match the predicate within a sequence.
    /// </summary>
    /// <param name="source">The sequence to be searched.</param>
    /// <param name="selector">The selector to use when comparing elements in the source.</param>
    /// <param name="count">The maximum number of indices to return.</param>
    /// <typeparam name="T">The type of the elements in the source sequence.</typeparam>
    /// <returns>A sequence of the last <paramref name="count"/> indices if one or more elements
    /// matching the predicate is found; an empty sequence otherwise.</returns>
    public static IEnumerable<int> LastNIndicesOf<T>(this IEnumerable<T> source, 
        Func<T, bool> selector, int count) 
        => FirstNIndicesOf(source.Reverse(), selector, count);

    /// <param name="str">The input string.</param>
    extension(string str)
    {
        /// <summary>
        /// Finds the last <paramref name="count"/> occurrences of a specified char within a string,
        /// starting from the beginning of the string.
        /// </summary>
        /// <param name="c">The character to find in the string.</param>
        /// <param name="count">The maximum number of indices to return.</param>
        /// <returns>
        /// A sequence of the last <paramref name="count"/> indices where the character is found;
        /// an empty sequence if the character could not be found.
        /// </returns>
        public IEnumerable<int> LastNIndicesOf(char c, int count)
            => FirstNIndicesOf(str.Reverse(), c, count);

        /// <summary>
        /// Finds the last <paramref name="count"/> occurrences of a specified substring within a string, starting from the beginning of the string.
        /// </summary>
        /// <param name="substring">The substring to look for.</param>
        /// <param name="count">The maximum number of indices to return.</param>
        /// <returns>A sequence of the last <paramref name="count"/> indices where the character is found;
        /// an empty sequence if the character could not be found.</returns>
        public IEnumerable<int> LastNIndicesOf(string substring, int count)
            => FirstNIndicesOf(string.Join("", str.Reverse()), substring, count);
    }
}