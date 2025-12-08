/*
    EnhancedLinq.Memory
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
    */

#if NET8_0_OR_GREATER
using AlastairLundy.DotExtensions.Numbers;

namespace EnhancedLinq.Memory.Immediate.Maths;

/// <summary>
/// Provides extension methods for performing arithmetic operations such as sum and average
/// on memory and span structures containing numerical data types.
/// </summary>
public static partial class EnhancedLinqMemoryImmediateMaths
{
    /// <param name="source">The span of type <see cref="TNumber"/> to be averaged.</param>
    /// <typeparam name="TNumber">The numeric type that represents the type of numbers in the span.</typeparam>
    extension<TNumber>(Span<TNumber> source) where TNumber : INumber<TNumber>
    {
        /// <summary>
        /// Calculates the arithmetic average of a span of numbers.
        /// </summary>
        /// <returns>The arithmetic average of the specified numbers.</returns>
        public TNumber Average()
        {
            InvalidOperationException.ThrowIfSpanIsEmpty(source);

            TNumber sum = source.Sum();

            return sum / source.Length.ToNumber<TNumber>();
        }
    }

    /// <param name="source">The Memory of type <see cref="TNumber"/> to be averaged.</param>
    /// <typeparam name="TNumber">The numeric type that represents the type of numbers in the span.</typeparam>
    extension<TNumber>(Memory<TNumber> source) where TNumber : INumber<TNumber>
    {
        /// <summary>
        /// Calculates the arithmetic average of a Memory struct holding numbers of type <see cref="TNumber"/>.
        /// </summary>
        /// <returns>The arithmetic average of the specified numbers.</returns>
        public TNumber Average() => Average(source.Span);
    }
}
#endif