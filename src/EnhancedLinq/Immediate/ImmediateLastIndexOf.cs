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

namespace AlastairLundy.EnhancedLinq.Immediate;

public static partial class EnhancedLinqImmediate
{
    
    /// <summary>
    /// Gets the index of the last element that matches the predicate condition.
    /// </summary>
    /// <remarks>
    /// This method is a computationally expensive as the number of items in the sequence is needed, to obtain the index
    /// of the last element that satisfies the predicate.
    /// </remarks>
    /// <param name="source">The <see cref="IEnumerable{T}"/> to be searched.</param>
    /// <param name="predicate">The predicate condition to check elements of the sequence against.</param>
    /// <typeparam name="T">The type of elements in the sequence.</typeparam>
    /// <returns>The index of the last element in the sequence to match the predicate condition,
    /// if the sequence contains any elements that match the predicate condition, returns -1 otherwise.
    /// </returns>
    public static int LastIndexOf<T>(this IEnumerable<T> source, Func<T, bool> predicate)
    {
#if NET8_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(source, nameof(source));
#endif
        if (source is ICollection<T> collection)
        {
            return LastIndexOf(collection, predicate);
        }
        
        bool foundItem = false;
        int reverseIndex = 0;
        
        int count = 0;
        
        foreach (T item in source.Reverse())
        {
            if (predicate(item))
            {
                foundItem = true;
                reverseIndex = count;
            }

            count++;
        }

        
        return foundItem ? Math.Abs(count - reverseIndex) : -1;
    }
    
    /// <summary>
    /// Gets the index of the last element that matches the predicate condition.
    /// </summary>
    /// <param name="source">The <see cref="ICollection{T}"/> to be searched.</param>
    /// <param name="predicate">The predicate condition to check elements of the collection against.</param>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <returns>The index of the last element in the collection to match the predicate condition,
    /// if the collection contains any elements that match the predicate condition, returns -1 otherwise.
    /// </returns>
    public static int LastIndexOf<T>(this ICollection<T> source, Func<T, bool> predicate)
    {
#if NET8_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(source, nameof(source));
#endif
        
        int index = source.Count - 1;
        
        foreach (T item in source.Reverse())
        {
            if(predicate(item))
                return index;

            index--;
        }

        return -1;
    }
    
    /// <summary>
    /// Gets the last index of an element in a sequence.
    /// </summary>
    /// <remarks>
    /// This method is a computationally expensive as the number of items in the sequence is needed, to obtain the index
    /// of the last element that satisfies the selector.
    /// </remarks>
    /// <param name="source">The sequence to be searched.</param>
    /// <param name="obj">The element to get the last index of.</param>
    /// <typeparam name="T">The type of elements in the sequence.</typeparam>
    /// <returns>The last index of an element in a sequence, if the sequence contains the element, returns -1 otherwise.</returns>
    public static int LastIndexOf<T>(this IEnumerable<T> source, T obj)
    {
#if NET8_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(source, nameof(source));
#endif
        
        bool foundItem = false;
        int reverseIndex = 0;
        int count = 0;
                
        foreach (T item in source.Reverse())
        {
            if (item is not null && item.Equals(obj))
            {
                foundItem = true;
                reverseIndex = count;
            }
                    
            count++;
        }
        
        return foundItem ? Math.Abs(count - reverseIndex) : -1;
    }

    /// <summary>
    /// Gets the last index of an element in a collection.
    /// </summary>
    /// <param name="source">The collection to be searched.</param>
    /// <param name="obj">The element to get the index of.</param>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <returns>The index of an element in a collection, if the collection contains the element, returns -1 otherwise.</returns>
    public static int LastIndexOf<T>(this ICollection<T> source, T obj)
    {
#if NET8_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(source, nameof(source));
#endif
        
        int index = source.Count -1;
                
        foreach (T item in source.Reverse())
        {
            if (item is not null && item.Equals(obj))
                return index;
                    
            index--;
        }
        
        return -1;
    }
}