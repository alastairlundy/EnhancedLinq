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

namespace AlastairLundy.EnhancedLinq.Memory.Immediate.Maths;

public static partial class EnhancedLinqMemoryImmediateMaths
{
    /// <summary>
    /// Calculates the sum of a span of numbers.
    /// </summary>
    /// <param name="source">The span of type <see cref="TNumber"/> to be summed.</param>
    /// <typeparam name="TNumber">The numeric type that represents the type of numbers in the span.</typeparam>
    /// <returns>The sum of all the number in the span.</returns>
    public static TNumber Sum<TNumber>(this Span<TNumber> source) where TNumber : INumber<TNumber>
    {
        TNumber total = TNumber.Zero;

        foreach (TNumber item in source)
        {
            total += item;
        }

        return total;
    }
    
    /// <summary>
    /// Calculates the sum of a memory of numbers.
    /// </summary>
    /// <param name="source">The memory of type <see cref="TNumber"/> to be summed.</param>
    /// <typeparam name="TNumber">The numeric type that represents the type of numbers in the memory.</typeparam>
    /// <returns>The sum of all the number in the memory.</returns>
    public static TNumber Sum<TNumber>(this Memory<TNumber> source) where TNumber : INumber<TNumber>
        => Sum<TNumber>(source.Span);
}
#endif