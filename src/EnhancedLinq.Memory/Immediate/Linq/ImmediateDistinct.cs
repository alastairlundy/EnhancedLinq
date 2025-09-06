/*
    EnhancedLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/.
 */

using System;
using System.Collections.Generic;

namespace AlastairLundy.EnhancedLinq.Memory.Immediate;

public static partial class EnhancedLinqMemoryImmediate
{
    /// <summary>
    /// Returns a new span containing distinct elements from the source span, using the default equality comparer.
    /// </summary>
    /// <param name="source">The source span from which to Enhancedct distinct elements.</param>
    /// <typeparam name="T">The type of elements in the source span.</typeparam>
    /// <returns>A span containing the distinct elements from the source span.</returns>
    public static Span<T> Distinct<T>(this Span<T> source) => 
        Distinct(source, EqualityComparer<T>.Default);

    /// <summary>
    /// Returns a new span containing distinct elements from the source span, using the specified equality comparer.
    /// </summary>
    /// <param name="source">The source span from which to Enhancedct distinct elements.</param>
    /// <param name="comparer">The equality comparer to use for comparing elements.</param>
    /// <typeparam name="T">The type of elements in the source span.</typeparam>
    /// <returns>A span containing the distinct elements from the source span.</returns>
    public static Span<T> Distinct<T>(this Span<T> source, IEqualityComparer<T>? comparer)
    {
        comparer ??= EqualityComparer<T>.Default;
        
        HashSet<T> set = new(capacity: source.Length, comparer: comparer);

        int currentIndex = 0;
        T[] output = new T[source.Length];
        
        foreach (T item in source)
        {
            bool result = set.Add(item);

            if (result)
            {
                output[currentIndex] = item;
                currentIndex++;
            }
        }
        
        if((currentIndex + 1) < source.Length)
            Array.Resize(ref output, currentIndex + 1);
        
        return new Span<T>(output);
    }
}