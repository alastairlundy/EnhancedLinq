/*
    EnhancedLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/.
 */

using System;
using System.Collections.Generic;

using AlastairLundy.DotExtensions.Collections;

using AlastairLundy.EnhancedLinq.Immediate.Linq;
using AlastairLundy.EnhancedLinq.Internals.Localizations;

using EnhancedLinq.Immediate.Ranges;

namespace AlastairLundy.EnhancedLinq.Immediate.Ranges;

/// <summary>
/// 
/// </summary>
public static partial class EnhancedLinqImmediateRange
{
    /// <summary>
    /// Inserts a specified range of elements from another sequence into this collection at a specified position.
    /// </summary>
    /// <param name="source">The collection into which to insert the new elements.</param>
    /// <param name="index">The zero-based index where the new elements will be inserted. If less than 0, values are inserted at the end of the collection.</param>
    /// <param name="values">The sequence of elements to be inserted into the collection.</param>
    /// <typeparam name="T">The type of elements in the value sequence and the collection.</typeparam>
    /// <exception cref="IndexOutOfRangeException">Thrown if the specified index is out of range for this collection.</exception>
    public static void InsertRange<T>(this ICollection<T> source, int index, IEnumerable<T> values)
    {
        ArgumentNullException.ThrowIfNull(source);
        
        if (index < 0 || index > source.Count)
        {
            throw new IndexOutOfRangeException(Resources.Exceptions_IndexOutOfRange
                .Replace("{x}", $"{index}")
                .Replace("{y}", $"0")
                .Replace("z", $"{source.Count}"));
        }

        int numberToRemove = source.LastIndex() - index;

        ICollection<T> itemsToRemove = source.Take(numberToRemove);
        
        source.RemoveRange(index, numberToRemove);;
       
        source.AddRange(values);
        source.AddRange(itemsToRemove);
    }
    
    /// <summary>
    /// Inserts a specified range of elements from another sequence into this list at a specified position.
    /// </summary>
    /// <param name="source">The list into which to insert the new elements.</param>
    /// <param name="index">The zero-based index where the new elements will be inserted. If less than 0, values are inserted at the end of the list.</param>
    /// <param name="values">The sequence of elements to be inserted into the list.</param>
    /// <typeparam name="T">The type of elements in the value sequence and the list.</typeparam>
    /// <exception cref="IndexOutOfRangeException">Thrown if the specified index is out of range for this list.</exception>
    /// <exception cref="OverflowException">Thrown if the list overflows with the new elements.</exception>
    public static void InsertRange<T>(this IList<T> source, int index, IEnumerable<T> values)
    {
        ArgumentNullException.ThrowIfNull(source);

        
        if (index < 0 || index > source.Count)
        {
            throw new IndexOutOfRangeException(Resources.Exceptions_IndexOutOfRange
                .Replace("{x}", $"{index}")
                .Replace("{y}", $"0")
                .Replace("z", $"{source.Count}"));
        }
            
        int newIndex = index;

        foreach (T value in values)
        {
            if (newIndex >= source.Count)
            {
                source.Add(value);       
            }
            else
            {
                source.Insert(newIndex, value);
            }
                
            newIndex++;
        }
    }
}