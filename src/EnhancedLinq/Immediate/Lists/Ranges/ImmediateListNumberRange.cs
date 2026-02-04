/*
    EnhancedLinq 
    Copyright (c) 2025-2026 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
    */

#if NET8_0_OR_GREATER
using System.Numerics;
using DotExtensions.Numbers;
#endif

namespace EnhancedLinq.Immediate.Ranges;

public static partial class EnhancedLinqImmediateRange
{
#if NET8_0_OR_GREATER
    /// <param name="startIndex">The starting value of the array.</param>
    /// <typeparam name="TNumber"></typeparam>
    extension<TNumber>(TNumber startIndex)
        where TNumber : INumber<TNumber>, IMinMaxValue<TNumber>
    {
        /// <summary>
        /// Generates an array of <see cref="INumber{TSelf}"/> values starting from a specified value and continuing for a specified count,
        /// with each value incremented by 1 from the starting point.
        /// </summary>
        /// <param name="count">The number of values to generate in the array.</param>
        /// <returns>An array of type <see cref="INumber{TSelf}"/> containing the generated range of values,
        /// incremented by 1 from the starting point.</returns>
        /// <exception cref="ArgumentException">Thrown when the count + startIndex are greater than <see cref="INumber{TSelf}"/>.MaxValue </exception>
        public TNumber[] GenerateNumberRangeAsArray(TNumber count)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(count);
            
            if (startIndex + count >= TNumber.MaxValue)
                throw new ArgumentException(Resources.Exceptions_Count_LessThanZero);

            int arrayCount = count.ToDestinationNumber<TNumber, int>() + 1;
        
            TNumber[] output = new TNumber[arrayCount];

            int index = 0;
        
            for (TNumber i = startIndex; i < count; i++)
            {
                output[index] = i;
                index++;
            }
        
            return output;
        }

        /// <summary>
        /// Generates a list of <see cref="INumber{TSelf}"/> values starting from a specified value and continuing for a specified count,
        /// with each value incremented by 1 from the starting point.
        /// </summary>
        /// <param name="count">The number of values to generate in the list.</param>
        /// <returns>A <see cref="List{INumber}"/> containing the generated range of <see cref="INumber{TSelf}"/> values,
        /// incremented by 1 from the starting point.</returns>
        /// <exception cref="ArgumentException">Thrown if the count is less than zero.</exception>
        public List<TNumber> GenerateNumberRangeAsList(TNumber count)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(count);

            if (startIndex + count > TNumber.MaxValue)
                throw new ArgumentException(Resources.Exceptions_Count_LessThanZero);
        
            List<TNumber> output = new List<TNumber>(count.ToDestinationNumber<TNumber, int>() + 1);
        
            for (TNumber i = startIndex; i < count; i++)
            {
                output.Add(i);
            }
        
            return output;
        }
    }

#endif
}