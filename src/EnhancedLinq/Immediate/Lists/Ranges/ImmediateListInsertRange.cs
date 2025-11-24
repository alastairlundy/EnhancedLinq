/*
    EnhancedLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
    */

using System;
using System.Collections.Generic;

using AlastairLundy.EnhancedLinq.Immediate.Linq;
using AlastairLundy.EnhancedLinq.Internals.Localizations;

namespace AlastairLundy.EnhancedLinq.Immediate.Ranges;

/// <summary>
/// Provides functionality to work with immediate insertion of ranges of items into collections and lists.
/// </summary>
public static partial class EnhancedLinqImmediateRange
{
    /// <param name="source">The collection into which to insert the new elements.</param>
    /// <typeparam name="T">The type of elements in the value sequence and the collection.</typeparam>
    extension<T>(ICollection<T> source)
    {
        /// <summary>
        /// Inserts a specified range of elements from another sequence into this collection at a specified position.
        /// </summary>
        /// <param name="index">The zero-based index where the new elements will be inserted. If less than 0, values are inserted at the end of the collection.</param>
        /// <param name="values">The sequence of elements to be inserted into the collection.</param>
        /// <exception cref="IndexOutOfRangeException">Thrown if the specified index is out of range for this collection.</exception>
        public void InsertRange(int index, IEnumerable<T> values)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(index);
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(values);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index, source.Count);

            int numberToRemove = source.LastIndex() - index;

            ICollection<T> itemsToRemove = source.Take(numberToRemove);
        
            source.RemoveRange(index, numberToRemove);;
       
            source.AddRange(values);
            source.AddRange(itemsToRemove);
        }
    }

    /// <param name="source">The list into which to insert the new elements.</param>
    /// <typeparam name="T">The type of elements in the value sequence and the list.</typeparam>
    extension<T>(IList<T> source)
    {
        /// <summary>
        /// Inserts a specified range of elements from another sequence into this list at a specified position.
        /// </summary>
        /// <param name="index">The zero-based index where the new elements will be inserted. If less than 0, values are inserted at the end of the list.</param>
        /// <param name="values">The sequence of elements to be inserted into the list.</param>
        /// <exception cref="IndexOutOfRangeException">Thrown if the specified index is out of range for this list.</exception>
        /// <exception cref="OverflowException">Thrown if the list overflows with the new elements.</exception>
        public void InsertRange(int index, IEnumerable<T> values)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(index);
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(values);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index, source.Count);
            
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
}