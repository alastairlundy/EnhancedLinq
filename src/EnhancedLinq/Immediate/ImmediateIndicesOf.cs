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

namespace AlastairLundy.EnhancedLinq.Immediate;

public static partial class EnhancedLinqImmediate
{
    /// <summary>
    /// Retrieves a collection of indices where the specified item can be found within the given producer-consumer collection.
    /// </summary>
    /// <param name="collection">The producer-consumer collection to search.</param>
    /// <param name="item">The item to find and return its indices for.</param>
    /// <typeparam name="T">The type of elements contained within the collection.</typeparam>
    /// <returns>A concurrent bag containing the indices where the specified item can be found, or empty if not found.</returns>
    public static IProducerConsumerCollection<int> IndicesOf<T>(this IProducerConsumerCollection<T> collection, T item)
    {
        ConcurrentBag<int> output = new ConcurrentBag<int>();
        
        int index = 0;
        foreach (T obj in collection)
        {
            if (item is not null && item.Equals(obj))
            {
                output.Add(index);
            }
            index++;
        }
        
        return output;
    }
    
    /// <summary>
    /// Retrieves a collection of indices where the specified item can be found within the given ICollection.
    /// </summary>
    /// <param name="source">The collection to search.</param>
    /// <param name="item">The item to find and return its indices for.</param>
    /// <typeparam name="T">The type of elements contained within the collection.</typeparam>
    /// <returns>A collection containing the indices where the specified item can be found, or empty if not found.</returns>
    public static ICollection<int> IndicesOf<T>(this ICollection<T> source, T item)
    {
        List<int> output = new List<int>();
        
        int index = 0;
        foreach (T obj in source)
        {
            if (item is not null && item.Equals(obj))
            {
                output.Add(index);
            }
            index++;
        }
        
        return output;
    }
    
    /// <summary>
    /// Retrieves a list of indices where the specified item can be found within the given List.
    /// </summary>
    /// <param name="source">The list to search.</param>
    /// <param name="item">The item to find and return its indices for.</param>
    /// <typeparam name="T">The type of elements contained within the list.</typeparam>
    /// <returns>A list containing the indices where the specified item can be found, or empty if not found.</returns>
    public static List<int> IndicesOf<T>(this List<T> source, T item)
    {
        List<int> indices = new List<int>();
        
        int index = 0;

        foreach (T obj in source)
        {
            if (obj is not null && obj.Equals(item))
            {
                indices.Add(index);
            }
        }

        return indices;
    }
    
    /// <summary>
    /// Retrieves an array of indices where the specified item can be found within the given array.
    /// </summary>
    /// <param name="source">The array to search.</param>
    /// <param name="item">The item to find and return its indices for.</param>
    /// <typeparam name="T">The type of elements contained within the array.</typeparam>
    /// <returns>An array containing the indices where the specified item can be found, or an empty array if not found.</returns>
    public static int[] IndicesOf<T>(this T[] source, T item)
    {
       int[] indices = new int[source.Length];

       int count = 0;
       int index = 0;

        foreach (T obj in source)
        {
            if (obj is not null && obj.Equals(item))
            {
                indices[count] = index;
                count++;
            }
        }
        
        Array.Resize(ref indices, count);

        return indices;
    }
    
    /// <summary>
    /// Retrieves a list of indices where the specified character can be found within the given List of chars.
    /// </summary>
    /// <param name="source">The list of characters to search.</param>
    /// <param name="c">The character to find and return its indices for.</param>
    /// <returns>A list containing the indices where the specified character can be found, or empty if not found.</returns>
    public static List<int> IndicesOf(this List<char> source, char c)
    {
        List<int> indices = new List<int>();
        
        int index = 0;

        foreach (char obj in source)
        {
            if (obj == c)
            {
                indices.Add(index);
            }
        }

        return indices;
    }

    /// <summary>
    /// Retrieves an array of indices where the specified character can be found within the given string.
    /// </summary>
    /// <param name="source">The string to search.</param>
    /// <param name="c">The character to find and return its indices for.</param>
    /// <returns>An array containing the indices where the specified character can be found, or an empty array if not found.</returns>
    public static int[] IndicesOf(this string source, char c)
    {
        int[] indices = new int[source.Length];

        int count = 0;
        int index = 0;

        foreach (char obj in source)
        {
            if (obj == c)
            {
                indices[count] = index;
                count++;
            }
        }
        
        Array.Resize(ref indices, count);

        return indices;
    }
}