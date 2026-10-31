/*
    EnhancedLinq 
    Copyright (c) 2025-2026 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
    */

using System.Linq;
using EnhancedLinq.Immediate.Lists.Linq;

namespace EnhancedLinq.Immediate.Lists;

/// <summary>
/// 
/// </summary>
public static class ImmediateListExcludeExtensions
{
    /// <summary>
    /// Provides extension methods for performing immediate operations on a list.
    /// </summary>
    /// <param name="list">The source list on which the operations are performed.</param>
    extension<TSource>(List<TSource> list)
    {
        /// <summary>
        /// Removes elements from the list that exist in the specified collection and satisfy a given predicate.
        /// </summary>
        /// <param name="collection">The collection of elements to be excluded from the list.</param>
        /// <param name="predicate">The predicate that determines whether an element should be excluded.</param>
        /// <returns>A new list that contains the elements of the original list minus the ones that are both in the specified collection and satisfy the predicate.</returns>
        public IList<TSource> Exclude(ICollection<TSource> collection, Func<TSource, bool> predicate)
        {
            ArgumentNullException.ThrowIfNull(collection);
            ArgumentNullException.ThrowIfNull(predicate);

            HashSet<TSource> excluded = new();

            foreach (TSource item in collection)
            {
                if (predicate(item))
                {
                    excluded.Add(item);
                }
            }

            List<TSource> output = new(list.Count);

            foreach (TSource item in list)
            {
                if (!excluded.Contains(item))
                    output.Add(item);
            }

            return output;
        }

        /// <summary>
        /// Removes elements from the list that exist in the specified collection and satisfy a given predicate.
        /// </summary>
        /// <param name="collection">The collection of elements to be excluded from the list.</param>
        /// <returns>A new list that contains the elements of the original list minus the ones that are both in the specified collection and satisfy the predicate.</returns>
        public IList<TSource> Exclude(ICollection<TSource> collection)
        {
            ArgumentNullException.ThrowIfNull(collection);

            HashSet<TSource> excluded = new(collection);

            List<TSource> output = new(capacity: list.Count);

            foreach (TSource item in list)
            {
                if (!excluded.Contains(item))
                    output.Add(item);
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
        /// <param name="collection">The collection of elements to be excluded from the array.</param>
        /// <returns>A new array that contains the elements of the original array minus the ones present in the specified collection.</returns>
        public TSource[] Exclude(ICollection<TSource> collection)
        {
            ArgumentNullException.ThrowIfNull(array);
            ArgumentNullException.ThrowIfNull(collection);

            TSource[] output = new TSource[array.Length];

            HashSet<TSource> excluded = new(collection);

            int currentIndex = 0;
            for (int index = 0; index < array.Length; index++)
            {
                if (!excluded.Contains(array[index]))
                {
                    output[currentIndex] = array[index];
                    currentIndex++;
                }
            }

            if (currentIndex < array.Length)
            {
                Array.Resize(ref output, currentIndex);
            }

            return output;
        }

        /// <summary>
        /// Removes elements from the current array that exist in the specified collection and satisfy the given predicate.
        /// </summary>
        /// <param name="collection">The collection of elements to be excluded from the array.</param>
        /// <param name="predicate">The predicate that determines whether an element should be excluded.</param>
        /// <returns>A new array that contains the elements of the original array minus the ones that are both in the specified collection and satisfy the predicate.</returns>
        public TSource[] Exclude(ICollection<TSource> collection, Func<TSource, bool> predicate)
        {
            ArgumentNullException.ThrowIfNull(collection);
            ArgumentNullException.ThrowIfNull(predicate);

            HashSet<TSource> excluded = new();

            foreach (TSource item in collection)
            {
                if (predicate(item))
                {
                    excluded.Add(item);
                }
            }

            List<TSource> output = new(array.Length);

            foreach (TSource item in array)
            {
                if (!excluded.Contains(item))
                    output.Add(item);
            }

            return output.ToArray();
        }
    }
}