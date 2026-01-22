/*
    EnhancedLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
    */

using System.Linq;
using EnhancedLinq.Immediate.Linq;

namespace EnhancedLinq.Immediate;

public static partial class EnhancedLinqImmediate
{
    /// <summary>
    /// Provides extension methods for performing immediate operations on a list.
    /// </summary>
    /// <typeparam name="TSource">The type of elements in the list.</typeparam>
    /// <param name="list">The source list on which the operations are performed.</param>
    extension<TSource>(List<TSource> list)
    {
        /// <summary>
        /// Removes elements from the list that exist in the specified collection and satisfy a given predicate.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the list and collection.</typeparam>
        /// <param name="collection">The collection of elements to be excluded from the list.</param>
        /// <param name="predicate">The predicate that determines whether an element should be excluded.</param>
        /// <returns>A new list that contains the elements of the original list minus the ones that are both in the specified collection and satisfy the predicate.</returns>
        public List<TSource> Exclude(ICollection<TSource> collection, Func<TSource, bool> predicate)
        {
            ArgumentNullException.ThrowIfNull(collection);
            ArgumentNullException.ThrowIfNull(predicate);
            
            List<TSource> output = new(list.Count);
            output.AddRange(list);

            foreach (TSource item in collection)
            {
                if (predicate(item))
                {
                    if(output.Contains(item))
                        output.Remove(item);
                }
            }
            
            return output;
        }

        /// <summary>
        /// Removes elements from the list that exist in the specified collection and satisfy a given predicate.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the list and collection.</typeparam>
        /// <param name="collection">The collection of elements to be excluded from the list.</param>
        /// <returns>A new list that contains the elements of the original list minus the ones that are both in the specified collection and satisfy the predicate.</returns>
        public List<TSource> Exclude(ICollection<TSource> collection)
        {
            ArgumentNullException.ThrowIfNull(collection);

            List<TSource> output = new(capacity: list.Count);
            output.AddRange(list);

            foreach (TSource item in collection)
            {
                if(output.Contains(item))
                    output.Remove(item);
            }
            
            return output;
        }
    }

    /// <summary>
    /// Provides extension methods for excluding elements from an array based on a collection and optional conditions.
    /// </summary>
    /// <typeparam name="TSource">The type of elements in the array and collections.</typeparam>
    /// <param name="array">The source array from which elements will be excluded.</param>
    extension<TSource>(TSource[] array) where TSource : notnull
    {
        /// <summary>
        /// Removes elements from the array that exist in the specified collection.
        /// </summary>
        /// <typeparam name="TSource">The type of elements in the array and the collection.</typeparam>
        /// <param name="collection">The collection of elements to be excluded from the array.</param>
        /// <returns>A new array that contains the elements of the original array minus the ones present in the specified collection.</returns>
        public TSource[] Exclude(ICollection<TSource> collection)
        {
            ArgumentNullException.ThrowIfNull(array);
            ArgumentNullException.ThrowIfNull(collection);
            
            TSource[] output = new TSource[array.Length];

            int currentIndex = 0;
            for (int index = 0; index < collection.Count; index++)
            {
                if (collection.IndexOf(collection.ElementAt(index)) == -1)
                {
                    output[currentIndex] = collection.First(x => x.Equals(output[index]));
                    currentIndex++;
                }
            }

            if (currentIndex < array.Length)
                return output.Take(currentIndex);

            return output;
        }

        /// <summary>
        /// Removes elements from the current array that exist in the specified collection and satisfy the given predicate.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the array and collection.</typeparam>
        /// <param name="collection">The collection of elements to be excluded from the array.</param>
        /// <param name="predicate">The predicate that determines whether an element should be excluded.</param>
        /// <returns>A new array that contains the elements of the original array minus the ones that are both in the specified collection and satisfy the predicate.</returns>
        public TSource[] Exclude(ICollection<TSource> collection, Func<TSource, bool> predicate)
        {
            ArgumentNullException.ThrowIfNull(collection);
            ArgumentNullException.ThrowIfNull(predicate);
            
            List<TSource> output = new(collection.Count);
            output.AddRange(array);
            
            foreach (TSource item in collection)
            {
                if (predicate(item))
                {
                    if(output.Contains(item))
                        output.Remove(item);
                }
            }

            return output.ToArray();
        }
    }
}