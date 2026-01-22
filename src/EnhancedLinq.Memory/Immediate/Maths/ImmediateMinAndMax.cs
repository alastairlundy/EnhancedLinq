/*
    EnhancedLinq.Memory
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
    */


#if NET8_0_OR_GREATER
namespace EnhancedLinq.Memory.Immediate.Maths;

/// <summary>
/// Provides an implementation of immediate mathematical operations on memory spans
/// for use in scenarios that demand enhanced LINQ-like capabilities with a focus
/// on performance and utility.
/// </summary>
public static partial class EnhancedLinqMemoryImmediateMaths
{
    /// <param name="source">The span to be searched.</param>
    /// <typeparam name="TNumber">The numeric type that represents the type of numbers in the span.</typeparam>
    extension<TNumber>(Span<TNumber> source) where TNumber : INumber<TNumber>
    {
        /// <summary>
        /// Determines the minimum value of a span of numbers.
        /// </summary>
        /// <returns>The minimum value of the number in the span.</returns>
        public INumber<TNumber> Minimum()
        {
            InvalidOperationException.ThrowIfSpanIsEmpty(source);
            
            TNumber total = source[0];

            foreach (TNumber item in source)
            {
                if (item <= total)
                {
                    total = item;
                }
            }

            return total;
        }

        /// <summary>
        /// Determines the maximum value of a span of numbers.
        /// </summary>
        /// <returns>The maximum value of the number in the span.</returns>
        public TNumber Maximum()
        {
            InvalidOperationException.ThrowIfSpanIsEmpty(source);

            TNumber total = TNumber.Zero;

            foreach (TNumber item in source)
            {
                if (item > total)
                {
                    total = item;
                }
            }

            return total;
        }
    }
    
    /// <param name="source">The <see cref="ReadOnlySpan{T}"/> to be searched.</param>
    /// <typeparam name="TNumber">The numeric type that represents the type of numbers in the <see cref="ReadOnlySpan{T}"/>.</typeparam>
    extension<TNumber>(ReadOnlySpan<TNumber> source) where TNumber : INumber<TNumber>
    {
        /// <summary>
        /// Determines the minimum value of a <see cref="ReadOnlySpan{T}"/> of numbers.
        /// </summary>
        /// <returns>The minimum value of the number in the <see cref="ReadOnlySpan{T}"/>.</returns>
        public TNumber Minimum()
        {
            InvalidOperationException.ThrowIfSpanIsEmpty(source);
            
            TNumber total = source[0];

            foreach (TNumber item in source)
            {
                if (item <= total)
                {
                    total = item;
                }
            }

            return total;
        }

        /// <summary>
        /// Determines the maximum value of a <see cref="ReadOnlySpan{T}"/> of numbers.
        /// </summary>
        /// <returns>The maximum value of the number in the <see cref="ReadOnlySpan{T}"/>.</returns>
        public TNumber Maximum()
        {
            InvalidOperationException.ThrowIfSpanIsEmpty(source);

            TNumber total = TNumber.Zero;

            foreach (TNumber item in source)
            {
                if (item > total)
                {
                    total = item;
                }
            }

            return total;
        }
    }
}
#endif