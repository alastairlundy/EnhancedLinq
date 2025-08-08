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
using System.Collections.Generic;
using System.Linq;

namespace ExtraLinq.ToProcess.Concurrent;

/// <summary>
/// 
/// </summary>
public static class ConcurrentRemoveExtensions
{
    
    /// <summary>
    /// Removes all occurrences of a specified object from a concurrent bag.
    /// </summary>
    /// <param name="concurrentBag">The concurrent bag to remove objects from.</param>
    /// <param name="obj">The object to be removed from the concurrent bag.</param>
    /// <typeparam name="T">The type of elements contained within the concurrent bag.</typeparam>
    /// <returns>A new concurrent bag with all occurrences of the specified object removed.</returns>
    public static ConcurrentBag<T> Remove<T>(this ConcurrentBag<T> concurrentBag, T obj)
    {
        IEnumerable<T> newCollection = from item in concurrentBag
            where item.Equals(obj) == false
            select item;
        
        return new ConcurrentBag<T>(newCollection);
    }
}