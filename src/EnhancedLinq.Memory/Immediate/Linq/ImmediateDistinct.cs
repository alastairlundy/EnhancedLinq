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
    /// <param name="source">The source span from which to remove non-distinct elements.</param>
    /// <typeparam name="T">The type of elements in the source span.</typeparam>
    extension<T>(Span<T> source)
    {
        /// <summary>
        /// Returns a new span containing distinct elements from the source span, using the default equality comparer.
        /// </summary>
        /// <returns>A span containing the distinct elements from the source span.</returns>
        public Span<T> Distinct()
            => DistinctArray(source, EqualityComparer<T>.Default);
        
        /// <summary>
        /// Returns a new span containing distinct elements from the source span, using the specified equality comparer.
        /// </summary>
        /// <param name="comparer">The equality comparer to use for comparing elements.</param>
        /// <returns>A span containing the distinct elements from the source span.</returns>
        public Span<T> Distinct(IEqualityComparer<T>? comparer) 
            => DistinctArray(source, comparer);
    }
    
    /// <param name="source">The source <see cref="ReadOnlySpan{T}"/> from which to remove non-distinct elements.</param>
    /// <typeparam name="T">The type of elements in the source <see cref="ReadOnlySpan{T}"/>.</typeparam>
    extension<T>(ReadOnlySpan<T> source)
    {
        /// <summary>
        /// Returns a new <see cref="ReadOnlySpan{T}"/> containing distinct elements from the source <see cref="ReadOnlySpan{T}"/>, using the default equality comparer.
        /// </summary>
        /// <returns>A <see cref="ReadOnlySpan{T}"/> containing the distinct elements from the source <see cref="ReadOnlySpan{T}"/>.</returns>
        public ReadOnlySpan<T> Distinct(IEqualityComparer<T>? comparer) 
            => DistinctArray(source, comparer);

        /// <summary>
        /// Returns a new <see cref="ReadOnlySpan{T}"/> containing distinct elements from the source <see cref="ReadOnlySpan{T}"/>, using the default equality comparer.
        /// </summary>
        /// <returns>A <see cref="ReadOnlySpan{T}"/> containing the distinct elements from the source <see cref="ReadOnlySpan{T}"/>.</returns>
        public ReadOnlySpan<T> Distinct() => 
            DistinctArray(source, EqualityComparer<T>.Default);
    }
    
    /// <param name="source">The source <see cref="ReadOnlyMemory{T}"/> from which to remove non-distinct elements.</param>
    /// <typeparam name="T">The type of elements in the source <see cref="ReadOnlyMemory{T}"/>.</typeparam>
    extension<T>(Memory<T> source)
    {
        /// <summary>
        /// Returns a new <see cref="Memory{T}"/> containing distinct elements from the source <see cref="Memory{T}"/>, using the default equality comparer.
        /// </summary>
        /// <returns>A <see cref="Memory{T}"/> containing the distinct elements from the source <see cref="Memory{T}"/>.</returns>
        public Memory<T> Distinct() => 
            DistinctArray(source.Span, EqualityComparer<T>.Default);
        
        /// <summary>
        /// Returns a new <see cref="Memory{T}"/> containing distinct elements from the source <see cref="Memory{T}"/>, using the specified equality comparer.
        /// </summary>
        /// <typeparam name="T">The type of elements in the source <see cref="Memory{T}"/>.</typeparam>
        /// <param name="comparer">The equality comparer to use for determining distinct elements. If null, the default equality comparer is used.</param>
        /// <returns>A <see cref="Memory{T}"/> containing the distinct elements from the source <see cref="Memory{T}"/>.</returns>
        public Memory<T> Distinct(IEqualityComparer<T>? comparer) =>
            DistinctArray(source.Span, comparer);
    }

    /// <param name="source">The source <see cref="ReadOnlyMemory{T}"/> from which to remove non-distinct elements.</param>
    /// <typeparam name="T">The type of elements in the source <see cref="ReadOnlyMemory{T}"/>.</typeparam>
    extension<T>(ReadOnlyMemory<T> source)
    {
        /// <summary>
        /// Returns a new <see cref="ReadOnlyMemory{T}"/> containing distinct elements from the source <see cref="ReadOnlyMemory{T}"/>, using the default equality comparer.
        /// </summary>
        /// <returns>A <see cref="ReadOnlyMemory{T}"/> containing the distinct elements from the source <see cref="ReadOnlyMemory{T}"/>.</returns>
        public ReadOnlyMemory<T> Distinct() => 
            DistinctArray(source.Span, EqualityComparer<T>.Default);
        
        /// <summary>
        /// Returns a new <see cref="ReadOnlyMemory{T}"/> containing distinct elements from the source <see cref="ReadOnlyMemory{T}"/>, using the specified equality comparer.
        /// </summary>
        /// <typeparam name="T">The type of elements in the source <see cref="ReadOnlyMemory{T}"/>.</typeparam>
        /// <param name="comparer">The equality comparer to use for determining distinct elements. If null, the default equality comparer is used.</param>
        /// <returns>A <see cref="ReadOnlyMemory{T}"/> containing the distinct elements from the source <see cref="ReadOnlyMemory{T}"/>.</returns>
        public ReadOnlyMemory<T> Distinct(IEqualityComparer<T>? comparer) =>
            DistinctArray(source.Span, comparer);
    }

    private static T[] DistinctArray<T>(Span<T> source, IEqualityComparer<T>? comparer)
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
        
        return output;
    }
    
    private static T[] DistinctArray<T>(ReadOnlySpan<T> source, IEqualityComparer<T>? comparer)
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
        
        return output;
    }
}