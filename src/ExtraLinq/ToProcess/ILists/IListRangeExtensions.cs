/*
        MIT License
       
       Copyright (c) 2025 Alastair Lundy
       
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

using System;
using System.Collections.Generic;
using ExtraLinq.Internals.Localizations;

// ReSharper disable InconsistentNaming

namespace ExtraLinq.ToProcess.ILists;

/// <summary>
/// 
/// </summary>
public static class IListRangeExtensions
{

    /// <summary>
    /// Inserts a specified range of elements from another sequence into this list at a specified position.
    /// </summary>
    /// <param name="list">The list into which to insert the new elements.</param>
    /// <param name="index">The zero-based index where the new elements will be inserted. If less than 0, values are inserted at the end of the list.</param>
    /// <param name="values">The sequence of elements to be inserted into the list.</param>
    /// <typeparam name="T">The type of elements in the value sequence and the list.</typeparam>
    /// <exception cref="IndexOutOfRangeException">Thrown if the specified index is out of range for this list.</exception>
    /// <exception cref="OverflowException">Thrown if the list overflows with the new elements.</exception>
    public static void InsertRange<T>(this IList<T> list, int index, IEnumerable<T> values)
    {
        if (index < 0 || index > list.Count)
        {
            throw new IndexOutOfRangeException(Resources.Exceptions_IndexOutOfRange
                .Replace("{x}", $"{index}")
                .Replace("{y}", $"0")
                .Replace("z", $"{list.Count}"));
        }
            
        int newIndex = index;

        foreach (T value in values)
        {
            if (newIndex >= list.Count)
            {
                list.Add(value);       
            }
            else
            {
                list.Insert(newIndex, value);
            }
                
            newIndex++;
        }
    }
        
    /// <summary>
    /// Returns a new list containing elements from this list at the specified start index to a distance of 'count' elements.
    /// </summary>
    /// <param name="list">The source list from which to extract the range.</param>
    /// <param name="startIndex">The starting index (inclusive) of the range in the original list.</param>
    /// <param name="count">The number of elements to include in the returned range.</param>
    /// <typeparam name="T">The type of elements in this list and the returned list.</typeparam>
    /// <returns>A new list containing the specified range of elements from the source list.</returns>
    /// <exception cref="IndexOutOfRangeException">Thrown if the start index is out of range for the original list.</exception>
    public static IList<T> GetRange<T>(this IList<T> list, int startIndex, int count)
    {
        List<T> output = new List<T>();
        int limit;
            
        if (list.Count < startIndex + count)
        {
            limit = startIndex + count;
        }
        else
        {
            throw new IndexOutOfRangeException(Resources.Exceptions_IndexOutOfRange
                .Replace("{x}", $"{count}")
                .Replace("{y}", "0")
                .Replace("{z}", $"{list.Count}"));
        }
                
        for (int index = startIndex; index < limit; index++)
        {
            output.Add(list[index]);
        }
                
        return output;
    }
        
        
    /// <summary>
    /// Retrieves a specified range of elements from the source list.
    /// 
    /// The indices are 0-based, meaning the first element is at index 0 and the last element is at index Count - 1.
    /// </summary>
    /// <param name="source">The list from which to retrieve the range of elements.</param>
    /// <param name="indices">A collection of 0-based indices specifying the range of elements to retrieve.</param>
    /// <typeparam name="T">The type of elements in the source list.</typeparam>
    /// <returns>A list containing the specified range of elements.</returns>
    /// <exception cref="IndexOutOfRangeException">Thrown if any index is out of range
    /// (less than 0 or greater than or equal to Count).</exception>
    public static IList<T> GetRange<T>(this IList<T> source, ICollection<int> indices)
    {
        List<T> output = new();

        foreach (int index in indices)
        {
            if (index < 0 || index >= source.Count)
            {
                throw new IndexOutOfRangeException(Resources.Exceptions_IndexOutOfRange
                    .Replace("{x}", index.ToString())
                    .Replace("{y}", $"0")
                    .Replace("{z}", $"{source.Count}"));
            }

            output.Add(source[index]);
        }

        return output;
    }

    /// <summary>
    /// Removes a specified range of elements from this list.
    /// </summary>
    /// <param name="list">The list from which to remove elements.</param>
    /// <param name="startIndex">The zero-based index (inclusive) where the removal starts.
    /// If less than 0, an ArgumentException is thrown.</param>
    /// <param name="count">The number of elements to be removed.
    /// If greater than or equal to the remaining elements at start index, an IndexOutOfRangeException is thrown.</param>
    /// <typeparam name="T">The type of elements in this list.</typeparam>
    /// <returns>This list with the specified range of elements removed.</returns>
    /// <exception cref="IndexOutOfRangeException">Thrown if the start index is out of range for this list or if the count exceeds available elements from that index.</exception>
    /// <exception cref="ArgumentException">Thrown if the start index is negative.</exception>
    public static void RemoveRange<T>(this IList<T> list, int startIndex, int count)
    {
        int limit;
            
        if (list.Count < startIndex + count)
        {
            limit = startIndex + count;
        }
        else if (startIndex >= list.Count || startIndex < 0 ) 
        {
            throw new IndexOutOfRangeException(Resources.Exceptions_IndexOutOfRange
                .Replace("{x}", $"{startIndex}")
                .Replace("{y}", "0")
                .Replace("{z}", $"{list.Count}"));
        }
        else
        {
            throw new ArgumentException(Resources.Exceptions_Count_LessThanZero);
        }
            
        for (int index = startIndex; index < limit; index++)
        {
            list.RemoveAt(index);
        }
    }
        
    
    /// <summary>Removes a range of elements from the specified list.
    ///
    /// <para>
    /// If the range of indices is empty, no elements will be removed.
    ///</para>
    /// </summary>
    /// <param name="list">The list from which to remove elements.</param>
    /// <param name="indices">A list of 0-based indices specifying the range of elements to remove.</param>
    /// <typeparam name="T">The type of elements in the list.</typeparam>
    /// <exception cref="IndexOutOfRangeException">Thrown if any index in the indices list is out of range for the corresponding element in the list.</exception>
    public static void RemoveRange<T>(this IList<T> list, IList<int> indices)
    {
        foreach (int index in indices)
        {
            if (index >= list.Count || index < 0 ) 
            {
                throw new IndexOutOfRangeException(Resources.Exceptions_IndexOutOfRange
                    .Replace("{x}", $"{index}")
                    .Replace("{y}", "0")
                    .Replace("{z}", $"{list.Count}"));
            }
            else if (indices.Count > list.Count)
            {
                throw new ArgumentException(Resources.Exceptions_Count_LessThanZero);
            }
            
            list.RemoveAt(index);
        }
    }
}