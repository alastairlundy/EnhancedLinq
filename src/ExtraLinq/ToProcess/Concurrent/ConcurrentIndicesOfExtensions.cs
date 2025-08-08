/*
        MIT License
       
       Copyright (c) 2024-2025 Alastair Lundy
       
       Permission is hereby granted, free of charge, to any person obtaining a copy
       of this software and associated documentation files (the "Software"), to deal
       in the Software without restriction, including without limitation the rights
       to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
       copies of the Software, and to permit persons to whom the Software is
       furnished to do so, subject to the following conditions:
       
       The above copyright notice and this permission notice shall be included in all
       copies or substantial portions of the Software.
       
       THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
       IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
       FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
       AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
       LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
       OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
       SOFTWARE.
   */

using System.Collections.Concurrent;

namespace ExtraLinq.ToProcess.Concurrent;

/// <summary>
/// 
/// </summary>
public static class ConcurrentIndicesOfExtensions
{
    
    /// <summary>
    /// Retrieves a collection of indices where the specified element can be found within the given collection.
    /// </summary>
    /// <param name="collection">The producer-consumer collection to search.</param>
    /// <param name="item">The item to find and return its indices for.</param>
    /// <typeparam name="T">The type of elements contained within the collection.</typeparam>
    /// <returns>A concurrent bag containing the indices where the specified item can be found, or empty if not found.</returns>
    public static IProducerConsumerCollection<int> IndicesOf<T>(this IProducerConsumerCollection<T> collection, T item)
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