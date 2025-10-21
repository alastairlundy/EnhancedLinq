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
        
#if NET8_0_OR_GREATER
        HashSet<T> set = new(capacity: source.Length, comparer: comparer);
#else
        HashSet<T> set = new(comparer: comparer);
#endif
        
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