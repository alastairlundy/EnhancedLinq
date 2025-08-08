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

// ReSharper disable InconsistentNaming

namespace ExtraLinq.ToProcess.ILists;

/// <summary>
/// 
/// </summary>
public static class IListElementsAtExtensions
{
    /// <summary>
    /// Returns a IList of elements from the specified source, 
    /// where the index of each element in the returned IList corresponds to an index in the provided indexes.
    /// </summary>
    /// <remarks>The order of the elements in the returned IList is determined by their original position in the source,
    /// but the order within the returned IList is based on the provided indexes.</remarks>
    /// <param name="source">The IList from which to retrieve elements.</param>
    /// <param name="indices">A sequence of indices, where each index corresponds to an element in the source.</param>
    /// <typeparam name="T">The type of the elements in the source and returned IList.</typeparam>
    /// <returns>A new IList containing the elements at the specified indexes from the original source.</returns>
    public static IList<T> ElementsAt<T>(this IList<T> source, IEnumerable<int> indices)
    {
        IList<int> indicesList = indices as IList<int> ?? [..indices];

        return ElementsAt(source, indicesList);
    }
    
    /// <summary>
    /// Returns a IList of elements from the specified source, 
    /// where the index of each element in the returned IList corresponds to an index in the provided indexes.
    /// </summary>
    /// <remarks>The order of the elements in the returned IList is determined by their original position in the source,
    /// but the order within the returned IList is based on the provided indexes.</remarks>
    /// <param name="source">The IList from which to retrieve elements.</param>
    /// <param name="indices">A sequence of indices, where each index corresponds to an element in the source.</param>
    /// <typeparam name="T">The type of the elements in the source and returned IList.</typeparam>
    /// <returns>A new IList containing the elements at the specified indexes from the original source.</returns>
    public static IList<T> ElementsAt<T>(this IList<T> source, IList<int> indices)
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
}