/*
    EnhancedLinq.Memory
    Copyright (c) 2025-2026 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
*/

namespace EnhancedLinq.Memory.Immediate;

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
        /// <param name="comparer">The equality comparer to use for determining distinct elements. If null, the default equality comparer is used.</param>
        /// <returns>A <see cref="ReadOnlyMemory{T}"/> containing the distinct elements from the source <see cref="ReadOnlyMemory{T}"/>.</returns>
        public ReadOnlyMemory<T> Distinct(IEqualityComparer<T>? comparer) =>
            DistinctArray(source.Span, comparer);
    }

    private static T[] DistinctArray<T>(Span<T> source, IEqualityComparer<T>? comparer)
    {
        comparer ??= EqualityComparer<T>.Default;
        
        InvalidOperationException.ThrowIfSpanIsEmpty(source);
        
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
        
        InvalidOperationException.ThrowIfSpanIsEmpty(source);
        
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