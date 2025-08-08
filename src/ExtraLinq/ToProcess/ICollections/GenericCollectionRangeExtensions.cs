/*
        MIT License

       Copyright (c) 2024-2025 Alastair Lundy

       Permission is hereby granted, free of charge, to any person obtaining a copy
       of this software and associated documentation files (the "Software"), to deal
       in the Software without restriction, including without limitation the rights
       to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
       copies of the Software, and to permit persons to whom the Software is
       furnished to do so, subject to the following conditions:

       The above copyright notice and this permission notice shall be included in all
       copies or substantial portions of the Software.

       THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
       IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
       FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
       AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
       LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
       OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
       SOFTWARE.
   */

using System.Collections.Generic;
using ExtraLinq.ToProcess.ILists;

namespace ExtraLinq.ToProcess.ICollections;

/// <summary>
/// 
/// </summary>
public static class GenericCollectionRangeExtensions
{

    /// <summary>
    /// Appends elements from another collection to the end of the specified collection.
    /// </summary>
    /// <param name="source">The collection into which elements will be appended.</param>
    /// <param name="enumerableToAdd">The IEnumerable containing elements to append to the original collection.</param>
    /// <typeparam name="T">The type of elements in both collections.</typeparam>
    public static void AddRange<T>(this ICollection<T> source, IEnumerable<T> enumerableToAdd)
    {
        if (source is IList<T> list1)
        {
            IListRangeExtensions.AddRange(list1, enumerableToAdd);
            return;
        }

        foreach (T item in enumerableToAdd)
        {
            source.Add(item);
        }
    }
        
    /// <summary>
    /// Appends elements from another collection to the end of the specified collection.
    /// </summary>
    /// <param name="source">The collection into which elements will be appended.</param>
    /// <param name="collectionToAdd">The collection containing elements to append to the original collection.</param>
    /// <typeparam name="T">The type of elements in both collections.</typeparam>
    public static void AddRange<T>(this ICollection<T> source, ICollection<T> collectionToAdd)
    {
        if (source is IList<T> list && collectionToAdd is IList<T> listToAdd)
        {
            IListRangeExtensions.AddRange(list, listToAdd);
            return;
        }

        foreach (T item in collectionToAdd)
        {
            source.Add(item);
        }
    }
    
}