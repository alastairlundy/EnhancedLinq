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
    /// <param name="target">The span to make a new span from.</param>
    /// <typeparam name="T">The type of items stored in the span.</typeparam>
    extension<T>(Span<T> target)
    {
        /// <summary>
        /// Returns a new Span with all the elements of the span except the specified first number of elements.
        /// </summary>
        /// <param name="count">The number of items to skip from the beginning of the span.</param>
        /// <returns>A new Span with all the elements of the original span except the specified number of first elements to skip.</returns>
        public Span<T> Skip(int count)
        {
            InvalidOperationException.ThrowIfSpanIsEmpty(target);

            if (count > target.Length)
                throw new ArgumentOutOfRangeException(Resources
                    .Exceptions_SkipCount_TooLarge);
        
            return target.Slice(start: count, target.Length - count);
        }

        /// <summary>
        /// Returns a new Span with all the elements of the span except the specified last number of elements.
        /// </summary>
        /// <param name="count">The number of items to skip from the end of the span.</param>
        /// <returns>A new Span with all the elements of the original span except the specified last number of elements to skip.</returns>
        public Span<T> SkipLast(int count)
        {
            InvalidOperationException.ThrowIfSpanIsEmpty(target);

            if (count > target.Length)
                throw new ArgumentOutOfRangeException(Resources
                    .Exceptions_SkipCount_TooLarge);
            
            return target.Slice(start: 0, target.Length - count);
        }

        /// <summary>
        /// Returns a new Span with all the elements of the original span that do not match the specified predicate func.
        /// </summary>
        /// <param name="predicate">The condition to use to determine whether to skip items or not in the span.</param>
        /// <returns>A new Span with all the elements of the original Span that did not match the specified predicate func.</returns>
        public Span<T> SkipWhile(Func<T, bool> predicate)
        {
            InvalidOperationException.ThrowIfSpanIsEmpty(target);

            return from item in target
                where !predicate.Invoke(item)
                select item;
        }
    }

    /// <summary>
    /// Provides extension methods for manipulating spans in memory with immediate operations.
    /// </summary>
    /// <typeparam name="T">The type of items stored in the span.</typeparam>
    /// <param name="target">The span to perform operations on.</param>
    extension<T>(ReadOnlySpan<T> target)
    {
        /// <summary>
        /// Returns a new read-only span with all the elements of the original span except the specified first number of elements.
        /// </summary>
        /// <param name="count">The number of items to skip from the beginning of the span.</param>
        public ReadOnlySpan<T> Skip(int count)
        {
            InvalidOperationException.ThrowIfSpanIsEmpty(target);
            ArgumentOutOfRangeException.ThrowIfNegative(count);

            if (count > target.Length)
                throw new ArgumentOutOfRangeException(Resources
                    .Exceptions_SkipCount_TooLarge);
        
            return target.Slice(start: count, target.Length - count);
        }


        /// <summary>
        /// Returns a new span with all the elements of the original span except for the specified number of elements from the end.
        /// </summary>
        /// <param name="count">The number of items to exclude from the end of the span.</param>
        /// <returns>A new span with all the elements of the original span excluding the specified number of elements from the end.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the specified count is negative or greater than the span's length.</exception>
        public ReadOnlySpan<T> SkipLast(int count)
        {
            InvalidOperationException.ThrowIfSpanIsEmpty(target);
            ArgumentOutOfRangeException.ThrowIfNegative(count);

            if (count > target.Length)
                throw new ArgumentOutOfRangeException(Resources
                    .Exceptions_SkipCount_TooLarge);
            
            return target.Slice(start: 0, target.Length - count);
        }

        /// <summary>
        /// Returns a new <see cref="ReadOnlySpan{T}"/> containing the elements of the original span starting from the first element
        /// for which the specified predicate evaluates to false and including all subsequent elements.
        /// </summary>
        /// <param name="predicate">The predicate function to apply to each element to determine if it should be skipped.</param>
        /// <returns>A new <see cref="ReadOnlySpan{T}"/> starting from the first element for which the predicate returns false.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the provided predicate is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the target span is empty.</exception>
        public ReadOnlySpan<T> SkipWhile(Func<T, bool> predicate)
        {
            InvalidOperationException.ThrowIfSpanIsEmpty(target);
            
            ArgumentNullException.ThrowIfNull(predicate);

            return from item in target
                where !predicate.Invoke(item)
                select item;
        }
    }
}