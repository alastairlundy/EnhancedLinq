/*
    EnhancedLinq.Memory
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
    */

#if NET8_0_OR_GREATER

namespace EnhancedLinq.Memory.Immediate.Maths;

public static partial class EnhancedLinqMemoryImmediateMaths
{
    /// <param name="source">The span to be summed.</param>
    /// <typeparam name="TNumber">The numeric type that represents the type of numbers in the span.</typeparam>
    extension<TNumber>(Span<TNumber> source) where TNumber : INumber<TNumber>
    {
        /// <summary>
        /// Calculates the sum of a span of numbers.
        /// </summary>
        /// <returns>The sum of all the number in the span.</returns>
        public TNumber Sum()
        {
            InvalidOperationException.ThrowIfSpanIsEmpty(source);

            TNumber total = TNumber.Zero;

            foreach (TNumber item in source)
            {
                total += item;
            }

            return total;
        }
    }
    
    /// <param name="source">The <see cref="ReadOnlySpan{T}"/> to be summed.</param>
    /// <typeparam name="TNumber">The numeric type that represents the type of numbers in the <see cref="ReadOnlySpan{T}"/>.</typeparam>
    extension<TNumber>(ReadOnlySpan<TNumber> source) where TNumber : INumber<TNumber>
    {
        /// <summary>
        /// Calculates the sum of a <see cref="ReadOnlySpan{T}"/> of numbers.
        /// </summary>
        /// <returns>The sum of all the number in the <see cref="ReadOnlySpan{T}"/>.</returns>
        public TNumber Sum()
        {
            InvalidOperationException.ThrowIfSpanIsEmpty(source);

            TNumber total = TNumber.Zero;

            foreach (TNumber item in source)
            {
                total += item;
            }

            return total;
        }
    }

    /// <param name="source">The memory to be summed.</param>
    /// <typeparam name="TNumber">The numeric type that represents the type of numbers in the memory.</typeparam>
    extension<TNumber>(Memory<TNumber> source) where TNumber : INumber<TNumber>
    {
        /// <summary>
        /// Calculates the sum of a memory of numbers.
        /// </summary>
        /// <returns>The sum of all the number in the memory.</returns>
        public TNumber Sum() => Sum(source.Span);
    }
    
    /// <param name="source">The <see cref="ReadOnlyMemory{T}"/> to be summed.</param>
    /// <typeparam name="TNumber">The numeric type that represents the type of numbers in the <see cref="ReadOnlyMemory{T}"/>.</typeparam>
    extension<TNumber>(ReadOnlyMemory<TNumber> source) where TNumber : INumber<TNumber>
    {
        /// <summary>
        /// Calculates the sum of a <see cref="ReadOnlyMemory{T}"/> of numbers.
        /// </summary>
        /// <returns>The sum of all the number in the <see cref="ReadOnlyMemory{T}"/>.</returns>
        public TNumber Sum() => Sum(source.Span);
    }
}
#endif