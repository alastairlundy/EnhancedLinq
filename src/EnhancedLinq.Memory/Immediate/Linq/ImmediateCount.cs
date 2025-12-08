/*
    EnhancedLinq.Memory
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
*/

namespace EnhancedLinq.Memory.Immediate;

public static partial class EnhancedLinqMemoryImmediate
{
    /// <param name="source">The span to search.</param>
    /// <typeparam name="TSource">The type of elements in the span.</typeparam>
    extension<TSource>(Span<TSource> source)
    {
        /// <summary>
        /// Returns the number of elements in a given span that satisfy a condition.
        /// </summary>
        /// <param name="predicate">A func that takes an element and returns a boolean indicating whether it should be counted.</param>
        /// <returns>The number of elements that satisfy the predicate.</returns>
        public int Count(Func<TSource, bool> predicate)
        {
            InvalidOperationException.ThrowIfSpanIsEmpty(source);
            ArgumentNullException.ThrowIfNull(predicate);
            
            int count = 0;

            foreach (TSource item in source)
            {
                if (predicate(item)) 
                    count++;
            }
        
            return count;
        }

#if NET8_0_OR_GREATER

        /// <summary>
        /// Returns the number of elements in a given span that satisfy a condition.
        /// </summary>
        /// <param name="predicate">A func that takes an element and returns a boolean indicating whether it should be counted.</param>
        /// <typeparam name="TNumber">The numeric type that represents the type of numbers in the span.</typeparam>
        /// <returns>The number of elements that satisfy the predicate.</returns>
        public TNumber Count<TNumber>(Func<TSource, bool> predicate) where TNumber : INumber<TNumber>
        {
            InvalidOperationException.ThrowIfSpanIsEmpty(source);
            ArgumentNullException.ThrowIfNull(predicate);
            
            TNumber total = TNumber.Zero;

            foreach (TSource item in source)
            {
                if(predicate(item))
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
        /// <param name="predicate">A func that takes an element and returns a boolean indicating whether it should be counted.</param>
        /// <returns>The number of elements that satisfy the predicate.</returns>
        public int Count(Func<TSource, bool> predicate)
        {
            InvalidOperationException.ThrowIfSpanIsEmpty(source);
            ArgumentNullException.ThrowIfNull(predicate);
            
            int count = 0;

            foreach (TSource item in source)
            {
                if (predicate(item)) 
                    count++;
            }
        
            return count;
        }

#if NET8_0_OR_GREATER

        /// <summary>
        /// Returns the number of elements in a given <see cref="ReadOnlySpan{T}"/> that satisfy a condition.
        /// </summary>
        /// <param name="predicate">A func that takes an element and returns a boolean indicating whether it should be counted.</param>
        /// <typeparam name="TNumber">The numeric type that represents the type of numbers in the <see cref="ReadOnlySpan{T}"/>.</typeparam>
        /// <returns>The number of elements that satisfy the predicate.</returns>
        public TNumber Count<TNumber>(Func<TSource, bool> predicate) where TNumber : INumber<TNumber>
        {
            InvalidOperationException.ThrowIfSpanIsEmpty(source);
            ArgumentNullException.ThrowIfNull(predicate);
            
            TNumber total = TNumber.Zero;

            foreach (TSource item in source)
            {
                if(predicate(item))
                    total += TNumber.One;
            }
        
            return total;
        }
#endif
    }
}