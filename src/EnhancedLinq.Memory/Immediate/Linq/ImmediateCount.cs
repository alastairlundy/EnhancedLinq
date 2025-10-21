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

#if NET8_0_OR_GREATER
using System.Numerics;
#endif

namespace AlastairLundy.EnhancedLinq.Memory.Immediate;

public static partial class EnhancedLinqMemoryImmediate
{
    /// <summary>
    /// Returns the number of elements in a given span that satisfy a condition.
    /// </summary>
    /// <param name="source">The span to search.</param>
    /// <param name="selector">A func that takes an element and returns a boolean indicating whether it should be counted.</param>
    /// <typeparam name="TSource">The type of elements in the span.</typeparam>
    /// <returns>The number of elements that satisfy the predicate.</returns>
    public static int Count<TSource>(this Span<TSource> source,
        Func<TSource, bool> selector)
    {
        int count = 0;

        foreach (TSource item in source)
        {
            if (selector(item))
            {
                count++;
            }
        }
        
        return count;
    }
    
#if NET8_0_OR_GREATER
    /// <summary>
    /// Returns the number of elements in a given span that satisfy a condition as a <see cref="TNumber"/>.
    /// </summary>
    /// <param name="source">The span to search.</param>
    /// <param name="selector">A func that takes an element and returns a boolean indicating whether it should be counted.</param>
    /// <typeparam name="TNumber">The numeric type that represents the type of numbers in the span.</typeparam>
    /// <typeparam name="TSource">The type of elements in the span.</typeparam>
    /// <returns>The number of elements that satisfy the predicate.</returns>
    public static TNumber Count<TSource,TNumber>(this Span<TSource> source, 
        Func<TSource, bool> selector) where TNumber : INumber<TNumber>
    {
        TNumber total = TNumber.Zero;

        foreach (TSource item in source)
        {
            if(selector(item))
                total += TNumber.One;
        }
        
        return total;
    }
#endif
}