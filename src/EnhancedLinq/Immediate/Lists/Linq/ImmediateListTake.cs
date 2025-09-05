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
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="count"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static T[] Take<T>(this T[] source, int count)
    {
        ArgumentNullException.ThrowIfNull(source);
        
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
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="count"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static IList<T> Take<T>(this IList<T> source, int count)
    {
        ArgumentNullException.ThrowIfNull(source);
        
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
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="count"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static ICollection<T> Take<T>(this ICollection<T> source, int count)
    {
        ArgumentNullException.ThrowIfNull(source);
        
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