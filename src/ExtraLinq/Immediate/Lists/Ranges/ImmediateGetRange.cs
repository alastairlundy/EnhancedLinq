/*
    ExtraLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/.
 */

using System;
using System.Collections.Generic;

using ExtraLinq.Internals.Localizations;

namespace ExtraLinq.Immediate.Ranges;

public static partial class ExtraLinqImmediateRange
{
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
        if (list.Count >= startIndex + count)
        {
            throw new IndexOutOfRangeException(Resources.Exceptions_IndexOutOfRange
                .Replace("{x}", $"{count}")
                .Replace("{y}", "0")
                .Replace("{z}", $"{list.Count}"));
        }
        
        List<T> output = new List<T>();
        
        int limit = startIndex + count;

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
}