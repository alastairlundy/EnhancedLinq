/*
    EnhancedLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using AlastairLundy.DotPrimitives.Collections.Enumerables;
using AlastairLundy.EnhancedLinq.Deferred.Enumerables;
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
    /// <returns>A sequence of the first <paramref name="count"/> indices if the object is found; an empty sequence otherwise.</returns>
    public static IEnumerable<int> FirstNIndicesOf<T>(this IEnumerable<T> source, T target, int count) where T : notnull
    {
        ArgumentNullException.ThrowIfNull(source);
        
        return new CustomEnumeratorEnumerable<int>(new IndicesEnumerator<T>(source, x => x.Equals(target)))
            .Take(count);
    }

    /// <summary>
    /// Gets the first <paramref name="count"/> indices of the elements that match the predicate within a sequence.
    /// </summary>
    /// <param name="source">The sequence to be searched.</param>
    /// <param name="predicate">The predicate to use when comparing elements in the source.</param>
    /// <param name="count">The maximum number of indices to return.</param>
    /// <typeparam name="T">The type of the elements in the source sequence.</typeparam>
    /// <returns>A sequence of the first <paramref name="count"/> indices if one or more elements matching the predicate is found; an empty sequence otherwise.</returns>
    public static IEnumerable<int> FirstNIndicesOf<T>(this IEnumerable<T> source, Func<T, bool> predicate, int count)
    {
        ArgumentNullException.ThrowIfNull(source);
        
        return new CustomEnumeratorEnumerable<int>(new IndicesEnumerator<T>(source, predicate))
            .Take(count);
    }

    /// <summary>
    /// Finds the first <paramref name="count"/> occurrences of a specified char within a string, starting from the beginning of the string.
    /// </summary>
    /// <param name="str">The input string.</param>
    /// <param name="c">The character to find in the string.</param>
    /// <param name="count">The maximum number of indices to return.</param>
    /// <returns>
    /// A sequence of the first <paramref name="count"/> indices where the character is found; an empty sequence if the character could not be found.
    /// </returns>
    public static IEnumerable<int> FirstNIndicesOf(this string str, char c, int count)
    {
        ArgumentNullException.ThrowIfNull(str);
        
        return new CustomEnumeratorEnumerable<int>(new IndicesEnumerator<char>(str, x => x.Equals(c)))
            .Take(count);
    }

    /// <summary>
    /// Finds the first <paramref name="count"/> occurrences of a specified substring within a string, starting from the beginning of the string.
    /// </summary>
    /// <param name="str">The input string.</param>
    /// <param name="substring">The substring to look for.</param>
    /// <param name="count">The maximum number of indices to return.</param>
    /// <returns>A sequence of the first <paramref name="count"/> indices where the character is found; an empty sequence if the character could not be found.</returns>
    public static IEnumerable<int> FirstNIndicesOf(this string str, string substring, int count)
    {
        ArgumentException.ThrowIfNullOrEmpty(str);
        ArgumentException.ThrowIfNullOrEmpty(substring);
        
        return new CustomEnumeratorEnumerable<int>(new StringIndicesEnumerator(str, substring))
            .Take(count);
    }
    
    /// <summary>
    /// Gets the last <paramref name="count"/> indices of the specified item within a sequence.
    /// </summary>
    /// <param name="source">The sequence to be searched.</param>
    /// <param name="target">The item to search for.</param>
    /// <param name="count">The maximum number of indices to return.</param>
    /// <typeparam name="T">The type of the elements in the source sequence.</typeparam>
    /// <returns>A sequence of the last <paramref name="count"/> indices if the object is found; an empty sequence otherwise.</returns>
    public static IEnumerable<int> LastNIndicesOf<T>(this IEnumerable<T> source, T target, int count) where T : notnull
       => FirstNIndicesOf(source.Reverse(), target, count);
       
    /// <summary>
    ///  Gets the last <paramref name="count"/> indices of the elements that match the predicate within a sequence.
    /// </summary>
    /// <param name="source">The sequence to be searched.</param>
    /// <param name="selector">The selector to use when comparing elements in the source.</param>
    /// <param name="count">The maximum number of indices to return.</param>
    /// <typeparam name="T">The type of the elements in the source sequence.</typeparam>
    /// <returns>A sequence of the last <paramref name="count"/> indices if one or more elements matching the predicate is found; an empty sequence otherwise.</returns>
    public static IEnumerable<int> LastNIndicesOf<T>(this IEnumerable<T> source, Func<T, bool> selector, int count) 
        => FirstNIndicesOf(source.Reverse(), selector, count);

    /// <summary>
    /// Finds the last <paramref name="count"/> occurrences of a specified char within a string, starting from the beginning of the string.
    /// </summary>
    /// <param name="str">The input string.</param>
    /// <param name="c">The character to find in the string.</param>
    /// <param name="count">The maximum number of indices to return.</param>
    /// <returns>
    /// A sequence of the last <paramref name="count"/> indices where the character is found; an empty sequence if the character could not be found.
    /// </returns>
    public static IEnumerable<int> LastNIndicesOf(this string str, char c, int count)
        => FirstNIndicesOf(str.Reverse(), c, count);

    /// <summary>
    /// Finds the last <paramref name="count"/> occurrences of a specified substring within a string, starting from the beginning of the string.
    /// </summary>
    /// <param name="str">The input string.</param>
    /// <param name="substring">The substring to look for.</param>
    /// <param name="count">The maximum number of indices to return.</param>
    /// <returns>A sequence of the last <paramref name="count"/> indices where the character is found; an empty sequence if the character could not be found.</returns>
    public static IEnumerable<int> LastNIndicesOf(this string str, string substring, int count)
        => FirstNIndicesOf(string.Join("", str.Reverse()), substring, count);

}