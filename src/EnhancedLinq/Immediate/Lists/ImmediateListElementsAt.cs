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

using System.Collections.Generic;

namespace AlastairLundy.EnhancedLinq.Immediate;

public static partial class EnhancedLinqImmediate
{
    /// <summary>
    /// Returns a List of elements from the specified source, 
    /// where the index of each element in the returned List corresponds to an index in the provided indexes.
    /// </summary>
    /// <remarks>The order of the elements in the returned List is determined by their original position in the source,
    /// but the order within the returned List is based on the provided indexes.</remarks>
    /// <param name="source">The List from which to retrieve elements.</param>
    /// <param name="indices">A sequence of indices, where each index corresponds to an element in the source.</param>
    /// <typeparam name="T">The type of the elements in the source and returned List.</typeparam>
    /// <returns>A new List containing the elements at the specified indexes from the original source.</returns>
    public static List<T> ElementsAt<T>(this List<T> source, List<int> indices)
    {
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
    
    /// <summary>
    /// Returns an array of elements from the specified source, 
    /// where the index of each element in the returned array corresponds to an index in the provided indexes.
    /// </summary>
    /// <remarks>The order of the elements in the returned array is determined by their original position in the source,
    /// but the order within the returned array is based on the provided indexes.</remarks>
    /// <param name="source">The array from which to retrieve elements.</param>
    /// <param name="indices">An array of indices, where each index corresponds to an element in the source.</param>
    /// <typeparam name="T">The type of the elements in the source and returned array.</typeparam>
    /// <returns>A new array containing the elements at the specified indexes from the original source.</returns>
    public static T[] ElementsAt<T>(this T[] source, int[] indices)
    {
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