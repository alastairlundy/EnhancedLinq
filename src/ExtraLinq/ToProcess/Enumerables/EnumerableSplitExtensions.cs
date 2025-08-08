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


using System;
using System.Collections.Generic;
using ExtraLinq.Internals.Localizations;

// ReSharper disable MemberCanBePrivate.Global

namespace ExtraLinq.ToProcess.Enumerables;

/// <summary>
/// 
/// </summary>
public static class EnumerableSplitExtensions
{

    /// <summary>
    /// Splits an IEnumerable based on the maximum number of items allowed in each IEnumerable. 
    /// </summary>
    /// <param name="source">The IEnumerable to be split.</param>
    /// <param name="maxCount">The number of items allowed in each IEnumerable.</param>
    /// <typeparam name="T">The type of item stored in the source IEnumerable.</typeparam>
    /// <returns>An IEnumerable of IEnumerables split by the maximum number of items allowed in each IEnumerable.</returns>
    /// <exception cref="ArgumentException">Thrown if the IEnumerable to be split is empty.</exception>
    public static IEnumerable<IEnumerable<T>> SplitByCount<T>(this IEnumerable<T> source, int maxCount)
    {
        int currentEnumerableCount = 0;
            
        List<T> currentList = new List<T>();

        int sourceCount = 0;
        foreach (T item in source)
        {
            sourceCount++;
            
            if (currentEnumerableCount < maxCount)
            {
                currentList.Add(item);
            }
            else if (currentEnumerableCount == maxCount)
            {
                yield return currentList;
                currentEnumerableCount = 0;
            }
        }
        
        if(sourceCount == 0)
            throw new ArgumentException(Resources.Exceptions_EnumerablesSplit_Empty);
    }
}