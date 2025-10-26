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
using System.Numerics;
using AlastairLundy.DotExtensions.Numbers;

namespace AlastairLundy.EnhancedLinq.Memory.Immediate.Maths;

/// <summary>
/// Provides extension methods for performing arithmetic operations such as sum and average
/// on memory and span structures containing numerical data types.
/// </summary>
public static partial class EnhancedLinqMemoryImmediateMaths
{
    /// <summary>
    /// Calculates the arithmetic average of a span of numbers.
    /// </summary>
    /// <param name="source">The span of type <see cref="TNumber"/> to be averaged.</param>
    /// <typeparam name="TNumber">The numeric type that represents the type of numbers in the span.</typeparam>
    /// <returns>The arithmetic average of the specified numbers.</returns>
    public static TNumber Average<TNumber>(this Span<TNumber> source) where TNumber : INumber<TNumber>
    {
        TNumber sum = source.Sum();

        return sum / source.Length.ToNumber<TNumber>();
    }
    
    /// <summary>
    /// Calculates the arithmetic average of a Memory struct holding numbers of type <see cref="TNumber"/>.
    /// </summary>
    /// <param name="source">The Memory of type <see cref="TNumber"/> to be averaged.</param>
    /// <typeparam name="TNumber">The numeric type that represents the type of numbers in the span.</typeparam>
    /// <returns>The arithmetic average of the specified numbers.</returns>
    public static TNumber Average<TNumber>(this Memory<TNumber> source) where TNumber : INumber<TNumber>
        => Average(source.Span);
}
#endif