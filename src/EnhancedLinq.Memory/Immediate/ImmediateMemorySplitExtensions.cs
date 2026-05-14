/*
    EnhancedLinq.Memory
    Copyright (c) 2025-2026 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/.
    */

namespace EnhancedLinq.Memory.Immediate;

/// <summary>
///     Extension methods for splitting memory using immediate memory operations.
/// </summary>
public static class ImmediateMemorySplitExtensions
{
    /// <param name="span">The span to split.</param>
    /// <typeparam name="T">The type of elements within the span.</typeparam>
    extension<T>(Span<T> span)
        where T : notnull
    {
        /// <summary>
        ///     Splits a span into an <see cref="IList{T}" /> of arrays based on the specified item count per array.
        /// </summary>
        /// <param name="count">The maximum number of items in each array.</param>
        /// <returns>An <see cref="IList{T}" /> of arrays, where each array contains a maximum of count elements.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the count is less than or equal to zero.</exception>
        public IList<T[]> SplitByItemCount(int count)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(count);

            List<T[]> list = [];

            if (span.Length == 0) return list;

            for (int i = 0; i < span.Length; i += count)
            {
                int length = Math.Min(count, span.Length - i);
                list.Add(span.Slice(i, length).ToArray());
            }

            return list;
        }

        /// <summary>
        ///     Splits a span into an <see cref="IList{T}" /> of arrays based on the number of processors available.
        /// </summary>
        /// <returns>An <see cref="IList{T}" /> of arrays, where the span is split by the number of processors available.</returns>
        public IList<T[]> SplitByProcessorCount()
        {
            return span.SplitByItemCount(Environment.ProcessorCount);
        }

        /// <summary>
        ///     Splits a span into an <see cref="IList{T}" /> of arrays based on a specified maximum number of arrays.
        /// </summary>
        /// <param name="maximumNumberOfArrays">The maximum number of arrays to divide the span into.</param>
        /// <returns>An <see cref="IList{T}" /> of arrays, where the span is divided into at most the maximum number the arrays.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the maximum number of arrays is less than or equal to zero.</exception>
        public IList<T[]> SplitByArrayCount(int maximumNumberOfArrays)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(maximumNumberOfArrays);

            if (span.Length == 0) return [];

            int maxItemCount = (int)Math.Ceiling((double)span.Length / maximumNumberOfArrays);

            return span.SplitByItemCount(maxItemCount);
        }

        /// <summary>
        ///     Splits a span by a separator, into a list of spans.
        /// </summary>
        /// <param name="separator">The separator to split by.</param>
        /// <returns>A list of spans, each containing the elements before the separator was found.</returns>
        public IList<T[]> SplitBy(T separator) 
            => span.SplitBy(x => x.Equals(separator));

        /// <summary>
        ///     Splits a span into an <see cref="IList{T}" /> of arrays based on the provided predicate.
        /// </summary>
        /// <param name="predicate">A function that returns true or false indicating if an element should start a new array.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public IList<T[]> SplitBy(Func<T, bool> predicate)
        {
            ArgumentNullException.ThrowIfNull(predicate);

            List<T[]> list = [];

            int start = 0;

            for (int i = 0; i < span.Length; i++)
            {
                if (predicate(span[i]))
                {
                    list.Add(span.Slice(start, i - start).ToArray());
                    start = i + 1;
                }
            }

            if (start < span.Length)
            {
                list.Add(span.Slice(start, span.Length - start).ToArray());
            }
            else if (start == span.Length)
            {
                // If the last element was a separator, we should add an empty array for the trailing part
                // depending on the desired behavior. Standard string.Split usually does this.
                // Given the original code's logic, let's see if it intended to omit trailing empty.
                // The original logic was: else if (i == span.Length - 1 && start != -1) 
                // which adds the last a fragment if it's not empty.
            }

            return list;
        }
    }
}