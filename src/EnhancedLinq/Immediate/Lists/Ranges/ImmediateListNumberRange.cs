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

#if NET8_0_OR_GREATER
using System;
using System.Collections.Generic;
using System.Numerics;
using AlastairLundy.DotExtensions.Numbers;
using AlastairLundy.EnhancedLinq.Internals.Localizations;
#endif

namespace AlastairLundy.EnhancedLinq.Immediate.Ranges;

public static partial class EnhancedLinqImmediateRange
{
#if NET8_0_OR_GREATER
    /// <param name="startIndex">The starting value of the array.</param>
    /// <typeparam name="TNumber"></typeparam>
    extension<TNumber>(TNumber startIndex)
        where TNumber : INumber<TNumber>, IMinMaxValue<TNumber>
    {
        /// <summary>
        /// Generates an array of <see cref="TNumber"/> values starting from a specified value and continuing for a specified count,
        /// with each value incremented by 1 from the starting point.
        /// </summary>
        /// <param name="count">The number of values to generate in the array.</param>
        /// <returns>An array of type <see cref="TNumber"/> containing the generated range of values,
        /// incremented by 1 from the starting point.</returns>
        /// <exception cref="ArgumentException">Thrown when the count + startIndex are greater than <see cref="TNumber"/>.MaxValue </exception>
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
        /// Generates a list of <see cref="TNumber"/> values starting from a specified value and continuing for a specified count,
        /// with each value incremented by 1 from the starting point.
        /// </summary>
        /// <param name="count">The number of values to generate in the list.</param>
        /// <returns>A <see cref="List{TNumber}"/> containing the generated range of <see cref="TNumber"/> values,
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