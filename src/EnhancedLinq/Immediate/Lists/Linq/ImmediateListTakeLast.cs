/*
    EnhancedLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/.
 */

using System;
using System.Collections.Generic;

using AlastairLundy.EnhancedLinq.Internals.Localizations;

namespace AlastairLundy.EnhancedLinq.Immediate.Linq;


public static partial class EnhancedLinqImmediateList
{
    /// <summary>
    /// Takes the last 'count' elements from a source array and returns them as a new array.
    /// </summary>
    /// <param name="source">The source array.</param>
    /// <param name="count">The number of elements to take from the end of the source array.</param>
    /// <typeparam name="T">The type of elements in the source array.</typeparam>
    /// <returns>An array containing the last 'count' elements from the source array.</returns>
    /// <exception cref="ArgumentException">Thrown when count is less than 0 or greater than the length of the source array.</exception>
    public static T[] TakeLast<T>(this T[] source, int count)
    {
#if NET8_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(source);
#endif
        
        if (count < 0 || count > source.Length)
            throw new ArgumentException(Resources.Exceptions_Count_LessThanZero);
        
        T[] output = new T[count];
        
        for (int index = source.Length - 1; index > count; index--)
        {
            output[index] = source[index];
        }

        return output;
    }

    /// <summary>
    /// Takes the last 'count' elements from a source <see cref="IList{T}"/> and returns them as a new <see cref="IList{T}"/>.
    /// </summary>
    /// <param name="source">The source <see cref="IList{T}"/>.</param>
    /// <param name="count">The number of elements to take from the end of the source.</param>
    /// <typeparam name="T">The type of elements in the source.</typeparam>
    /// <returns>A <see cref="IList{T}"/> containing the last 'count' elements from the source.</returns>
    /// <exception cref="ArgumentException">Thrown when count is less than 0 or greater than the length/count of the source <see cref="IList{T}"/>.</exception>
    public static IList<T> TakeLast<T>(this IList<T> source, int count)
    {
#if NET8_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(source);
#endif
        
        if (count < 0 || count > source.Count)
            throw new ArgumentException(Resources.Exceptions_Count_LessThanZero);
        
        List<T> output = new List<T>(capacity: count);

        for (int index = source.Count - 1; index > count; index++)
        {
            output.Add(source[index]);
        }

        return output;
    }


    /// <summary>
    /// Takes the last 'count' elements from a source collection and returns them as a new collection.
    /// </summary>
    /// <param name="source">The source collection.</param>
    /// <param name="count">The number of elements to take from the end of the source collection.</param>
    /// <typeparam name="T">The type of elements in the source collection.</typeparam>
    /// <returns>A collection containing the last 'count' elements from the source collection.</returns>
    /// <exception cref="ArgumentException">Thrown when count is less than 0 or greater than the length/count of the source collection.</exception>
    public static ICollection<T> TakeLast<T>(this ICollection<T> source, int count)
    {
#if NET8_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(source);
#endif
        
        if (count < 0 || count > source.Count)
            throw new ArgumentException(Resources.Exceptions_Count_LessThanZero);
        
        List<T> output = new List<T>(capacity: count);

       ICollection<T> reversedCollection = source.Reverse();
        
        int index = 0;
        foreach (T item in reversedCollection)
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