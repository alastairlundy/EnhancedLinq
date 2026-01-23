/*
    EnhancedLinq.Memory
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
    */

using System.Linq;

namespace EnhancedLinq.Memory.Immediate;

public static partial class EnhancedLinqMemoryImmediate
{
    /// <summary>
    /// Provides extension methods for performing immediate operations on <see cref="Span{T}"/>.
    /// </summary>
    extension<TSource>(Span<TSource> source) where TSource : notnull
    {
        public Span<TSource> Exclude(Span<TSource> span)
        {
            InvalidOperationException.ThrowIfSpanIsEmpty(source);

            List<int> indices = new();
            
            foreach (var item in span)
            {
                indices.AddRange(source.IndicesOf(item));
            }

            indices = indices.Distinct().ToList();
            
            List<TSource> result = new List<TSource>(indices.Count);

            int index = 0;
            foreach (TSource item in  source)
            {
                if(!indices.Contains(index))
                    result.Add(item);
            }

            return result.ToArray();
        }

        /// <summary>
        /// Returns a new <see cref="Span{TSource}"/> containing the elements of the input span
        /// that do not satisfy the given predicate.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the input span.</typeparam>
        /// <param name="predicate">The predicate function to determine which elements to exclude.</param>
        /// <returns>A new <see cref="Span{TSource}"/> containing the elements that do not satisfy the predicate.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the provided predicate is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown if the input span is empty.</exception>
        public Span<TSource> Exclude(Func<TSource, bool> predicate)
        {
            InvalidOperationException.ThrowIfSpanIsEmpty(source);
            
            ArgumentNullException.ThrowIfNull(predicate);
            
            return source.SkipWhile(s => predicate(s));
        }
    }

    /// <summary>
    /// Provides extension methods for performing immediate operations on <see cref="ReadOnlySpan{T}"/>.
    /// </summary>
    extension<TSource>(ReadOnlySpan<TSource> source)
    {
        /// <summary>
        /// Returns a new <see cref="ReadOnlySpan{TSource}"/> containing the elements of the input span
        /// that do not satisfy the given predicate.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the input span.</typeparam>
        /// <param name="predicate">The predicate function to determine which elements to exclude.</param>
        /// <returns>A new <see cref="ReadOnlySpan{TSource}"/> containing elements that do not satisfy the predicate.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the provided predicate is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown if the input span is empty.</exception>
        public ReadOnlySpan<TSource> Exclude(Func<TSource, bool> predicate)
        {
            InvalidOperationException.ThrowIfSpanIsEmpty(source);
            
            ArgumentNullException.ThrowIfNull(predicate);
            
            return source.SkipWhile(s => predicate(s));
        }
    }
}