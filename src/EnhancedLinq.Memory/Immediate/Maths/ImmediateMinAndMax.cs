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

namespace AlastairLundy.EnhancedLinq.Memory.Immediate.Maths;

/// <summary>
/// 
/// </summary>
public static partial class EnhancedLinqMemoryImmediateMaths
{
    /// <param name="source">The span of type <see cref="TNumber"/> to be searched.</param>
    /// <typeparam name="TNumber">The numeric type that represents the type of numbers in the span.</typeparam>
    extension<TNumber>(Span<TNumber> source) where TNumber : INumber<TNumber>
    {
        /// <summary>
        /// Determines the minimum value of a span of numbers of type <see cref="TNumber"/>.
        /// </summary>
        /// <returns>The minimum value of the number in the span.</returns>
        public TNumber Minimum()
        {
            TNumber total = source[0];

            foreach (TNumber item in source)
            {
                if (item <= total)
                {
                    total = item;
                }
            }

            return total;
        }

        /// <summary>
        /// Determines the maximum value of a span of numbers of type <see cref="TNumber"/>.
        /// </summary>
        /// <returns>The maximum value of the number in the span.</returns>
        public TNumber Maximum()
        {
            TNumber total = TNumber.Zero;

            foreach (TNumber item in source)
            {
                if (item > total)
                {
                    total = item;
                }
            }

            return total;
        }
    }
}
#endif