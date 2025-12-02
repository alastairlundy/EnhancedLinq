/*
    EnhancedLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
    */

using System;
using System.Collections.Generic;
using EnhancedLinq.Deferred.Enumerators.Indices;

namespace EnhancedLinq.Deferred;

public static partial class EnhancedLinqDeferred
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="source">The sequence to be searched.</param>
    /// <typeparam name="T">The type of the elements in the source sequence.</typeparam>
    extension<T>(IEnumerable<T> source)
        where T : notnull
    {
        /// <summary>
        /// Gets all the indices of the specified item within a sequence.
        /// </summary>
        /// <param name="target">The item to search for.</param>
        /// <returns>The indices if the object is found; an empty sequence otherwise.</returns>
        public IEnumerable<int> IndicesOf(T target)
        {
            ArgumentNullException.ThrowIfNull(source);
        
            return new Internals.Infra.CustomEnumeratorEnumerable<int>(
                new IndicesEnumerator<T>(source, x => x.Equals(target)));
        }
        
        /// <summary>
        /// Gets all the indices of the elements that match the predicate within a sequence.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>The indices if one or more elements that matching the predicate are found; an empty sequence otherwise.</returns>
        public IEnumerable<int> IndicesOf(Func<T, bool> predicate)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(predicate);
        
            return new Internals.Infra.CustomEnumeratorEnumerable<int>(new IndicesEnumerator<T>(source, predicate));
        }
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="str">The input string.</param>
    extension(string str)
    {
        /// <summary>
        /// Finds all occurrences of a specified char within a string, starting from the beginning of the string.
        /// </summary>
        /// <param name="c">The character to find in the string.</param>
        /// <returns>
        /// A sequence of indices where the character is found; an empty sequence if the character could not be found.
        /// </returns>
        public IEnumerable<int> IndicesOf(char c)
        {
            ArgumentNullException.ThrowIfNull(str);
        
            return new Internals.Infra.CustomEnumeratorEnumerable<int>(
                new IndicesEnumerator<char>(str, x => x.Equals(c)));
        }
        
        /// <summary>
        /// Finds all occurrences of a specified substring within a string, starting from the beginning of the string.
        /// </summary>
        /// <param name="substring">The substring to look for.</param>
        /// <returns>A sequence of indices where the character is found; an empty sequence if the character could not be found.</returns>
        public IEnumerable<int> IndicesOf(string substring)
        {
            ArgumentException.ThrowIfNullOrEmpty(str);
            ArgumentException.ThrowIfNullOrEmpty(substring);
        
            return new Internals.Infra.CustomEnumeratorEnumerable<int>(new StringIndicesEnumerator(str, substring));
        }
    }
}