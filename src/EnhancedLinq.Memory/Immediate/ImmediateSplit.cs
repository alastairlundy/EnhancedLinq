/*
    EnhancedLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
    */

namespace AlastairLundy.EnhancedLinq.Memory.Immediate;

public static partial class EnhancedLinqMemoryImmediate
{
    /// <param name="span">The span to split.</param>
    /// <typeparam name="T">The type of elements within the span.</typeparam>
    extension<T>(Span<T> span)
    where T : notnull
    {
        /// <summary>
        /// Splits a span into an <see cref="IList{T}"/> of arrays of type <see cref="T"/> based on the specified item count per array.
        /// </summary>
        /// <typeparam name="T">The type of elements within the span.</typeparam>
        /// <param name="count">The maximum number of items in each array.</param>
        /// <returns>An <see cref="IList{T}"/> of arrays, where each array contains a maximum of <paramref name="count"/> elements.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="count"/> is less than or equal to zero.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the input span is empty.</exception>
        public IList<T[]> SplitByItemCount(int count)
        {
            InvalidOperationException.ThrowIfSpanIsEmpty(span);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(count);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(count, span.Length);
            
            List<T[]> list = new();

            if (!(span.Length > count))
            {
                return [span.ToArray()];
            }

            int start = 0;
            int nextSplit = 0;

            for (int i = 0; i < span.Length; i++)
            {
                if (start == -1)
                {
                    start = i;
                }
            
                nextSplit += 1;

                if (nextSplit - start == count)
                {
                    list.Add(span.Slice(start, count).ToArray());
                    start = -1;
                }
                else if (i == span.Length - 1 && start != -1)
                {
                    list.Add(span.Slice(start, span.Length - start).ToArray());
                }
            }
        
            return list;
        }

        /// <summary>
        /// Splits a span into an <see cref="IList{T}"/> of arrays of type <see cref="T"/> based on the number of processors available.
        /// </summary>
        /// <returns></returns>
        public IList<T[]> SplitByProcessorCount()
            => SplitByItemCount(span, Environment.ProcessorCount);

        /// <summary>
        /// Splits a span into an <see cref="IList{T}"/> of arrays of type <see cref="T"/> based on a specified maximum number of arrays.
        /// </summary>
        /// <typeparam name="T">The type of elements within the span.</typeparam>
        /// <param name="maximumNumberOfArrays">The maximum number of arrays to divide the span into.</param>
        /// <returns>An <see cref="IList{T}"/> of arrays, where the span is divided into at most <paramref name="maximumNumberOfArrays"/> arrays.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="maximumNumberOfArrays"/> is less than or equal to zero.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the input span is empty.</exception>
        public IList<T[]> SplitByArrayCount(int maximumNumberOfArrays)
        {
            InvalidOperationException.ThrowIfSpanIsEmpty(span);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(maximumNumberOfArrays);

            double maxItems = Convert.ToDouble(span.Length / maximumNumberOfArrays);
            int maxItemCount;
        
            if (maxItems % 1 != 0)
            {
                maxItemCount = Convert.ToInt32(maxItems) + 1;
            }
            else
            {
                maxItemCount = Convert.ToInt32(maxItems);
            }
        
            return SplitByItemCount(span, maxItemCount);
        }

        /// <summary>
        /// Splits a span by a separator, into a list of spans.
        /// </summary>
        /// <param name="separator">The separator to split by.</param>
        /// <returns>A list of spans, each containing the elements before the separator was found.</returns>
        public IList<T[]> SplitBy(T separator)
        {
            InvalidOperationException.ThrowIfSpanIsEmpty(span);

            return SplitBy(span, x => x.Equals(separator));
        }

        /// <summary>
        /// Splits a span into an <see cref="IList{T}"/> of arrays of type <see cref="T"/> based on the provided predicate.
        /// </summary>
        /// <param name="predicate">A function that returns true or false indicating if an element should start a new array.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public IList<T[]> SplitBy(Func<T, bool> predicate)
        {
            InvalidOperationException.ThrowIfSpanIsEmpty(span);
            ArgumentNullException.ThrowIfNull(predicate);

            List<T[]> list = new();

            int start = 0;
            int nextSplit = 0;

            for (int i = 0; i < span.Length; i++)
            {
                if (start == -1)
                {
                    start = i;
                }
            
                nextSplit += 1;
            
                if (predicate(span[i]))
                {
                    list.Add(span.Slice(start, Math.Abs(nextSplit - start)).ToArray());
                    start = -1;
                }
                else if (i == span.Length - 1 && start != -1)
                {
                    list.Add(span.Slice(start, span.Length - start).ToArray());
                }
            }
        
            return list;
        }
    }
}