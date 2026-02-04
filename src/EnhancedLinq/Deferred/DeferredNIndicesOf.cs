/*
    EnhancedLinq 
    Copyright (c) 2025-2026 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
    */

using System.Linq;

namespace EnhancedLinq.Deferred;

public static partial class EnhancedLinqDeferred
{
    /// <param name="source">The sequence to be searched.</param>
    /// <typeparam name="T">The type of the elements in the source sequence.</typeparam>
    extension<T>(IEnumerable<T> source) where T : notnull
    {
        /// <summary>
        /// Gets the first <paramref name="count"/> indices of the specified item within a sequence.
        /// </summary>
        /// <param name="target">The item to search for.</param>
        /// <param name="count">The maximum number of indices to return.</param>
        /// <returns>A sequence of the first <paramref name="count"/> indices if the object is found;
        /// an empty sequence otherwise.</returns>
        public IEnumerable<int> FirstNIndicesOf(T target, int count)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(count);
        
            return source.IndicesOf(target).Take(count);
        }
        
        /// <summary>
        /// Gets the first <paramref name="count"/> indices of the elements that match the predicate
        /// within a sequence.
        /// </summary>
        /// <param name="predicate">The predicate to use when comparing elements in the source.</param>
        /// <param name="count">The maximum number of indices to return.</param>
        /// <returns>A sequence of the first <paramref name="count"/> indices if one or more elements
        /// matching the predicate are found; an empty sequence otherwise.</returns>
        public IEnumerable<int> FirstNIndicesOf(Func<T, bool> predicate,
            int count)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(predicate);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(count);
        
            return source.IndicesOf(predicate).Take(count);
        }

        /// <summary>
        /// Gets the last <paramref name="count"/> indices of the specified item within a sequence.
        /// </summary>
        /// <param name="target">The item to search for.</param>
        /// <param name="count">The maximum number of indices to return.</param>
        /// <returns>A sequence of the last <paramref name="count"/> indices if the object is found;
        /// an empty sequence otherwise.</returns>
        public IEnumerable<int> LastNIndicesOf(T target, int count)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(count);
            
            return source.IndicesOf(target).TakeLast(count);
        }

        /// <summary>
        ///  Gets the last <paramref name="count"/> indices of the elements that match the predicate within a sequence.
        /// </summary>
        /// <param name="selector">The selector to use when comparing elements in the source.</param>
        /// <param name="count">The maximum number of indices to return.</param>
        /// <returns>A sequence of the last <paramref name="count"/> indices if one or more elements
        /// matching the predicate are found; an empty sequence otherwise.</returns>
        public IEnumerable<int> LastNIndicesOf(Func<T, bool> selector, int count) 
            => source.Reverse().FirstNIndicesOf(selector, count);
    }

    /// <param name="str">The input string.</param>
    extension(string str)
    {
        /// <summary>
        /// Finds the first <paramref name="count"/> occurrences of a specified char within a string,
        /// starting from the beginning of the string.
        /// </summary>
        /// <param name="c">The character to find in the string.</param>
        /// <param name="count">The maximum number of indices to return.</param>
        /// <returns>
        /// A sequence of the first <paramref name="count"/> indices where the character is found;
        /// an empty sequence if the character could not be found.
        /// </returns>
        public IEnumerable<int> FirstNIndicesOf(char c, int count)
        {
            ArgumentNullException.ThrowIfNull(str);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(count);
        
            return str.IndicesOf(c).Take(count);
        }

        /// <summary>
        /// Finds the first <paramref name="count"/> occurrences of a specified substring within a string,
        /// starting from the beginning of the string.
        /// </summary>
        /// <param name="substring">The substring to look for.</param>
        /// <param name="count">The maximum number of indices to return.</param>
        /// <returns>A sequence of the first <paramref name="count"/> indices where the character is found;
        /// an empty sequence if the character could not be found.</returns>
        public IEnumerable<int> FirstNIndicesOf(string substring, int count)
        {
            ArgumentNullException.ThrowIfNull(str);
            ArgumentException.ThrowIfNullOrEmpty(substring);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(count);
        
            return str.IndicesOf(substring).Take(count);
        }

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
        {
            ArgumentNullException.ThrowIfNull(str);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(count);
            
            return str.IndicesOf(c).TakeLast(count);
        }

        /// <summary>
        /// Finds the last <paramref name="count"/> occurrences of a specified substring within a string, starting from the beginning of the string.
        /// </summary>
        /// <param name="substring">The substring to look for.</param>
        /// <param name="count">The maximum number of indices to return.</param>
        /// <returns>A sequence of the last <paramref name="count"/> indices where the character is found;
        /// an empty sequence if the character could not be found.</returns>
        public IEnumerable<int> LastNIndicesOf(string substring, int count)
        {
            ArgumentNullException.ThrowIfNull(str);
            ArgumentNullException.ThrowIfNull(substring);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(count);
            
            return str.IndicesOf(substring).TakeLast(count);
        }
    }
}