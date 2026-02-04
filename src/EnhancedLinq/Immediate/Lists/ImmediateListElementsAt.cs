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
    /// <param name="source">The List from which to retrieve elements.</param>
    /// <typeparam name="T">The type of the elements in the source and returned List.</typeparam>
    extension<T>(List<T> source)
    {
        /// <summary>
        /// Returns a List of elements from the specified source, 
        /// where the index of each element in the returned List corresponds to an index in the provided indexes.
        /// </summary>
        /// <remarks>The order of the elements in the returned List is determined by their original position in the source,
        /// but the order within the returned List is based on the provided indexes.</remarks>
        /// <param name="indices">A sequence of indices, where each index corresponds to an element in the source.</param>
        /// <returns>A new List containing the elements at the specified indexes from the original source.</returns>
        public List<T> ElementsAt(List<int> indices)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(indices);
            
            List<T> output = new();
        
            for (int i = 0; i < indices.Count; i++)
            {
                int index = indices[i];

                if (index >= 0 && index < source.Count)
                {
                    output.Add(source[index]);
                }
            }
            
            return output;
        }
    }

    /// <param name="source">The array from which to retrieve elements.</param>
    /// <typeparam name="T">The type of the elements in the source and returned array.</typeparam>
    extension<T>(T[] source)
    {
        /// <summary>
        /// Returns an array of elements from the specified source, 
        /// where the index of each element in the returned array corresponds to an index in the provided indexes.
        /// </summary>
        /// <remarks>The order of the elements in the returned array is determined by their original position in the source,
        /// but the order within the returned array is based on the provided indexes.</remarks>
        /// <param name="indices">An array of indices, where each index corresponds to an element in the source.</param>
        /// <returns>A new array containing the elements at the specified indexes from the original source.</returns>
        public T[] ElementsAt(int[] indices)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(indices);

            T[] output = new T[indices.Length];
            int count = 0;
        
            for (int i = 0; i < indices.Length; i++)
            {
                int index = indices[i];

                if (index >= 0 && index < source.Length)
                {
                    output[count] = source[index];
                }
            }
            
            return output;
        }
    }
}