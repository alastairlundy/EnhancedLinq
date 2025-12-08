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
            
            TResult[] array = new  TResult[source.Length];
        
            int index = 0;
        
            source.ForEach(x =>
            {
                array[index] = predicate.Invoke(x);
                index++;
            });

            return new Span<TResult>(array);
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
            
            TResult[] array = new  TResult[source.Length];
        
            int index = 0;

            foreach (TSource item in source)
            {
                array[index] = predicate.Invoke(item);
                index++;
            }

            return new Span<TResult>(array);
        }
    }

    /// <param name="source">The source <see cref="Memory{T}"/> containing elements to be transformed.</param>
    /// <typeparam name="TSource">The type of elements in the source <see cref="Memory{T}"/>.</typeparam>
    extension<TSource>(Memory<TSource> source)
    {
        /// <summary>
        /// Transforms elements of a <see cref="Memory{T}"/> into a new <see cref="Memory{T}"/> using the defined predicate.
        /// </summary>
        /// <param name="predicate">The transformation function to apply to each element in the <see cref="Memory{T}"/>.</param>
        /// <typeparam name="TResult">The type of elements in the transformed <see cref="Memory{T}"/>.</typeparam>
        /// <returns>A new <see cref="Memory{T}"/> containing the elements transformed by the predicate.</returns>
        public Memory<TResult> Select<TResult>(Func<TSource, TResult> predicate)
        {
            InvalidOperationException.ThrowIfMemoryIsEmpty(source);
            ArgumentNullException.ThrowIfNull(predicate);
            
            TResult[] array = new  TResult[source.Length];
        
            int index = 0;
        
            source.ForEach(x =>
            {
                array[index] = predicate.Invoke(x);
                index++;
            });
        
            return new Memory<TResult>(array);
        }
    }

    /// <param name="source">The <see cref="ReadOnlyMemory{T}"/> containing the elements to be transformed.</param>
    /// <typeparam name="TSource">The type of the elements in the source <see cref="ReadOnlyMemory{T}"/>.</typeparam>
    extension<TSource>(ReadOnlyMemory<TSource> source)
    {
        /// <summary>
        /// Transforms elements of a <see cref="ReadOnlyMemory{T}"/> according using the defined predicate.
        /// </summary>
        /// <param name="predicate">The transformation function to apply to each element in the <see cref="ReadOnlyMemory{T}"/></param>
        /// <typeparam name="TResult">The type of the elements in the resulting <see cref="ReadOnlyMemory{T}"/>.</typeparam>
        /// <returns>A new <see cref="ReadOnlyMemory{T}"/> containing the transformed elements.</returns>
        public ReadOnlyMemory<TResult> Select<TResult>(Func<TSource, TResult> predicate)
        {
            InvalidOperationException.ThrowIfMemoryIsEmpty(source);
            ArgumentNullException.ThrowIfNull(predicate);
            
            TResult[] array = new TResult[source.Length];

            int index = 0;

            foreach (TSource item in source.Span)
            {
                array[index] = predicate.Invoke(item);
                index++;
            }
        
            return new Memory<TResult>(array);
        }
    }
}