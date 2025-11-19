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

#if NET8_0_OR_GREATER
using System;
#endif
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace AlastairLundy.EnhancedLinq.Immediate.Concurrent.Ranges;

public static partial class EnhancedLinqImmediateConcurrentRange
{
    /// <param name="collection">The producer-consumer collection to add objects to.</param>
    /// <typeparam name="T">The type of elements contained within the collection.</typeparam>
    extension<T>(IProducerConsumerCollection<T> collection)
    {
        /// <summary>
        /// Attempts to add multiple objects to a producer-consumer collection.
        /// </summary>
        /// <param name="items">The collection of objects to be added to the collection.</param>
        /// <returns>True if all objects were successfully added, false otherwise.</returns>
        public bool TryAddRange(IEnumerable<T> items)
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
    }

    /// <summary>
    /// Adds multiple objects to a concurrent bag.
    /// </summary>
    /// <param name="source">The concurrent bag to which the objects will be added.</param>
    /// <param name="items">The collection of objects to add into the concurrent bag.</param>
    /// <typeparam name="T">The type of elements contained within the bag.</typeparam>
    public static void AddRange<T>(this ConcurrentBag<T> source, IEnumerable<T> items)
    {
#if NET8_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(items);
#endif
        
        foreach (T item in items)
        {
            source.Add(item);
        }
    }

    /// <summary>
    /// Adds multiple objects to a concurrent queue.
    /// </summary>
    /// <param name="source">The concurrent queue to which the objects will be added.</param>
    /// <param name="items">The collection of objects to enqueue into the concurrent queue.</param>
    /// <typeparam name="T">The type of elements contained within the queue.</typeparam>
    public static void EnqueueRange<T>(this ConcurrentQueue<T> source, IEnumerable<T> items)
    {
#if NET8_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(items);
#endif
        
        foreach (T item in items)
        {
            source.Enqueue(item);
        }
    }
}