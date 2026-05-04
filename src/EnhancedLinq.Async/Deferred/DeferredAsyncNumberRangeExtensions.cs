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
        /// 
        /// </summary>
        /// <param name="count"></param>
        /// <param name="incrementor"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="NotFiniteNumberException"></exception>
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
        /// 
        /// </summary>
        /// <param name="count"></param>
        /// <param name="incrementor"></param>
        /// <param name="numbersToSkip"></param>
        /// <returns></returns>
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
        /// 
        /// </summary>
        /// <param name="count"></param>
        /// <param name="incrementor"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public IAsyncEnumerable<int> GenerateNumberRange(int count, int incrementor)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(count);
            ArgumentOutOfRangeException.ThrowIfZero(incrementor);

            ArgumentOutOfRangeException.ThrowIfGreaterThan(count, int.MaxValue);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(incrementor, int.MaxValue);
        
            /*if (int.IsNaN(start))
                throw new ArgumentException(Resources.Exceptions_Numbers_ParameterIsNotANumber, nameof(start));

            if (int.IsNaN(count))
                throw new ArgumentException(Resources.Exceptions_Numbers_ParameterIsNotANumber, nameof(count));

            if (int.IsNaN(incrementor))
                throw new ArgumentException(Resources.Exceptions_Numbers_ParameterIsNotANumber, nameof(incrementor));

            if (int.IsInfinity(start) || int.IsInfinity(count))
                throw new NotFiniteNumberException();*/

            return new AsyncNumberRangeNetStandardEnumerable(start, count, incrementor);
        }
    }
#endif
}