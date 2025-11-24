/*
    EnhancedLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
    */

using AlastairLundy.EnhancedLinq.Memory.Internals.Helpers;

#if NET8_0_OR_GREATER

namespace AlastairLundy.EnhancedLinq.Memory.Immediate.Maths;

/// <summary>
/// 
/// </summary>
public static partial class EnhancedLinqMemoryImmediateMaths
{
    /// <param name="source">The span of type <see cref="TNumber"/> to be searched.</param>
    /// <typeparam name="TNumber">The numeric type that represents the type of numbers in the span.</typeparam>
    extension<TNumber>(Span<TNumber> source) where TNumber : INumber<TNumber>
    {
        /// <summary>
        /// Determines the minimum value of a span of numbers of type <see cref="TNumber"/>.
        /// </summary>
        /// <returns>The minimum value of the number in the span.</returns>
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
        /// Determines the maximum value of a span of numbers of type <see cref="TNumber"/>.
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
}
#endif