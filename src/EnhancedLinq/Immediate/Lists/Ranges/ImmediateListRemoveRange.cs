/*
      EnhancedLinq 
      Copyright (c) 2025 Alastair Lundy
      
     Licensed under the Apache License, Version 2.0 (the "License");
     you may not use this file except in compliance with the License.
     You may obtain a copy of the License at

         http://www.apache.org/licenses/LICENSE-2.0

     Unless required by applicable law or agreed to in writing, software
     distributed under the License is distributed on an "AS IS" BASIS,
     WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
     See the License for the specific language governing permissions and
     limitations under the License.
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
    /// <summary>
    /// Removes a specified range of elements from this collection.
    /// </summary>
    /// <param name="source">The collection from which to remove elements.</param>
    /// <param name="startIndex">The zero-based index (inclusive) where the removal starts.
    /// If less than 0, an ArgumentException is thrown.</param>
    /// <param name="count">The number of elements to be removed.
    /// If greater than or equal to the remaining elements at start index, an IndexOutOfRangeException is thrown.</param>
    /// <typeparam name="T">The type of elements in this collection.</typeparam>
    /// <exception cref="IndexOutOfRangeException">Thrown if the start index is out of range for this collection or if the count exceeds available elements from that index.</exception>
    /// <exception cref="ArgumentException">Thrown if the start index is negative.</exception>
    public static void RemoveRange<T>(this ICollection<T> source, int startIndex, int count)
    {
        if (source.IsReadOnly || source is T[])
            throw new NotSupportedException();
        
        if (startIndex >= source.Count || startIndex < 0 )
            throw new IndexOutOfRangeException(Resources.Exceptions_IndexOutOfRange
                .Replace("{x}", $"{startIndex}")
                .Replace("{y}", "0")
                .Replace("{z}", $"{source.Count}"));

        if(count < 0)
            throw new ArgumentException(Resources.Exceptions_Count_LessThanZero);

        if ((source.Count < startIndex + count) == false)
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
}