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
// ReSharper disable CheckNamespace

namespace ExtraLinq.Immediate.Ranges;

public static class ImmediateRemoveRange
{
    
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
        if (list.IsReadOnly || list is T[])
            throw new NotSupportedException();
        
        if (startIndex >= list.Count || startIndex < 0 )
            throw new IndexOutOfRangeException(Resources.Exceptions_IndexOutOfRange
                .Replace("{x}", $"{startIndex}")
                .Replace("{y}", "0")
                .Replace("{z}", $"{list.Count}"));

        if(count < 0)
            throw new ArgumentException(Resources.Exceptions_Count_LessThanZero);

        if ((list.Count < startIndex + count) == false)
            throw new ArgumentException();
        
        int limit = startIndex + count;
        
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
        if (list.IsReadOnly || list is T[])
            throw new NotSupportedException();
        
        foreach (int index in indices)
        {
            if (index >= list.Count || index < 0 )
                throw new IndexOutOfRangeException(Resources.Exceptions_IndexOutOfRange
                    .Replace("{x}", $"{index}")
                    .Replace("{y}", "0")
                    .Replace("{z}", $"{list.Count}"));

            if (indices.Count > list.Count)
                throw new ArgumentException(Resources.Exceptions_Count_LessThanZero);

            list.RemoveAt(index);
        }
    }
}