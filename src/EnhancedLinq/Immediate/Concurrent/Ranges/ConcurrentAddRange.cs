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

namespace EnhancedLinq.Immediate.Concurrent.Ranges;

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
            ArgumentNullException.ThrowIfNull(collection);
            ArgumentNullException.ThrowIfNull(items);
            
            foreach (T item in items)
            {
                bool result = collection.TryAdd(item);

                if (!result)
                {
                    return false;
                }
            }
            return true;
        }
    }

    /// <param name="source">The concurrent bag to which the objects will be added.</param>
    /// <typeparam name="T">The type of elements contained within the bag.</typeparam>
    extension<T>(ConcurrentBag<T> source)
    {
        /// <summary>
        /// Adds multiple objects to a concurrent bag.
        /// </summary>
        /// <param name="items">The collection of objects to add into the concurrent bag.</param>
        public void AddRange(IEnumerable<T> items)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(items);
        
            foreach (T item in items)
            {
                source.Add(item);
            }
        }
    }

    /// <param name="source">The concurrent queue to which the objects will be added.</param>
    /// <typeparam name="T">The type of elements contained within the queue.</typeparam>
    extension<T>(ConcurrentQueue<T> source)
    {
        /// <summary>
        /// Adds multiple objects to a concurrent queue.
        /// </summary>
        /// <param name="items">The collection of objects to enqueue into the concurrent queue.</param>
        public void EnqueueRange(IEnumerable<T> items)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(items);
        
            foreach (T item in items)
            {
                source.Enqueue(item);
            }
        }
    }
}