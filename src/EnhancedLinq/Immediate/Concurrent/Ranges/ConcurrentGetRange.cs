/*
    EnhancedLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/.
 */

using System;
using System.Collections.Concurrent;
using EnhancedLinq.Internals.Localizations;

namespace EnhancedLinq.Immediate.Concurrent.Ranges;

public static partial class EnhancedLinqImmediateConcurrentRange
{
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