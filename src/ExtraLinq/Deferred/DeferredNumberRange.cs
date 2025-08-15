/*
    ExtraLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

using ExtraLinq.Deferred.Enumerables.NumberRanges;

// ReSharper disable ConvertClosureToMethodGroup

namespace ExtraLinq.Deferred;

public static partial class ExtraLinqDeferred
{
    
    /// <summary>
    /// Generates a sequence of numeric values starting from a specified value and continuing for a specified count,
    /// with each value incremented by 1 from the starting point.
    /// </summary>
    /// <param name="start">The starting value of the sequence.</param>
    /// <param name="count">The number of values to generate in the sequence.</param>
    /// <returns>An <see cref="IEnumerable{T}"/> containing the generated sequence of unsigned long values,
    /// incremented by 1 from the starting point.</returns>
    /// <exception cref="ArgumentException">Thrown if the start number or count are NaN.</exception>
    /// <exception cref="NotFiniteNumberException">Thrown if the start number or count are infinity.</exception>
    public static IEnumerable<TNumber> NumberRange<TNumber>(this TNumber start, TNumber count)
        where TNumber : INumber<TNumber>
    {
        if (TNumber.IsNaN(start) || TNumber.IsNaN(count))
            throw new ArgumentException();

        if (TNumber.IsInfinity(start) || TNumber.IsInfinity(count))
            throw new NotFiniteNumberException();
        
        return new NumberRangeEnumerable<TNumber>(start, count);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="start"></param>
    /// <param name="count"></param>
    /// <param name="numbersToSkip"></param>
    /// <typeparam name="TNumber"></typeparam>
    /// <returns></returns>
    public static IEnumerable<TNumber> NumberRange<TNumber>(this TNumber start, TNumber count,
        IEnumerable<TNumber> numbersToSkip) where TNumber : INumber<TNumber> 
        => NumberRange(start, count)
            .SkipWhile(x => numbersToSkip.Contains(x));
}