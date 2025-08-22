/*
    EnhancedLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/.
 */

using System;
using System.Collections.Generic;
using AlastairLundy.EnhancedLinq.Deferred.Enumerables;

namespace AlastairLundy.EnhancedLinq.Deferred;

public static partial class EnhancedLinqDeferred
{
    /// <summary>
    /// Gets all the indices of the specified item within a sequence.
    /// </summary>
    /// <param name="source">The sequence to be searched.</param>
    /// <param name="target">The item to search for.</param>
    /// <typeparam name="T"></typeparam>
    /// <returns>The indices if the object is found; an empty sequence otherwise.</returns>
    public static IEnumerable<int> IndicesOf<T>(this IEnumerable<T> source, T target) where T : notnull
    {
        ArgumentNullException.ThrowIfNull(source);
        
        return new IndicesEnumerable<T>(source, x => x.Equals(target));
    }

    /// <summary>
    /// Gets all the indices of the elements that match the predicate within a sequence.
    /// </summary>
    /// <param name="source">The sequence to be searched.</param>
    /// <param name="selector"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns>The indices if one or more elements matching the predicate is found; an empty sequence otherwise.</returns>
    public static IEnumerable<int> IndicesOf<T>(this IEnumerable<T> source, Func<T, bool> selector)
    {
        ArgumentNullException.ThrowIfNull(source);
        
        return new IndicesEnumerable<T>(source, selector);
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
        ArgumentNullException.ThrowIfNull(str);
        
        return new IndicesEnumerable<char>(str, x => x.Equals(c));
    }
    
    /// <summary>
    /// Finds all occurrences of a specified substring within a string, starting from the beginning of the string.
    /// </summary>
    /// <param name="str">The input string.</param>
    /// <param name="substring">The substring to look for.</param>
    /// <returns>A sequence of indices where the character is found; an empty sequence if the character could not be found.</returns>
    public static IEnumerable<int> IndicesOf(this string str, string substring)
    {
        ArgumentException.ThrowIfNullOrEmpty(str);
        ArgumentException.ThrowIfNullOrEmpty(substring);
        
        return new StringIndicesEnumerable(str, substring);
    }

}