/*
    EnhancedLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/.
 */

using System.Numerics;
using AlastairLundy.DotExtensions.Numbers;

namespace EnhancedLinq.Memory.Immediate.Maths;

public static partial class EnhancedLinqMemoryImmediateMaths
{
    /// <summary>
    /// Calculates the arithmetic average of a span of numbers.
    /// </summary>
    /// <param name="source">The span of type <see cref="TNumber"/> to be averaged.</param>
    /// <typeparam name="TNumber">The numeric type that represents the type of numbers in the span.</typeparam>
    /// <returns>The arithmetic average of the specified numbers.</returns>
    public static TNumber Average<TNumber>(this Span<TNumber> source) where TNumber : INumber<TNumber>
    {
        TNumber sum = source.Sum();

        return sum / source.Length.ToNumber<TNumber>();
    }
    
    /// <summary>
    /// Calculates the arithmetic average of a Memory struct holding numbers of type <see cref="TNumber"/>.
    /// </summary>
    /// <param name="source">The Memory of type <see cref="TNumber"/> to be averaged.</param>
    /// <typeparam name="TNumber">The numeric type that represents the type of numbers in the span.</typeparam>
    /// <returns>The arithmetic average of the specified numbers.</returns>
    public static TNumber Average<TNumber>(this Memory<TNumber> source) where TNumber : INumber<TNumber>
        => Average(source.Span);
}