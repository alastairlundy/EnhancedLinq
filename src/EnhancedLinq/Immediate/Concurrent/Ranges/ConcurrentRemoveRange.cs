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
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using AlastairLundy.EnhancedLinq.Internals.Localizations;

namespace AlastairLundy.EnhancedLinq.Immediate.Concurrent.Ranges;

public static partial class EnhancedLinqImmediateConcurrentRange
{
    /// <param name="concurrentBag">The concurrent bag from which objects will be removed.</param>
    /// <typeparam name="T">The type of elements contained within the concurrent bag.</typeparam>
    extension<T>(ConcurrentBag<T> concurrentBag)
    {
        /// <summary>
        /// Removes multiple objects from a concurrent bag.
        /// </summary>
        /// <param name="itemsToRemove">A sequence of objects to be removed from the concurrent bag.</param>
        /// <returns>A new concurrent bag with all specified objects removed.</returns>
        public ConcurrentBag<T> RemoveRange(IEnumerable<T> itemsToRemove)
        {
            ArgumentNullException.ThrowIfNull(concurrentBag);
            ArgumentNullException.ThrowIfNull(itemsToRemove);
            
            IEnumerable<T> newCollection = from item in concurrentBag
                where !itemsToRemove.Contains(item)
                select item;
            
            return new ConcurrentBag<T>(newCollection);
        }

        /// <summary>
        /// Removes a range of objects from a concurrent bag.
        /// </summary>
        /// <param name="startIndex">The starting index (inclusive) of the range to remove.</param>
        /// <param name="count">The number of elements in the range to remove.</param>
        /// <returns>A new concurrent bag with the specified range removed.</returns>
        /// <exception cref="ArgumentException">Thrown if more items are requested
        /// to be removed than exist in the ConcurrentBag.</exception>
        /// <exception cref="IndexOutOfRangeException">Thrown if the start index is less than 0 or greater
        /// than the number of items in the ConcurrentBag.</exception>
        public ConcurrentBag<T> RemoveRange(int startIndex, int count)
        {
            ArgumentNullException.ThrowIfNull(concurrentBag);
            ArgumentOutOfRangeException.ThrowIfNegative(startIndex);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(count);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(count, concurrentBag.Count);
            
            ConcurrentBag<T> output = new ConcurrentBag<T>();

            int limit = startIndex + count;

            if (limit > concurrentBag.Count)
            {
                throw new ArgumentException(Resources.Exceptions_Count_LessThanZero);
            }

            if (startIndex >= concurrentBag.Count && startIndex != 0 ||
                startIndex > concurrentBag.Count)
            {
                throw new IndexOutOfRangeException(Resources.Exceptions_IndexOutOfRange
                    .Replace("{x}", $"{startIndex}")
                    .Replace("{y}", $"0")
                    .Replace("{z}", $"{limit}"));
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
    }

    /// <param name="collection">The producer-consumer collection to remove elements from.</param>
    /// <typeparam name="T">The type of elements contained within the collection.</typeparam>
    extension<T>(IProducerConsumerCollection<T> collection)
    {
        /// <summary>
        /// Removes specified items from the collection and returns a new producer-consumer collection.
        /// </summary>
        /// <param name="itemsToRemove">A sequence of objects to be removed from the collection.</param>
        /// <returns>A new producer-consumer collection with all specified objects removed.</returns>
        public IProducerConsumerCollection<T> RemoveRange(IEnumerable<T> itemsToRemove)
        {
            ArgumentNullException.ThrowIfNull(collection);
            ArgumentNullException.ThrowIfNull(itemsToRemove);

            IEnumerable<T> newCollection = from item in collection
                where itemsToRemove.Contains(item) == false
                select item;
            
            return new ConcurrentBag<T>(newCollection);
        }

        /// <summary>
        /// Removes a range of elements from the collection and returns a new producer-consumer collection.
        /// </summary>
        /// <param name="startIndex">The starting index (inclusive) of the range to remove.</param>
        /// <param name="count">The number of elements in the range to remove.</param>
        /// <returns>A new producer-consumer collection with the specified range removed.</returns>
        public IProducerConsumerCollection<T> RemoveRange(int startIndex, int count)
        {
            ArgumentNullException.ThrowIfNull(collection);
            ArgumentOutOfRangeException.ThrowIfNegative(startIndex);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(count);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(count, collection.Count);
            
            ConcurrentBag<T> output = new ConcurrentBag<T>();

            int limit = startIndex + count;

            if (limit > collection.Count)
                throw new ArgumentException(Resources.Exceptions_Count_LessThanZero);

            if (startIndex >= collection.Count && startIndex != 0 ||
                startIndex > collection.Count)
            {
                throw new IndexOutOfRangeException(Resources.Exceptions_IndexOutOfRange
                    .Replace("{x}", $"{startIndex}")
                    .Replace("{y}", $"0")
                    .Replace("{z}", $"{limit}"));
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
}