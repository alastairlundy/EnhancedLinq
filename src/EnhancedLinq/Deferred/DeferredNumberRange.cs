/*
      EnhancedLinq 
      Copyright (c) 2025 Alastair Lundy
      
     Licensed under the Apache License, Version 2.0 (the "License");
     you may not use this file except in compliance with the License.
     You may obtain a copy of the License at

         http://www.apache.org/licenses/LICENSE-2.0

     Unless required by applicable law or agreed to in writing, software
     distributed under the License is distributed on an "AS IS" BASIS,
     WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
     See the License for the specific language governing permissions and
     limitations under the License.
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