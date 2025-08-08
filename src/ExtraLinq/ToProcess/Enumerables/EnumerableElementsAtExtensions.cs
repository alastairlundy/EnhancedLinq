/*
        MIT License
       
       Copyright (c) 2025 Alastair Lundy
       
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

using System.Collections.Generic;
using System.Linq;
using ExtraLinq.ToProcess.ILists;

namespace ExtraLinq.ToProcess.Enumerables;

/// <summary>
/// 
/// </summary>
public static class EnumerableElementsAtExtensions
{
    
    /// <summary>
    /// Returns a IEnumerable of elements from the specified source, 
    /// where the index of each element in the returned IEnumerable corresponds to an index in the provided indexes.
    /// </summary>
    /// <remarks>The order of the elements in the returned IEnumerable is determined by their original position in the source,
    /// but the order within the returned IEnumerable is based on the provided indexes.</remarks>
    /// <param name="source">The IEnumerable from which to retrieve elements.</param>
    /// <param name="indices">A sequence of indices, where each index corresponds to an element in the source.</param>
    /// <typeparam name="T">The type of the elements in the source and returned IEnumerable.</typeparam>
    /// <returns>A new IEnumerable containing the elements at the specified indexes from the original source.</returns>
    public static IEnumerable<T> ElementsAt<T>(this IEnumerable<T> source, IEnumerable<int> indices)
    {
        if (source is IList<T> list)
        {
            return IListElementsAtExtensions.ElementsAt(list, indices);
        }

        return (from index in indices
            select source.ElementAt(index));
    }
}