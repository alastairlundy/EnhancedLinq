/*
    EnhancedLinq
    Copyright (c) 2025 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/.
 */

#if NET8_0_OR_GREATER

using System;
using System.Numerics;

namespace AlastairLundy.EnhancedLinq.Memory.Immediate.Maths;

public static partial class EnhancedLinqMemoryImmediateMaths
{
    /// <summary>
    /// Calculates the sum of a span of numbers.
    /// </summary>
    /// <param name="source">The span of type <see cref="TNumber"/> to be summed.</param>
    /// <typeparam name="TNumber">The numeric type that represents the type of numbers in the span.</typeparam>
    /// <returns>The sum of all the number in the span.</returns>
    public static TNumber Sum<TNumber>(this Span<TNumber> source) where TNumber : INumber<TNumber>
    {
        TNumber total = TNumber.Zero;

        foreach (TNumber item in source)
        {
            total += item;
        }

        return total;
    }
    
    /// <summary>
    /// Calculates the sum of a memory of numbers.
    /// </summary>
    /// <param name="source">The memory of type <see cref="TNumber"/> to be summed.</param>
    /// <typeparam name="TNumber">The numeric type that represents the type of numbers in the memory.</typeparam>
    /// <returns>The sum of all the number in the memory.</returns>
    public static TNumber Sum<TNumber>(this Memory<TNumber> source) where TNumber : INumber<TNumber>
        => Sum<TNumber>(source.Span);
}
#endif