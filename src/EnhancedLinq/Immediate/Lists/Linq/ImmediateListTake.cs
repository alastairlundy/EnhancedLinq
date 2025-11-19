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
using System.Collections.Generic;

using AlastairLundy.EnhancedLinq.Internals.Localizations;

namespace AlastairLundy.EnhancedLinq.Immediate.Linq;

/// <summary>
/// A class that represents a collection of methods to extend LINQ capabilities for immediate list/array operations.
/// </summary>
public static partial class EnhancedLinqImmediateList
{
    /// <summary>
    /// Takes the first 'count' elements from the specified source.
    /// </summary>
    /// <param name="source">The source array to extract elements from.</param>
    /// <param name="count">The number of elements to take.</param>
    /// <typeparam name="T">The type of elements in the source collection.</typeparam>
    /// <returns>A new array containing the first 'count' elements from the source.</returns>
    /// <exception cref="ArgumentException">Thrown when the count is less than zero or greater than the length of the source.</exception>
    public static T[] Take<T>(this T[] source, int count)
    {
#if NET8_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(source);
#endif

        if (count < 0 || count > source.Length)
            throw new ArgumentException(Resources.Exceptions_Count_LessThanZero);
        
        T[] output = new T[count];

        for (int index = 0; index < count; index++)
        {
            output[index] = source[index];
        }

        return output;
    }

    /// <summary>
    /// Takes the first 'count' elements from the specified source.
    /// </summary>
    /// <param name="source">The source <see cref="IList{T}"/> to extract elements from.</param>
    /// <param name="count">The number of elements to take.</param>
    /// <typeparam name="T">The type of elements in the source collection.</typeparam>
    /// <returns>A new <see cref="IList{T}"/> containing the first 'count' elements from the source.</returns>
    /// <exception cref="ArgumentException">Thrown when the count is less than zero or greater than the length/size of the source.</exception>
    public static IList<T> Take<T>(this IList<T> source, int count)
    {
#if NET8_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(source);
#endif

        if (count < 0 || count > source.Count)
            throw new ArgumentException(Resources.Exceptions_Count_LessThanZero);
        
        List<T> output = new List<T>(capacity: count);

        for (int index = 0; index < count; index++)
        {
            output.Add(source[index]);
        }

        return output;
    }

    /// <summary>
    /// Takes the first 'count' elements from the specified source.
    /// </summary>
    /// <param name="source">The source <see cref="ICollection{T}"/> to extract elements from.</param>
    /// <param name="count">The number of elements to take.</param>
    /// <typeparam name="T">The type of elements in the source collection.</typeparam>
    /// <returns>A new <see cref="ICollection{T}"/> containing the first 'count' elements from the source.</returns>
    /// <exception cref="ArgumentException">Thrown when the count is less than zero or greater than the length/size of the source.</exception>
    public static ICollection<T> Take<T>(this ICollection<T> source, int count)
    {
#if NET8_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(source);
#endif
        
        if (count < 0 || count > source.Count)
            throw new ArgumentException(Resources.Exceptions_Count_LessThanZero);
        
        List<T> output = new List<T>(capacity: count);

        int index = 0;
        foreach (T item in source)
        {
            if (index <= count)
            {
                output.Add(item);
            }

            index++;
        }

        return output;
    }
}