/*
    EnhancedLinq 
    Copyright (c) 2025-2026 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
    */

namespace EnhancedLinq.Immediate.Lists;

public static partial class EnhancedLinqListImmediate
{
    /// <param name="list">The list to reverse.</param>
    /// <typeparam name="T">The type of elements in the array.</typeparam>
    extension<T>(List<T> list)
    {
        /// <summary>
        /// Reverses a <see cref="List{T}"/>.
        /// </summary>
        /// <returns>The reversed IList.</returns>
        public void Reverse()
        {
            ArgumentNullException.ThrowIfNull(list);
            
            List<T> output = new List<T>(list.Count);

            for (int i = 0; i < output.Count; i++)
            {
                if(list.Count -1 - i >= 0)
                    output.Add(list[list.Count - 1 - i]);
                else
                    break;
            }
        
            list.Clear();
            list.AddRange(output);
        }
    }

    /// <param name="array">The array to reverse.</param>
    /// <typeparam name="T">The type of elements in the array.</typeparam>
    extension<T>(T[] array)
    {
        /// <summary>
        /// Returns a reversed array.
        /// </summary>
        /// <returns>The reversed array.</returns>
        public T[] Reverse()
        {
            ArgumentNullException.ThrowIfNull(array);

            T[] newArray = new T[array.Length];

            for (int i = 0; i < array.Length; i++)
            {
                if(array.Length - 1 - i >= 0)
                    newArray[i] = array[array.Length - 1 - i];
            }
        
            return newArray;
        }
    }

    /// <param name="source">The collection to reverse.</param>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    extension<T>(ICollection<T> source)
    {
        /// <summary>
        /// Returns a reversed collection.
        /// </summary>
        /// <returns>The reversed collection.</returns>
        public ICollection<T> Reverse()
        {
            ArgumentNullException.ThrowIfNull(source);

            T[] newArray = new T[source.Count];

            int index = 0;
        
            foreach (T item in source)
            {
                if(source.Count - 1 - index >= 0)
                    newArray[source.Count - 1 - index] = item;
            }
        
            return newArray;
        }
    }

    /// <param name="source">The list to reverse.</param>
    /// <typeparam name="T">The type of elements in the list.</typeparam>
    extension<T>(IList<T> source)
    {
        /// <summary>
        /// Returns a reversed list.
        /// </summary>
        /// <returns>The reversed list.</returns>
        public IList<T> Reverse()
        {
            ArgumentNullException.ThrowIfNull(source);

            T[] newArray = new T[source.Count];

            int index = 0;
        
            foreach (T item in source)
            {
                if(source.Count - 1 - index >= 0)
                    newArray[source.Count - 1 - index] = item;
            }
        
            return newArray;
        }
    }
}