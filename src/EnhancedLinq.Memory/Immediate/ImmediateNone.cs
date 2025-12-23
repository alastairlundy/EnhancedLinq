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
    /// <param name="span">The span to be searched.</param>
    /// <typeparam name="TSource">The type of elements in the span.</typeparam>
    extension<TSource>(Span<TSource> span)
    {
        /// <summary>
        /// Determines if none of the elements in a <see cref="Span{T}"/> match a predicate condition.
        /// </summary>
        /// <param name="predicate">The predicate to check elements against.</param>
        /// <returns>True if none of the elements matched the predicate, false otherwise.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the predicate is null.</exception>
        /// <exception cref="ArgumentException">Thrown if the <see cref="Span{T}"/> is empty.</exception>
        public bool None(Func<TSource, bool> predicate)
            => span.CountAtMost(predicate, 0);
    }

    /// <param name="span">The <see cref="ReadOnlySpan{T}"/> to be searched.</param>
    /// <typeparam name="TSource">The type of elements in the <see cref="ReadOnlySpan{T}"/>.</typeparam>
    extension<TSource>(ReadOnlySpan<TSource> span)
    {
        /// <summary>
        /// Determines if none of the elements in a <see cref="ReadOnlySpan{T}"/> match a predicate condition.
        /// </summary>
        /// <param name="predicate">The predicate to check elements against.</param>
        /// <returns>True if none of the elements matched the predicate, false otherwise.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the predicate is null.</exception>
        /// <exception cref="ArgumentException">Thrown if the <see cref="ReadOnlySpan{T}"/> is empty.</exception>
        public bool None(Func<TSource, bool> predicate)
            => span.CountAtMost(predicate, 0);
    }


    /// <param name="memory">The <see cref="Memory{T}"/> to be searched.</param>
    /// <typeparam name="TSource">The type of elements in the <see cref="Memory{T}"/>.</typeparam>
    extension<TSource>(Memory<TSource> memory)
    {
        /// <summary>
        /// Determines if none of the elements in a <see cref="Memory{T}"/> match a predicate condition.
        /// </summary>
        /// <param name="predicate">The predicate to check elements against.</param>
        /// <returns>True if none of the elements matched the predicate, false otherwise.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the predicate is null.</exception>
        /// <exception cref="ArgumentException">Thrown if the <see cref="Memory{T}"/> is empty.</exception>
        public bool None(Func<TSource, bool> predicate)
            => memory.CountAtMost(predicate, 0);
    }

    /// <param name="memory">The <see cref="ReadOnlyMemory{T}"/> to be searched.</param>
    /// <typeparam name="TSource">The type of elements in the <see cref="ReadOnlyMemory{T}"/>.</typeparam>
    extension<TSource>(ReadOnlyMemory<TSource> memory)
    {
        /// <summary>
        /// Determines if none of the elements in a <see cref="ReadOnlyMemory{T}"/> match a predicate condition.
        /// </summary>
        /// <param name="predicate">The predicate to check elements against.</param>
        /// <returns>True if none of the elements matched the predicate, false otherwise.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the predicate is null.</exception>
        /// <exception cref="ArgumentException">Thrown if the <see cref="ReadOnlyMemory{T}"/> is empty.</exception>
        public bool None(Func<TSource, bool> predicate)
            => memory.CountAtMost(predicate, 0);
    }
}