/*
    EnhancedLinq
    Copyright (c) 2025 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/.
 */

using System;
using System.Collections.Generic;
using System.Linq;

#if NET8_0_OR_GREATER
using System.Numerics;
using AlastairLundy.EnhancedLinq.Deferred.Enumerables.NumberRanges;
#endif

// ReSharper disable ConvertClosureToMethodGroup

namespace AlastairLundy.EnhancedLinq.Deferred;

public static partial class EnhancedLinqDeferred
{
#if NET8_0_OR_GREATER
    /// <summary>
    /// Generates a sequence of numeric values starting from a specified value and continuing for a specified count,
    /// with each value incremented by 1 from the starting point.
    /// </summary>
    /// <param name="start">The starting value of the sequence.</param>
    /// <param name="count">The number of values to generate in the sequence.</param>
    /// <param name="incrementor">The amount to increment each number by.</param>
    /// <returns>A sequence containing the generated numeric values,
    /// incremented by the incrementor amount from the starting point.</returns>
    /// <typeparam name="TNumber">The numeric type used to represent the numbers.</typeparam>
    /// <exception cref="ArgumentException">Thrown if the start number or count are NaN.</exception>
    /// <exception cref="NotFiniteNumberException">Thrown if the start number or count are infinity.</exception>
    public static IEnumerable<TNumber> GenerateNumberRange<TNumber>(this TNumber start, TNumber count, TNumber incrementor)
        where TNumber : INumber<TNumber>
    {
        if (TNumber.IsNaN(start) || TNumber.IsNaN(count) || TNumber.IsNaN(incrementor))
            throw new ArgumentException();

        if (TNumber.IsInfinity(start) || TNumber.IsInfinity(count))
            throw new NotFiniteNumberException();

        if (incrementor == TNumber.Zero)
            throw new ArgumentOutOfRangeException(nameof(incrementor));
        
        return new NumberRangeEnumerable<TNumber>(start, count, incrementor);
    }

    /// <summary>
    /// Generates a sequence of numeric values starting from a specified value and continuing for a specified count, skipping, 
    /// with each value incremented by 1 from the starting point.
    /// </summary>
    /// <param name="start">The starting value of the sequence.</param>
    /// <param name="count">The number of values to generate in the sequence.</param>
    /// <param name="incrementor">The amount to increment each number by.</param>
    /// <param name="numbersToSkip">The numbers to skip when generating the range of numbers.</param>
    /// <typeparam name="TNumber">The numeric type used to represent the numbers.</typeparam>
    /// <returns>A sequence containing the generated numeric values,
    /// incremented by the incrementor amount from the starting point.</returns>
    public static IEnumerable<TNumber> GenerateNumberRange<TNumber>(this TNumber start, TNumber count, TNumber incrementor,
        IEnumerable<TNumber> numbersToSkip) where TNumber : INumber<TNumber> 
        => GenerateNumberRange(start, count, incrementor)
            .SkipWhile(x => numbersToSkip.Contains(x));
#else

      /// <summary>
    /// Generates a sequence of numeric values starting from a specified value and continuing for a specified count,
    /// with each value incremented by 1 from the starting point.
    /// </summary>
    /// <param name="start">The starting value of the sequence.</param>
    /// <param name="count">The number of values to generate in the sequence.</param>
    /// <param name="incrementor">The amount to increment each number by.</param>
    /// <returns>A sequence containing the generated numeric values,
    /// incremented by the incrementor amount from the starting point.</returns>
    /// <exception cref="ArgumentException">Thrown if the start number or count are NaN.</exception>
    /// <exception cref="NotFiniteNumberException">Thrown if the start number or count are infinity.</exception>
    public static IEnumerable<int> GenerateNumberRange(this int start, int count, int incrementor)
    {
        if (incrementor == 0)
            throw new ArgumentOutOfRangeException(nameof(incrementor));

        for (int i = start; i < count; i += incrementor)
        {
            yield return i;
        }
    }

    /// <summary>
    /// Generates a sequence of numeric values starting from a specified value and continuing for a specified count, skipping, 
    /// with each value incremented by 1 from the starting point.
    /// </summary>
    /// <param name="start">The starting value of the sequence.</param>
    /// <param name="count">The number of values to generate in the sequence.</param>
    /// <param name="incrementor">The amount to increment each number by.</param>
    /// <param name="numbersToSkip">The numbers to skip when generating the range of numbers.</param>
    /// <returns>A sequence containing the generated numeric values,
    /// incremented by the incrementor amount from the starting point.</returns>
    public static IEnumerable<int> GenerateNumberRange(this int start, int count, int incrementor,
        IEnumerable<int> numbersToSkip)
        => GenerateNumberRange(start, count, incrementor)
            .SkipWhile(x => numbersToSkip.Contains(x));
#endif
}