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

/// <summary>
/// 
/// </summary>
public static partial class EnhancedLinqMemoryImmediateMaths
{
    /// <summary>
    /// Determines the minimum value of a span of numbers of type <see cref="TNumber"/>.
    /// </summary>
    /// <param name="source">The span of type <see cref="TNumber"/> to be searched.</param>
    /// <typeparam name="TNumber">The numeric type that represents the type of numbers in the span.</typeparam>
    /// <returns>The minimum value of the number in the span.</returns>
    public static TNumber Minimum<TNumber>(this Span<TNumber> source) where TNumber : INumber<TNumber>
    {
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
    /// <param name="source">The span of type <see cref="TNumber"/> to be searched.</param>
    /// <typeparam name="TNumber">The numeric type that represents the type of numbers in the span.</typeparam>
    /// <returns>The maximum value of the number in the span.</returns>
    public static TNumber Maximum<TNumber>(this Span<TNumber> source) where TNumber : INumber<TNumber>
    {
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
#endif