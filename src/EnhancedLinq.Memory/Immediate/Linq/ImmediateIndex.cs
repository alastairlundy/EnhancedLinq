/*
    EnhancedLinq.Memory
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
    */

namespace EnhancedLinq.Memory.Immediate;

public static partial class EnhancedLinqMemoryImmediate
{
    /// <param name="span">The span whose indices will be generated.</param>
    /// <typeparam name="TSource">The type of the elements in the span.</typeparam>
    extension<TSource>(Span<TSource> span)
    {
        /// <summary>
        /// Returns a collection of indices of all elements within the given span, starting from an initial index of zero.
        /// </summary>
        /// <returns>A collection of integers representing the indices of the elements in the span starting from index zero.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the span is empty.</exception>
        public ICollection<(int Index, TSource Item)> Index()
        { 
            InvalidOperationException.ThrowIfSpanIsEmpty(span);

            (int, TSource)[] output = new (int, TSource)[span.Length];
        
            for (int i = 0; i < span.Length; i++)
            {
                output[i] = (i, span[i]);
            }
        
            return output;
        }
    }
    
    /// <param name="span">The <see cref="ReadOnlySpan{T}"/> whose indices will be generated.</param>
    /// <typeparam name="TSource">The type of the elements in the <see cref="ReadOnlySpan{T}"/>.</typeparam>
    extension<TSource>(ReadOnlySpan<TSource> span)
    {
        /// <summary>
        /// Returns a collection of indices of all elements within the given <see cref="ReadOnlySpan{T}"/>, starting from an initial index of zero.
        /// </summary>
        /// <returns>A collection of integers representing the indices of the elements in the <see cref="ReadOnlySpan{T}"/> starting from index zero.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the <see cref="ReadOnlySpan{T}"/> is empty.</exception>
        public ICollection<(int Index, TSource Item)> Index()
        { 
            InvalidOperationException.ThrowIfSpanIsEmpty(span);

            (int, TSource)[] output = new (int, TSource)[span.Length];

            for (int i = 0; i < span.Length; i++)
            {
                output[i] = (i, span[i]);
            }
        
            return output;
        }
    }
}