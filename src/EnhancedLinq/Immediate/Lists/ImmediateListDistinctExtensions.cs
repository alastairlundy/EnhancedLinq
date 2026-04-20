/*
    EnhancedLinq 
    Copyright (c) 2025-2026 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
*/

using System.Linq;

namespace EnhancedLinq.Immediate.Lists;

/// <summary>
/// Provides extension methods for performing immediate distinct operations on lists.
/// </summary>
public static class ImmediateListDistinctExtensions
{
    /// <param name="source">The list to deduplicate.</param>
    /// <typeparam name="T">The type of elements in the list.</typeparam>
    extension<T>(List<T> source)
    {
        /// <summary>
        /// Creates a new <see cref="List{T}"/> with distinct elements from the source list.
        /// </summary>
        /// <returns>The new list with distinct elements from the source list.</returns>
        public IList<T> Distinct()
            => source.Distinct(EqualityComparer<T>.Default);
        
        /// <summary>
        /// Creates a new <see cref="List{T}"/> with distinct elements from the source list.
        /// </summary>
        /// <param name="equalityComparer">The equality comparer to use.</param>
        /// <returns>The new list with distinct elements from the source list.</returns>
        public IList<T> Distinct(IEqualityComparer<T> equalityComparer)
            => DistinctShared(source, equalityComparer);
    }

    /// <param name="source">The array to deduplicate.</param>
    /// <typeparam name="T">The type of elements in the array.</typeparam>
    extension<T>(T[] source)
    {
        /// <summary>
        /// Creates a new array with distinct elements from the source array.
        /// </summary>
        /// <returns>The new array with distinct elements from the source array.</returns>
        public T[] Distinct()
            => source.Distinct(EqualityComparer<T>.Default);

        /// <summary>
        /// Creates a new array with distinct elements from the source array.
        /// </summary>
        /// <param name="equalityComparer">The equality comparer to use.</param>
        /// <returns>The new array with distinct elements from the source array.</returns>
        public T[] Distinct(IEqualityComparer<T> equalityComparer)
            => DistinctShared(source, equalityComparer).ToArray();
    }

    private static IList<T> DistinctShared<T>(IList<T> source, IEqualityComparer<T> equalityComparer)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(equalityComparer);
        
#if NET8_0_OR_GREATER
        HashSet<T> hash = new(capacity: source.Count, comparer: equalityComparer);
#else
        HashSet<T> hash = new(comparer: equalityComparer);
#endif
        List<T> output = new(source.Count);

        for (int index = 0; index < source.Count; index++)
        {
            T item = source[index];
            bool result = hash.Add(item);

            if (!result)
                output.Add(item);
        }

        return output;
    }
}