/*
    EnhancedLinq.Memory
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
    */

using System.Buffers;

namespace EnhancedLinq.Memory.Immediate;

public static partial class EnhancedLinqMemoryImmediate
{
    /// <param name="source">The span to search.</param>
    /// <typeparam name="TSource">The type of elements in the source Span.</typeparam>
    extension<TSource>(Span<TSource> source)
    {
        /// <summary>
        /// Transforms elements of a Span according to behaviour defined by the predicate.
        /// </summary>
        /// <param name="predicate">The predicate to use.</param>
        /// <typeparam name="TResult">The type of elements the predicate transforms elements into.</typeparam>
        /// <returns>The newly created Span with the elements transformed by the predicate.</returns>
        public Span<TResult> Select<TResult>(Func<TSource, TResult> predicate)
        {
            InvalidOperationException.ThrowIfSpanIsEmpty(source);
            ArgumentNullException.ThrowIfNull(predicate);
            
            TResult[] array = ArrayPool<TResult>.Shared.Rent(source.Length);
        
            int index = 0;
        
            source.ForEach(x =>
            {
                array[index] = predicate.Invoke(x);
                index++;
            });
            
            Span<TResult> output = array.AsSpan(0, source.Length);
            ArrayPool<TResult>.Shared.Return(array);
            
            return output;
        }
    }

    /// <param name="source">The <see cref="ReadOnlySpan{T}"/> to transform.</param>
    /// <typeparam name="TSource">The type of elements in the source <see cref="ReadOnlySpan{T}"/>.</typeparam>
    extension<TSource>(ReadOnlySpan<TSource> source)
    {
        /// <summary>
        /// Transforms elements of a <see cref="ReadOnlySpan{T}"/> according to the defined predicate.
        /// </summary>
        /// <param name="predicate">The function to apply to each element of the <see cref="ReadOnlySpan{T}"/>.</param>
        /// <typeparam name="TResult">The type of elements in the resulting <see cref="ReadOnlySpan{T}"/> after transformation.</typeparam>
        /// <returns>A new <see cref="ReadOnlySpan{T}"/> with the elements transformed by the predicate.</returns>
        public ReadOnlySpan<TResult> Select<TResult>(Func<TSource, TResult> predicate)
        {
            InvalidOperationException.ThrowIfSpanIsEmpty(source);
            ArgumentNullException.ThrowIfNull(predicate);
            
            TResult[] array = ArrayPool<TResult>.Shared.Rent(source.Length);
        
            int index = 0;

            foreach (TSource item in source)
            {
                array[index] = predicate.Invoke(item);
                index++;
            }

            Span<TResult> output = array.AsSpan(0, source.Length);
            ArrayPool<TResult>.Shared.Return(array);
            
            return output;
        }
    }
}