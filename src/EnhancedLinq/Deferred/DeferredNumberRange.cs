/*
    EnhancedLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
    */

using System.Linq; 

#if NET8_0_OR_GREATER
using System.Numerics;
using EnhancedLinq.Deferred.Enumerables.NumberRanges;
#endif

// ReSharper disable ConvertClosureToMethodGroup

namespace EnhancedLinq.Deferred;

public static partial class EnhancedLinqDeferred
{
#if NET8_0_OR_GREATER
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
        /// <exception cref="ArgumentException">Thrown if the start number or count are NaN.</exception>
        /// <exception cref="NotFiniteNumberException">Thrown if the start number or count are infinity.</exception>
        public IEnumerable<TNumber> GenerateNumberRange(TNumber count, TNumber incrementor)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(count);
            ArgumentOutOfRangeException.ThrowIfZero(incrementor);
            
            if (TNumber.IsNaN(start) || TNumber.IsNaN(count) || TNumber.IsNaN(incrementor))
                throw new ArgumentException();

            if (TNumber.IsInfinity(start) || TNumber.IsInfinity(count))
                throw new NotFiniteNumberException();

            return new NumberRangeEnumerable<TNumber>(start, count, incrementor);
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
        public IEnumerable<TNumber> GenerateNumberRange(TNumber count, TNumber incrementor,
            IEnumerable<TNumber> numbersToSkip) => GenerateNumberRange(start, count, incrementor)
            .SkipWhile(x => numbersToSkip.Contains(x));
    }

#else
    /// <param name="start">The starting value of the sequence.</param>
    extension(int start)
    {
        /// <summary>
        /// Generates a sequence of numeric values starting from a specified value and continuing for a specified count,
        /// with each value incremented by 1 from the starting point.
        /// </summary>
        /// <param name="count">The number of values to generate in the sequence.</param>
        /// <param name="incrementor">The amount to increment each number by.</param>
        /// <returns>A sequence containing the generated numeric values,
        /// incremented by the incrementor amount from the starting point.</returns>
        /// <exception cref="ArgumentException">Thrown if the start number or count are NaN.</exception>
        /// <exception cref="NotFiniteNumberException">Thrown if the start number or count are infinity.</exception>
        public IEnumerable<int> GenerateNumberRange(int count, int incrementor)
        {
            ArgumentOutOfRangeException.ThrowIfZero(incrementor);
            ArgumentOutOfRangeException.ThrowIfEqual(incrementor,int.MaxValue);
            ArgumentOutOfRangeException.ThrowIfEqual(incrementor, int.MinValue);
            
            for (int i = start; i < count; i += incrementor)
            {
                yield return i;
            }
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
        public IEnumerable<int> GenerateNumberRange(int count, int incrementor,
            IEnumerable<int> numbersToSkip)
            => GenerateNumberRange(start, count, incrementor)
                .SkipWhile(x => numbersToSkip.Contains(x));
    }

#endif
}