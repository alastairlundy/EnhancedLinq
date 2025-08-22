/*
    EnhancedLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/.
 */

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using AlastairLundy.EnhancedLinq.Internals.Localizations;

namespace AlastairLundy.EnhancedLinq.Immediate.Concurrent.Ranges;

public static partial class EnhancedLinqImmediateConcurrentRange
{
        /// <summary>
    /// Removes multiple objects from a concurrent bag.
    /// </summary>
    /// <param name="concurrentBag">The concurrent bag from which objects will be removed.</param>
    /// <param name="itemsToRemove">A sequence of objects to be removed from the concurrent bag.</param>
    /// <typeparam name="T">The type of elements contained within the concurrent bag.</typeparam>
    /// <returns>A new concurrent bag with all specified objects removed.</returns>
    public static ConcurrentBag<T> RemoveRange<T>(this ConcurrentBag<T> concurrentBag,
        IEnumerable<T> itemsToRemove)
    {
        IEnumerable<T> newCollection = from item in concurrentBag
            where itemsToRemove.Contains(item) == false
            select item;
            
        return new ConcurrentBag<T>(newCollection);
    }

    /// <summary>
    /// Removes a range of objects from a concurrent bag.
    /// </summary>
    /// <param name="concurrentBag">The concurrent bag from which objects will be removed.</param>
    /// <param name="startIndex">The starting index (inclusive) of the range to remove.</param>
    /// <param name="count">The number of elements in the range to remove.</param>
    /// <typeparam name="T">The type of elements contained within the concurrent bag.</typeparam>
    /// <returns>A new concurrent bag with the specified range removed.</returns>
    /// <exception cref="ArgumentException">Thrown if more items are requested
    /// to be removed than exist in the ConcurrentBag.</exception>
    /// <exception cref="IndexOutOfRangeException">Thrown if the start index is less than 0 or greater
    /// than the number of items in the ConcurrentBag.</exception>
    public static ConcurrentBag<T> RemoveRange<T>(this ConcurrentBag<T> concurrentBag,
        int startIndex, int count)
    {
        ConcurrentBag<T> output = new ConcurrentBag<T>();

        int limit = startIndex + count;

        if (limit > concurrentBag.Count)
        {
            throw new ArgumentException(Resources.Exceptions_Count_LessThanZero);
        }

        if (startIndex < 0 || startIndex >= concurrentBag.Count && startIndex != 0 ||
            startIndex > concurrentBag.Count)
        {
            throw new IndexOutOfRangeException(Resources.Exceptions_IndexOutOfRange
                .Replace("{x}", $"{startIndex}")
                .Replace("{y}", $"0")
                .Replace("{z}", $"{limit}"));
        }

        if (count < 0)
        {
            //TODO: Add CountOutOfRange in the future
        }
            
        int actualIndex = 0;
        foreach (T item in concurrentBag)
        {
            if (actualIndex < startIndex || actualIndex > limit)
            {
                output.Add(item);
            }
            actualIndex++;
        }
           
        return output;
    }

    /// <summary>
    /// Removes specified items from the collection and returns a new producer-consumer collection.
    /// </summary>
    /// <param name="collection">The producer-consumer collection to remove elements from.</param>
    /// <param name="itemsToRemove">A sequence of objects to be removed from the collection.</param>
    /// <typeparam name="T">The type of elements contained within the collection.</typeparam>
    /// <returns>A new producer-consumer collection with all specified objects removed.</returns>
    public static IProducerConsumerCollection<T> RemoveRange<T>(this IProducerConsumerCollection<T> collection,
        IEnumerable<T> itemsToRemove)
    {
        IEnumerable<T> newCollection = from item in collection
            where itemsToRemove.Contains(item) == false
            select item;
            
        return new ConcurrentBag<T>(newCollection);
    }
        
    /// <summary>
    /// Removes a range of elements from the collection and returns a new producer-consumer collection.
    /// </summary>
    /// <param name="collection">The producer-consumer collection to remove elements from.</param>
    /// <param name="startIndex">The starting index (inclusive) of the range to remove.</param>
    /// <param name="count">The number of elements in the range to remove.</param>
    /// <typeparam name="T">The type of elements contained within the collection.</typeparam>
    /// <returns>A new producer-consumer collection with the specified range removed.</returns>
    public static IProducerConsumerCollection<T> RemoveRange<T>(this IProducerConsumerCollection<T> collection,
        int startIndex, int count)
    {
        ConcurrentBag<T> output = new ConcurrentBag<T>();

        int limit = startIndex + count;

        if (limit > collection.Count)
        {
            throw new ArgumentException(Resources.Exceptions_Count_LessThanZero);
        }

        if (startIndex < 0 || startIndex >= collection.Count && startIndex != 0 ||
            startIndex > collection.Count)
        {
            throw new IndexOutOfRangeException(Resources.Exceptions_IndexOutOfRange
                .Replace("{x}", $"{startIndex}")
                .Replace("{y}", $"0")
                .Replace("{z}", $"{limit}"));
        }

        if (count < 0)
        {
            //TODO: Add CountOutOfRange in the future
        }
            
        int actualIndex = 0;
        foreach (T item in collection)
        {
            if (actualIndex < startIndex || actualIndex > limit)
            {
                output.Add(item);
            }
            actualIndex++;
        }
           
        return output;
    }
}