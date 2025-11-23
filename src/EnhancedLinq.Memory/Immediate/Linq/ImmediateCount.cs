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
#endif

namespace AlastairLundy.EnhancedLinq.Memory.Immediate;

public static partial class EnhancedLinqMemoryImmediate
{
    /// <param name="source">The span to search.</param>
    /// <typeparam name="TSource">The type of elements in the span.</typeparam>
    extension<TSource>(Span<TSource> source)
    {
        /// <summary>
        /// Returns the number of elements in a given span that satisfy a condition.
        /// </summary>
        /// <param name="selector">A func that takes an element and returns a boolean indicating whether it should be counted.</param>
        /// <returns>The number of elements that satisfy the predicate.</returns>
        public int Count(Func<TSource, bool> selector)
        {
            int count = 0;

            foreach (TSource item in source)
            {
                if (selector(item)) 
                    count++;
            }
        
            return count;
        }

#if NET8_0_OR_GREATER

        /// <summary>
        /// Returns the number of elements in a given span that satisfy a condition as a <see cref="TNumber"/>.
        /// </summary>
        /// <param name="selector">A func that takes an element and returns a boolean indicating whether it should be counted.</param>
        /// <typeparam name="TNumber">The numeric type that represents the type of numbers in the span.</typeparam>
        /// <returns>The number of elements that satisfy the predicate.</returns>
        public TNumber Count<TNumber>(Func<TSource, bool> selector) where TNumber : INumber<TNumber>
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
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <typeparam name="TSource"></typeparam>
    extension<TSource>(ReadOnlySpan<TSource> source)
    {
        /// <summary>
        /// Returns the number of elements in a given <see cref="ReadOnlySpan{T}"/> that satisfy a condition.
        /// </summary>
        /// <param name="selector">A func that takes an element and returns a boolean indicating whether it should be counted.</param>
        /// <returns>The number of elements that satisfy the predicate.</returns>
        public int Count(Func<TSource, bool> selector)
        {
            int count = 0;

            foreach (TSource item in source)
            {
                if (selector(item)) 
                    count++;
            }
        
            return count;
        }

#if NET8_0_OR_GREATER

        /// <summary>
        /// Returns the number of elements in a given <see cref="ReadOnlySpan{T}"/> that satisfy a condition as a <see cref="TNumber"/>.
        /// </summary>
        /// <param name="selector">A func that takes an element and returns a boolean indicating whether it should be counted.</param>
        /// <typeparam name="TNumber">The numeric type that represents the type of numbers in the <see cref="ReadOnlySpan{T}"/>.</typeparam>
        /// <returns>The number of elements that satisfy the predicate.</returns>
        public TNumber Count<TNumber>(Func<TSource, bool> selector) where TNumber : INumber<TNumber>
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
}