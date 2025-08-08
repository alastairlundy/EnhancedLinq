/*
        MIT License
       
       Copyright (c) 2024-2025 Alastair Lundy
       
       Permission is hereby granted, free of charge, to any person obtaining a copy
       of this software and associated documentation files (the "Software"), to deal
       in the Software without restriction, including without limitation the rights
       to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
       copies of the Software, and to permit persons to whom the Software is
       furnished to do so, subject to the following conditions:
       
       The above copyright notice and this permission notice shall be included in all
       copies or substantial portions of the Software.
       
       THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
       IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
       FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
       AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
       LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
       OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
       SOFTWARE.
   */

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using ExtraLinq.Internals.Localizations;

namespace ExtraLinq.ToProcess.Concurrent;

/// <summary>
/// 
/// </summary>
public static class ConcurrentRangeExtensions
{
    /// <summary>
    /// Adds all objects in a provided IEnumerable to a concurrent bag.
    /// </summary>
    /// <param name="concurrentBag">The concurrent bag to add objects to.</param>
    /// <param name="items">The collection of objects to be added to the concurrent bag.</param>
    /// <typeparam name="T">The type of elements contained within the concurrent bag.</typeparam>
    public static void AddRange<T>(this ConcurrentBag<T> concurrentBag, IEnumerable<T> items)
    {
        foreach (T item in items)
        {
            concurrentBag.Add(item);
        }
    }
        
    /// <summary>
    /// Attempts to add multiple objects to a producer-consumer collection.
    /// </summary>
    /// <param name="collection">The producer-consumer collection to add objects to.</param>
    /// <param name="items">The collection of objects to be added to the collection.</param>
    /// <typeparam name="T">The type of elements contained within the collection.</typeparam>
    /// <returns>True if all objects were successfully added, false otherwise.</returns>
    public static bool TryAddRange<T>(this IProducerConsumerCollection<T> collection, IEnumerable<T> items)
    {
        foreach (T item in items)
        {
            bool result = collection.TryAdd(item);

            if (result == false)
            {
                return false;
            }
        }
        return true;
    }

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


    /// <summary>
    /// Retrieves the specified range of elements from the collection.
    /// </summary>
    /// <param name="collection">The producer-consumer collection to retrieve elements from.</param>
    /// <param name="startIndex">The starting index (inclusive) of the range to retrieve.</param>
    /// <param name="count">The number of elements in the range to retrieve.</param>
    /// <typeparam name="T">The type of elements contained within the collection.</typeparam>
    /// <returns>A new sequence containing the specified range of elements.</returns>
    public static IProducerConsumerCollection<T> GetRange<T>(this IProducerConsumerCollection<T> collection,
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
            if (actualIndex >= startIndex || actualIndex <= limit)
            {
                output.Add(item);
            }

            if (actualIndex == limit)
            {
                break;
            }

            actualIndex++;
        }
           
        return output;
    }

}