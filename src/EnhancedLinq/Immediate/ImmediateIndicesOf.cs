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
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace AlastairLundy.EnhancedLinq.Immediate;

public static partial class EnhancedLinqImmediate
{
    /// <param name="collection">The producer-consumer collection to search.</param>
    /// <typeparam name="T">The type of elements contained within the collection.</typeparam>
    extension<T>(IProducerConsumerCollection<T> collection)
    {
        /// <summary>
        /// Retrieves a collection of indices where the specified item can be found within the given producer-consumer collection.
        /// </summary>
        /// <param name="item">The item to find and return its indices for.</param>
        /// <returns>A concurrent bag containing the indices where the specified item can be found, or empty if not found.</returns>
        public IProducerConsumerCollection<int> IndicesOf(T item)
        {
            ConcurrentBag<int> output = new ConcurrentBag<int>();
        
            int index = 0;
            foreach (T obj in collection)
            {
                if (item is not null && item.Equals(obj))
                {
                    output.Add(index);
                }
                index++;
            }
        
            return output;
        }
    }

    /// <param name="source">The collection to search.</param>
    /// <typeparam name="T">The type of elements contained within the collection.</typeparam>
    extension<T>(ICollection<T> source)
    {
        /// <summary>
        /// Retrieves a collection of indices where the specified item can be found within the given ICollection.
        /// </summary>
        /// <param name="item">The item to find and return its indices for.</param>
        /// <returns>A collection containing the indices where the specified item can be found, or empty if not found.</returns>
        public ICollection<int> IndicesOf(T item)
        {
            List<int> output = new List<int>();
        
            int index = 0;
            foreach (T obj in source)
            {
                if (item is not null && item.Equals(obj))
                {
                    output.Add(index);
                }
                index++;
            }
        
            return output;
        }
    }

    /// <param name="source">The list to search.</param>
    /// <typeparam name="T">The type of elements contained within the list.</typeparam>
    extension<T>(List<T> source)
    {
        /// <summary>
        /// Retrieves a list of indices where the specified item can be found within the given List.
        /// </summary>
        /// <param name="item">The item to find and return its indices for.</param>
        /// <returns>A list containing the indices where the specified item can be found, or empty if not found.</returns>
        public List<int> IndicesOf(T item)
        {
            List<int> indices = new List<int>();
        
            int index = 0;

            foreach (T obj in source)
            {
                if (obj is not null && obj.Equals(item))
                {
                    indices.Add(index);
                }
            }

            return indices;
        }
    }

    /// <param name="source">The array to search.</param>
    /// <typeparam name="T">The type of elements contained within the array.</typeparam>
    extension<T>(T[] source)
    {
        /// <summary>
        /// Retrieves an array of indices where the specified item can be found within the given array.
        /// </summary>
        /// <param name="item">The item to find and return its indices for.</param>
        /// <returns>An array containing the indices where the specified item can be found, or an empty array if not found.</returns>
        public int[] IndicesOf(T item)
        {
            int[] indices = new int[source.Length];

            int count = 0;
            int index = 0;

            foreach (T obj in source)
            {
                if (obj is not null && obj.Equals(item))
                {
                    indices[count] = index;
                    count++;
                }
            }
        
            Array.Resize(ref indices, count);

            return indices;
        }
    }

    /// <param name="source">The list of characters to search.</param>
    extension(List<char> source)
    {
        /// <summary>
        /// Retrieves a list of indices where the specified character can be found within the given List of chars.
        /// </summary>
        /// <param name="c">The character to find and return its indices for.</param>
        /// <returns>A list containing the indices where the specified character can be found, or empty if not found.</returns>
        public List<int> IndicesOf(char c)
        {
            List<int> indices = new List<int>();
        
            int index = 0;

            foreach (char obj in source)
            {
                if (obj == c)
                {
                    indices.Add(index);
                }
            }

            return indices;
        }
    }

    /// <param name="source">The string to search.</param>
    extension(string source)
    {
        /// <summary>
        /// Retrieves an array of indices where the specified character can be found within the given string.
        /// </summary>
        /// <param name="c">The character to find and return its indices for.</param>
        /// <returns>An array containing the indices where the specified character can be found, or an empty array if not found.</returns>
        public int[] IndicesOf(char c)
        {
            int[] indices = new int[source.Length];

            int count = 0;
            int index = 0;

            foreach (char obj in source)
            {
                if (obj == c)
                {
                    indices[count] = index;
                    count++;
                }
            }
        
            Array.Resize(ref indices, count);

            return indices;
        }
    }
}