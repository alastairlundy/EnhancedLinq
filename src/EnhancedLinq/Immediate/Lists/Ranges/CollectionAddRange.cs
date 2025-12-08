/*
    EnhancedLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
    */

namespace EnhancedLinq.Immediate.Ranges;

public static partial class EnhancedLinqImmediateRange
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="source">The collection into which elements will be appended.</param>
    /// <typeparam name="T">The type of elements in both collections.</typeparam>
    extension<T>(ICollection<T> source)
    {
        /// <summary>
        /// Appends elements from another collection to the end of the specified collection.
        /// </summary>
        /// <param name="enumerableToAdd">The IEnumerable containing elements to append to the original collection.</param>
        /// <exception cref="NotSupportedException">Thrown if adding to the collection is not supported.</exception>
        public void AddRange(IEnumerable<T> enumerableToAdd)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(enumerableToAdd);

            if (source.IsReadOnly)
                throw new NotSupportedException();
        
            if (source.Count == int.MaxValue)
            {
                throw new OverflowException($"{nameof(source)} contains the maximum size of {int.MaxValue} and cannot be added to.");
            }
        
            foreach (T item in enumerableToAdd)
            {
                source.Add(item);
            }
        }

        /// <summary>
        /// Appends elements from another collection to the end of the specified collection.
        /// </summary>
        /// <param name="collectionToAdd">The collection containing elements to append to the original collection.</param>
        public void AddRange(ICollection<T> collectionToAdd)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(collectionToAdd);

            if (source.IsReadOnly)
                throw new NotSupportedException();
        
            if (source.Count == int.MaxValue)
            {
                throw new OverflowException($"{nameof(source)} contains the maximum size of {int.MaxValue} and cannot be added to.");
            }
            else if (collectionToAdd.Count == int.MaxValue)
            {
                throw new OverflowException($"{nameof(collectionToAdd)} contains the maximum size of {int.MaxValue} and cannot be added to {nameof(source)}.");
            }
        
            if (source is List<T> { Count: 0 })
            {
                source = new List<T>(capacity: collectionToAdd.Count);
            }
        
            foreach (T item in collectionToAdd)
            {
                source.Add(item);
            }
        }
    }
}