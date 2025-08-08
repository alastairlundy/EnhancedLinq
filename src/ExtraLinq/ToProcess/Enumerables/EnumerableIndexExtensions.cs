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

using System.Collections.Generic;
#if NET8_0_OR_GREATER
#endif

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable SuggestVarOrType_BuiltInTypes

namespace ExtraLinq.ToProcess.Enumerables;

/// <summary>
/// 
/// </summary>
public static class EnumerableIndexExtensions
{

        
    /// <summary>
    /// Gets the indices of the specified item within an IEnumerable.
    /// </summary>
    /// <param name="source">The IEnumerable to be searched.</param>
    /// <param name="obj">The item to search for.</param>
    /// <typeparam name="T"></typeparam>
    /// <returns>The indices if the object is found; an empty sequence otherwise.</returns>
    public static IEnumerable<int> IndicesOf<T>(this IEnumerable<T> source, T obj)
    {
        int index = 0;
        
        foreach (T item in source)
        {
            if (obj is not null && obj.Equals(item))
            {
                yield return index;
            }

            index += 1;
        }
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="item"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static int[] IndicesOfAsArray<T>(this IEnumerable<T> source, T item)
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

        return indices.ToArray();
    }
}