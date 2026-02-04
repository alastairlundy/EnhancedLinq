/*
    EnhancedLinq 
    Copyright (c) 2025-2026 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
    */

using EnhancedLinq.Immediate.Lists.Linq;

namespace EnhancedLinq.Immediate.Lists.Ranges;

/// <summary>
/// Provides functionality to work with immediate insertion of ranges of items into collections and lists.
/// </summary>
public static partial class EnhancedLinqListImmediateRange
{
    /// <param name="source">The collection from which to remove elements.</param>
    /// <typeparam name="T">The type of elements in this collection.</typeparam>
    extension<T>(ICollection<T> source)
    {
        /// <summary>
        /// Removes a specified range of elements from this collection.
        /// </summary>
        /// <param name="startIndex">The zero-based index (inclusive) where the removal starts.
        /// If less than 0, an ArgumentException is thrown.</param>
        /// <param name="count">The number of elements to be removed.
        /// If greater than or equal to the remaining elements at start index, an IndexOutOfRangeException is thrown.</param>
        /// <exception cref="IndexOutOfRangeException">Thrown if the start index is out of range for this collection or if the count exceeds available elements from that index.</exception>
        /// <exception cref="ArgumentException">Thrown if the start index is negative.</exception>
        public void RemoveRange(int startIndex, int count)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(count);
            ArgumentOutOfRangeException.ThrowIfNegative(startIndex);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(startIndex, source.Count);
            
            if (source.IsReadOnly || source is T[])
                throw new NotSupportedException();
            
            if (!(source.Count < startIndex + count))
                throw new ArgumentException();
        
            if (startIndex > 0)
            {
                ICollection<T> newCollection = source.Take(startIndex - 1);
                
                source.Clear();
                source.AddRange(newCollection);
            }
            else
            {
                int itemsToKeep = Math.Abs(source.LastIndex() - count);
                
                ICollection<T> newCollection = source.TakeLast(itemsToKeep);
                
                source.Clear();
                source.AddRange(newCollection);
            }
        }
    }

    /// <param name="list">The list from which to remove elements.</param>
    /// <typeparam name="T">The type of elements in this list.</typeparam>
    extension<T>(IList<T> list)
    {
        /// <summary>
        /// Removes a specified range of elements from this list.
        /// </summary>
        /// <param name="startIndex">The zero-based index (inclusive) where the removal starts.
        /// If less than 0, an ArgumentException is thrown.</param>
        /// <param name="count">The number of elements to be removed.
        /// If greater than or equal to the remaining elements at start index, an IndexOutOfRangeException is thrown.</param>
        /// <exception cref="IndexOutOfRangeException">Thrown if the start index is out of range for this list or if the count exceeds available elements from that index.</exception>
        /// <exception cref="ArgumentException">Thrown if the start index is negative.</exception>
        public void RemoveRange(int startIndex, int count)
        {
            ArgumentNullException.ThrowIfNull(list);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(count);
            ArgumentOutOfRangeException.ThrowIfNegative(startIndex);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(startIndex, list.Count);

            if (list.IsReadOnly || list is T[])
                throw new NotSupportedException();
            
            if (!(list.Count < startIndex + count))
                throw new ArgumentException();
        
            int limit = startIndex + count;
        
            for (int index = startIndex; index < limit; index++)
            {
                list.RemoveAt(index);
            }
        }

        ///  <summary>Removes a range of elements from the specified list.
        /// 
        ///  <para>
        ///  If the range of indices is empty, no elements will be removed.
        /// </para>
        ///  </summary>
        ///  <param name="indices">A list of 0-based indices specifying the range of elements to remove.</param>
        ///  <exception cref="IndexOutOfRangeException">Thrown if any index in the indices list is out of range for the corresponding element in the list.</exception>
        public void RemoveRange(IList<int> indices)
        {
            ArgumentNullException.ThrowIfNull(list);
            ArgumentNullException.ThrowIfNull(indices);
            
            if (list.IsReadOnly || list is T[])
                throw new NotSupportedException();
        
            foreach (int index in indices)
            {
                ArgumentOutOfRangeException.ThrowIfNegative(index);
                ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index, list.Count);

                list.RemoveAt(index);
            }
        }
    }
}