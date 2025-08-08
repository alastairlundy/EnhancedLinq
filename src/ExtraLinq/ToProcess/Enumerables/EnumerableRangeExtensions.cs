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

namespace ExtraLinq.ToProcess.Enumerables;

/// <summary>
/// 
/// </summary>
public static class EnumerableRangeExtensions
{
    /// <summary>
    /// Adds multiple elements to the specified sequence of elements.
    /// </summary>
    /// <param name="source">The sequence to add items to.</param>
    /// <param name="toBeAdded">The elements to add to the sequence.</param>
    /// <typeparam name="T">The type of element in the sequence and elements being added.</typeparam>
    public static IEnumerable<T> AddRange<T>(this IEnumerable<T> source, IEnumerable<T> toBeAdded)
    {
        foreach (T item in source)
        {
            yield return item;
        }

        foreach (T item in toBeAdded)
        {
            yield return item;
        }
    }
}