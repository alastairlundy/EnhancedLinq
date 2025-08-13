using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace ExtraLinq.Immediate;

public static class ImmediateIndicesOf
{
    /// <summary>
    /// Retrieves a collection of indices where the specified element can be found within the given collection.
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
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="item"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
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
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="item"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
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
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="item"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
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
}