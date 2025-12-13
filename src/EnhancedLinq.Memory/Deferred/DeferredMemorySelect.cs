/*
    EnhancedLinq.Memory
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
*/

using EnhancedLinq.Memory.Deferred.Enumerables;

namespace EnhancedLinq.Memory.Deferred;

public static partial class EnhancedLinqMemoryDeferred
{
    /// <typeparam name="TSource">The type of elements in the source memory.</typeparam>
    /// <param name="source">A memory sequence of elements to apply the transform function to.</param>
    extension<TSource>(Memory<TSource> source)
    {
        /// <summary>
        /// Projects each element of a memory-based enumerable into a new form.
        /// </summary>
        /// <typeparam name="TResult">The type of the value returned by the selector function.</typeparam>
        /// <param name="predicate">A transform function to apply to each element.</param>
        /// <returns>An IEnumerable containing the transformed elements.</returns>
        public IEnumerable<TResult> Select<TResult>(Func<TSource, TResult> predicate)
        {
            ArgumentNullException.ThrowIfNull(predicate);

            return new MemorySelectEnumerable<TSource,TResult>(source, predicate);
        }
    }
    
    /// <param name="source">A <see cref="ReadOnlyMemory{T}"/> sequence of elements to apply the transform function to.</param>
    /// <typeparam name="TSource">The type of elements in the source <see cref="ReadOnlyMemory{T}"/>.</typeparam>
    extension<TSource>(ReadOnlyMemory<TSource> source)
    {
        /// <summary>
        /// Projects each element of a memory-based enumerable into a new form using the specified transform function.
        /// </summary>
        /// <typeparam name="TResult">The type of the value returned by the transform function.</typeparam>
        /// <param name="predicate">A function to transform each element from the source.</param>
        /// <returns>An enumerable containing the transformed elements.</returns>
        public IEnumerable<TResult> Select<TResult>(Func<TSource, TResult> predicate)
        {
            ArgumentNullException.ThrowIfNull(predicate);

            return new MemorySelectEnumerable<TSource,TResult>(source, predicate);
        }
    }
}