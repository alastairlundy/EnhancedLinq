/*
    EnhancedLinq.Memory
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
    */

// ReSharper disable ReplaceSliceWithRangeIndexer

namespace EnhancedLinq.Memory.Immediate;

public static partial class EnhancedLinqMemoryImmediate
{
    /// <param name="source">The Span to extract elements from.</param>
    /// <typeparam name="T">The type of elements in the <see cref="Span{T}"/>.</typeparam>
    extension<T>(Span<T> source)
    {
        /// <summary>
        /// Takes the first 'count' elements from the specified Span.
        /// </summary>
        /// <param name="count">The number of elements to take.</param>
        /// <returns>A <see cref="ArgumentException"/> containing the first 'count' elements from the Span.</returns>
        /// <exception cref="Span{T}">Thrown when the count is less than zero or greater than the length of the Span.</exception>
        public Span<T> Take(int count)
        {
            InvalidOperationException.ThrowIfSpanIsEmpty(source);
            ArgumentOutOfRangeException.ThrowIfNegative(count);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(count, source.Length);
            
            return source.Slice(0, count);
        }

        /// <summary>
        /// Returns elements from the <see cref="Span{T}"/> as long as the predicate condition matches elements in the Span.
        /// </summary>
        /// <param name="predicate">The predicate condition to test each element of the Span against.</param>
        /// <returns>A <see cref="Span{T}"/> containing the elements that occur before the predicate condition is false.</returns>
        /// <exception cref="ArgumentException">Thrown if the Span is empty.</exception>
        public Span<T> TakeWhile(Func<T, int, bool> predicate)
        {
            InvalidOperationException.ThrowIfSpanIsEmpty(source);
            ArgumentNullException.ThrowIfNull(predicate);
            
            int end = 0;
            int start = 0;
            
            bool firstDetection = true;

            for (int index = 0; index < source.Length; index++)
            {
                bool result = predicate(source[index], index);

                if (result && firstDetection)
                {
                    start = index;
                    firstDetection = false;
                }
                
                if (!result && !firstDetection)
                {
                    end = index;
                    break;
                }
            }

            return source.Slice(start, end - 1);
        }

        /// <summary>
        /// Takes the last 'count' elements from a Span.
        /// </summary>
        /// <param name="count">The number of elements to take from the end of the source Span.</param>
        /// <returns>An <see cref="Span{T}"/> containing the last 'count' elements from the Span.</returns>
        /// <exception cref="ArgumentException">Thrown when the count is less than 0 or greater than the length of the Span.</exception>
        public Span<T> TakeLast(int count)
        {
            InvalidOperationException.ThrowIfSpanIsEmpty(source);
            ArgumentOutOfRangeException.ThrowIfNegative(count);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(count, source.Length);
            
            int start = source.Length - count;
            int length = source.Length - start;

            return source.Slice(start, length);
        }
    }

    extension<T>(ReadOnlySpan<T> source)
    {
        /// <summary>
        /// Takes the first 'count' elements from the specified <see cref="ReadOnlySpan{T}"/>.
        /// </summary>
        /// <param name="count">The number of elements to take.</param>
        /// <returns>A <see cref="ReadOnlySpan{T}"/> containing the first 'count' elements from the <see cref="ReadOnlySpan{T}"/>.</returns>
        /// <exception cref="ArgumentException">Thrown when the count is less than zero or greater than the length of the <see cref="ReadOnlySpan{T}"/>.</exception>
        public ReadOnlySpan<T> Take(int count)
        {
            InvalidOperationException.ThrowIfSpanIsEmpty(source);
            ArgumentOutOfRangeException.ThrowIfNegative(count);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(count, source.Length);
            
            return source.Slice(0, count);
        }

        /// <summary>
        /// Returns elements from the <see cref="ReadOnlySpan{T}"/> as long as the predicate condition matches elements in the <see cref="ReadOnlySpan{T}"/>.
        /// </summary>
        /// <param name="predicate">The predicate condition to test each element of the <see cref="ReadOnlySpan{T}"/> against.</param>
        /// <returns>A <see cref="ReadOnlySpan{T}"/> containing the elements that occur before the predicate condition is false.</returns>
        /// <exception cref="ArgumentException">Thrown if the <see cref="ReadOnlySpan{T}"/> is empty.</exception>
        public ReadOnlySpan<T> TakeWhile(Func<T, int, bool> predicate)
        {
            InvalidOperationException.ThrowIfSpanIsEmpty(source);
            ArgumentNullException.ThrowIfNull(predicate);
            
            int end = 0;
            int start = 0;

            bool firstDetection = true;

            for (int index = 0; index < source.Length; index++)
            {
                bool result = predicate(source[index], index);

                if (result && firstDetection)
                {
                    firstDetection = false;
                    start = index;
                }
                
                if (!result && !firstDetection)
                {
                    end = index;
                    break;
                }
            }

            return source.Slice(start, end - 1);
        }

        /// <summary>
        /// Takes the last 'count' elements from a <see cref="ReadOnlySpan{T}"/>.
        /// </summary>
        /// <param name="count">The number of elements to take from the end of the source <see cref="ReadOnlySpan{T}"/>.</param>
        /// <returns>A <see cref="ReadOnlySpan{T}"/> containing the last 'count' elements from the <see cref="ReadOnlySpan{T}"/>.</returns>
        /// <exception cref="ArgumentException">Thrown when the count is less than 0 or greater than the length of the <see cref="ReadOnlySpan{T}"/>.</exception>
        public ReadOnlySpan<T> TakeLast(int count)
        {
            InvalidOperationException.ThrowIfSpanIsEmpty(source);
            ArgumentOutOfRangeException.ThrowIfNegative(count);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(count, source.Length);
            
            int start = source.Length - count;
            int length = source.Length - start;

            return source.Slice(start, length);
        }
    }
}