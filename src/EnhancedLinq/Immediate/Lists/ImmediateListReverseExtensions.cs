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