/*
    EnhancedLinq.Async
    Copyright (c) 2025-2026 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/.
    */

#if NET8_0_OR_GREATER

using System.Linq;
using System.Numerics;
using EnhancedLinq.Async.Deferred.Enumerables;

namespace EnhancedLinq.Async.Deferred;

/// <summary>
/// Provides extension methods for working with deferred number ranges.
/// </summary>
public static class DeferredNumberRangeExtensions
{
    /// <param name="start">The starting value of the sequence.</param>
    /// <typeparam name="TNumber">The numeric type used to represent the numbers.</typeparam>
    extension<TNumber>(TNumber start) where TNumber : INumber<TNumber>
    {
        /// <summary>
        /// Generates a sequence of numeric values starting from a specified value and continuing for a specified count,
        /// with each value incremented by 1 from the starting point.
        /// </summary>
        /// <param name="count">The number of values to generate in the sequence.</param>
        /// <param name="incrementor">The amount to increment each number by.</param>
        /// <returns>A sequence containing the generated numeric values,
        /// incremented by the incrementor amount from the starting point.</returns>
        /// <exception cref="ArgumentException">Thrown if the start number or count is NaN.</exception>
        /// <exception cref="NotFiniteNumberException">Thrown if the start number or count is infinity.</exception>
        public IAsyncEnumerable<TNumber> GenerateNumberRange(TNumber count, TNumber incrementor)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(count);
            ArgumentOutOfRangeException.ThrowIfZero(incrementor);

            if (TNumber.IsNaN(start))
                throw new ArgumentException(Resources.Exceptions_Numbers_ParameterIsNotANumber, nameof(start));

            if (TNumber.IsNaN(count))
                throw new ArgumentException(Resources.Exceptions_Numbers_ParameterIsNotANumber, nameof(count));

            if (TNumber.IsNaN(incrementor))
                throw new ArgumentException(Resources.Exceptions_Numbers_ParameterIsNotANumber, nameof(incrementor));

            if (TNumber.IsInfinity(start) || TNumber.IsInfinity(count))
                throw new NotFiniteNumberException();

            return new AsyncNumberRangeEnumerable<TNumber>(start, count, incrementor);
        }

        /// <summary>
        /// Generates a sequence of numeric values starting from a specified value and continuing for a specified count, skipping, 
        /// with each value incremented by 1 from the starting point.
        /// </summary>
        /// <param name="count">The number of values to generate in the sequence.</param>
        /// <param name="incrementor">The amount to increment each number by.</param>
        /// <param name="numbersToSkip">The numbers to skip when generating the range of numbers.</param>
        /// <returns>A sequence containing the generated numeric values,
        /// incremented by the incrementor amount from the starting point.</returns>
        public async IAsyncEnumerable<TNumber> GenerateNumberRange(TNumber count, TNumber incrementor,
            IAsyncEnumerable<TNumber> numbersToSkip)
        {
            IAsyncEnumerable<TNumber> numbers = start.GenerateNumberRange(count, incrementor)
                .WhereAsync(n => numbersToSkip.ContainsAsync(n));

            await foreach (var number in numbers.ConfigureAwait(false))
            {
                yield return number;
            }
        }
    }
}
#endif