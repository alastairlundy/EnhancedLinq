/*
    EnhancedLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/.
 */

using System;
using System.Collections.Generic;

namespace EnhancedLinq.Immediate;

/// <summary>
/// 
/// </summary>
public static partial class EnhancedLinqImmediate
{
    /// <summary>
    /// Creates a new <see cref="List{T}"/> with distinct elements from the source list.
    /// </summary>
    /// <param name="source">The list to de-duplicate.</param>
    /// <typeparam name="T">The type of elements in the list.</typeparam>
    /// <returns>The new list with distinct elements from the source list.</returns>
    public static List<T> Distinct<T>(this List<T> source)
        => Distinct(source, EqualityComparer<T>.Default);
    
    /// <summary>
    /// Creates a new <see cref="List{T}"/> with distinct elements from the source list.
    /// </summary>
    /// <param name="source">The list to de-duplicate.</param>
    /// <param name="equalityComparer">The equality comparer to use.</param>
    /// <typeparam name="T">The type of elements in the list.</typeparam>
    /// <returns>The new list with distinct elements from the source list.</returns>
    public static List<T> Distinct<T>(this List<T> source, IEqualityComparer<T> equalityComparer)
    {
        HashSet<T> hash = new(capacity: source.Count / 10, comparer: equalityComparer);
        List<T> output = new(capacity: source.Count / 10);
        
        for (int index = 0; index < source.Count; index++)
        {
            T item = source[index];
            bool result = hash.Add(item);

            if (result == false)
                output.Add(item);
        }

        return output;
    }

    /// <summary>
    /// Creates a new array with distinct elements from the source array.
    /// </summary>
    /// <param name="source">The array to de-duplicate.</param>
    /// <typeparam name="T">The type of elements in the array.</typeparam>
    /// <returns>The new array with distinct elements from the source array.</returns>
    public static T[] Distinct<T>(this T[] source)
        => Distinct(source, EqualityComparer<T>.Default);
    
    /// <summary>
    /// Creates a new array with distinct elements from the source array.
    /// </summary>
    /// <param name="source">The array to de-duplicate.</param>
    /// <typeparam name="T">The type of elements in the array.</typeparam>
    /// <param name="equalityComparer">The equality comparer to use.</param>
    /// <returns>The new array with distinct elements from the source array.</returns>
    public static T[] Distinct<T>(this T[] source, IEqualityComparer<T> equalityComparer)
    {
        HashSet<T> hash = new(capacity: source.Length / 10, comparer: equalityComparer);
        T[] output = new T[source.Length];

        int count = 0;

        for (int index = 0; index < source.Length; index++)
        {
            T item = source[index];
            
            bool result = hash.Add(item);

            if (result == false)
            {
                output[count] = source[index];
                count++;
            }
        }
        
        Array.Resize(ref output, count);
        
        return output;
    }
}