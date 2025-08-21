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
using System.Numerics;
using AlastairLundy.DotExtensions.Numbers;
using EnhancedLinq.Internals.Localizations;

namespace EnhancedLinq.Immediate.Ranges;

public static partial class EnhancedLinqImmediateRange
{
    /// <summary>
    /// Generates an array of <see cref="TNumber"/> values starting from a specified value and continuing for a specified count,
    /// with each value incremented by 1 from the starting point.
    /// </summary>
    /// <param name="startIndex">The starting value of the array.</param>
    /// <param name="count">The number of values to generate in the array.</param>
    /// <typeparam name="TNumber"></typeparam>
    /// <returns>An array of type <see cref="TNumber"/> containing the generated range of values,
    /// incremented by 1 from the starting point.</returns>
    /// <exception cref="ArgumentException">Thrown when the count + startIndex are greater than <see cref="TNumber"/>.MaxValue </exception>
    public static TNumber[] GenerateNumberRangeAsArray<TNumber>(this TNumber startIndex, TNumber count) where TNumber : INumber<TNumber>, IMinMaxValue<TNumber>?
    {
        if (startIndex + count >= TNumber.MaxValue)
        {
            throw new ArgumentException(Resources.Exceptions_Count_LessThanZero);
        }

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
    /// <param name="startIndex">The starting value of the list.</param>
    /// <param name="count">The number of values to generate in the list.</param>
    /// <returns>A <see cref="List{TNumber}"/> containing the generated range of <see cref="TNumber"/> values,
    /// incremented by 1 from the starting point.</returns>
    /// <exception cref="ArgumentException">Thrown if the count is less than zero.</exception>
    public static List<TNumber> GenerateNumberRangeAsList<TNumber>(this TNumber startIndex, TNumber count) where TNumber : INumber<TNumber>, IMinMaxValue<TNumber>?
    {
        if (startIndex + count > TNumber.MaxValue)
            throw new ArgumentException(Resources.Exceptions_Count_LessThanZero);
        
        if (count < TNumber.Zero)
            throw new ArgumentException(Resources.Exceptions_Count_LessThanZero
                .Replace("{x}", $"{count}"));
        
        List<TNumber> output = new List<TNumber>(count.ToDestinationNumber<TNumber, int>() + 1);
        
        for (TNumber i = startIndex; i < count; i++)
        {
            output.Add(i);
        }
        
        return output;
    }
}