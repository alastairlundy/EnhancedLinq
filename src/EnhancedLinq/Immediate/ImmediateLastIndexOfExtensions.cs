/*
    EnhancedLinq 
    Copyright (c) 2025-2026 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
    */

using System.Linq;

namespace EnhancedLinq.Immediate;

/// <summary>
/// Provides extension methods for improved functionality related to determining the last index
/// of elements in a collection with immediate evaluation.
/// </summary>
public static class ImmediateLastIndexOfExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="source">The <see cref="IEnumerable{T}"/> to be searched.</param>
    /// <typeparam name="T">The type of elements in the sequence.</typeparam>
    extension<T>(IEnumerable<T> source)
    {
        /// <summary>
        /// Gets the index of the last element that matches the predicate condition.
        /// </summary>
        /// <remarks>
        /// This method performs a single forward pass over the sequence, tracking the index of the most recent element
        /// that satisfies the predicate, without buffering the sequence.
        /// </remarks>
        /// <param name="predicate">The predicate condition to check elements of the sequence against.</param>
        /// <returns>The index of the last element in the sequence to match the predicate condition,
        /// if the sequence contains any elements that match the predicate condition, returns -1 otherwise.
        /// </returns>
        public int LastIndexOf(Func<T, bool> predicate)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(predicate);

            if (source is ICollection<T> collection)
            {
                return collection.LastIndexOf(predicate);
            }

            int index = 0;
            int matchIndex = -1;

            foreach (T item in source)
            {
                if (predicate(item))
                {
                    matchIndex = index;
                }

                index++;
            }

            return matchIndex;
        }

        /// <summary>
        /// Gets the last index of an element in a sequence.
        /// </summary>
        /// <remarks>
        /// This method performs a single forward pass over the sequence, tracking the index of the most recent element
        /// that equals obj, without buffering the sequence.
        /// </remarks>
        /// <param name="obj">The element to get the last index of.</param>
        /// <returns>The last index of an element in a sequence, if the sequence contains the element, returns -1 otherwise.</returns>
        public int LastIndexOf(T obj)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(obj);
        
            int index = 0;
            int matchIndex = -1;

            foreach (T item in source)
            {
                if (item is not null && item.Equals(obj))
                {
                    matchIndex = index;
                }

                index++;
            }

            return matchIndex;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="source">The <see cref="ICollection{T}"/> to be searched.</param>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    extension<T>(ICollection<T> source)
    {
        /// <summary>
        /// Gets the index of the last element that matches the predicate condition.
        /// </summary>
        /// <param name="predicate">The predicate condition to check elements of the collection against.</param>
        /// <returns>The index of the last element in the collection to match the predicate condition,
        /// if the collection contains any elements that match the predicate condition, returns -1 otherwise.
        /// </returns>
        public int LastIndexOf(Func<T, bool> predicate)
        {
#if NET10_0_OR_GREATER
            ArgumentNullException.ThrowIfNull(source);
#endif
        
            int index = 0;
            int matchIndex = -1;

            foreach (T item in source)
            {
                if (predicate(item))
                {
                    matchIndex = index;
                }

                index++;
            }

            return matchIndex;
        }
        
        /// <summary>
        /// Gets the last index of an element in a collection.
        /// </summary>
        /// <param name="obj">The element to get the index of.</param>
        /// <returns>The index of an element in a collection, if the collection contains the element, returns -1 otherwise.</returns>
        public int LastIndexOf(T obj)
        {
#if NET10_0_OR_GREATER
            ArgumentNullException.ThrowIfNull(source);
#endif
        
            int index = 0;
            int matchIndex = -1;

            foreach (T item in source)
            {
                if (item is not null && item.Equals(obj))
                {
                    matchIndex = index;
                }

                index++;
            }

            return matchIndex;
        }
    }
}