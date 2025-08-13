using System;
using System.Collections.Generic;

using ExtraLinq.Internals.Localizations;

// ReSharper disable CheckNamespace

namespace ExtraLinq.Immediate.Ranges;

public static class ImmediateInsertRange
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
}