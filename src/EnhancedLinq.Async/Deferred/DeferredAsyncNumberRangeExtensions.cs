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
#endif

namespace EnhancedLinq.Async.Deferred;

/// <summary>
/// Provides deferred asynchronous number range extensions for numeric sequences.
/// </summary>
public static class DeferredAsyncNumberRangeExtensions
{
#if NET8_0_OR_GREATER
    /// <param name="start">The starting value of the sequence.</param>
    /// <typeparam name="TNumber">The numeric type used to represent the numbers.</typeparam>
    extension<TNumber>(TNumber start) where TNumber : INumber<TNumber>
    {
        /// <summary>
        /// Generates an async enumerable sequence of numbers based on a starting value and step size.
        /// </summary>
        /// <param name="count">The number of elements to generate.</param>
        /// <param name="incrementor">The step between successive values.</param>
        /// <returns>An async enumerable of type TNumber representing the generated range.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the count is non‑positive or incrementor is zero.</exception>
        /// <exception cref="NotFiniteNumberException">Thrown if start, count, or incrementor are infinite numbers.</exception>
        /// <exception cref="ArgumentException">Thrown if any parameter is NaN.</exception>
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
        /// Generates an async enumerable sequence of numbers based on a starting value and step size,
        /// optionally skipping elements from another asynchronous sequence.
        /// </summary>
        /// <param name="count">The number of elements to generate.</param>
        /// <param name="incrementor">The step between successive values.</param>
        /// <param name="numbersToSkip">An asynchronous sequence of numbers to skip during generation.</param>
        /// <returns>An async enumerable of type TNumber representing the generated range with optional skips.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the count is non‑positive or incrementor is zero.</exception>
        /// <exception cref="NotFiniteNumberException">Thrown if start, count, or incrementor are infinite numbers.</exception>
        /// <exception cref="ArgumentException">Thrown if any parameter is NaN.</exception>
        public async IAsyncEnumerable<TNumber> GenerateNumberRange(TNumber count, TNumber incrementor,
            IAsyncEnumerable<TNumber> numbersToSkip)
        {
            IAsyncEnumerable<TNumber> numbers = start.GenerateNumberRange(count, incrementor)
                .WhereAsync(async n => await numbersToSkip.ContainsAsync(n).ConfigureAwait(false));

            await foreach (TNumber number in numbers.ConfigureAwait(false))
            {
                yield return number;
            }
        }
    }

#else
    extension(int start)
    {
        /// <summary>
        /// Generates a deferred asynchronous number range.
        /// </summary>
        /// <param name="count">The number of elements to generate.</param>
        /// <param name="incrementor">The increment between consecutive numbers.</param>
        /// <returns>An <see cref="IAsyncEnumerable{int}"/> representing the sequence from start with step incrementor, containing count elements.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if count or incrementor is zero or negative; or exceeds Int32.MaxValue.</exception>
        public IAsyncEnumerable<int> GenerateNumberRange(int count, int incrementor)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(count);
            ArgumentOutOfRangeException.ThrowIfZero(incrementor);

            ArgumentOutOfRangeException.ThrowIfGreaterThan(count, int.MaxValue);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(incrementor, int.MaxValue);

            return new AsyncNumberRangeNetStandardEnumerable(start, count, incrementor);
        }
    }
#endif
}