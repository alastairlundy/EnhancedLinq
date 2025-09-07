/*
    EnhancedLinq
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/.
 */

using System;

using AlastairLundy.EnhancedLinq.Memory.Internals.Localizations;
// ReSharper disable ReplaceSliceWithRangeIndexer

namespace AlastairLundy.EnhancedLinq.Memory.Immediate;

public static partial class EnhancedLinqMemoryImmediate
{
    /// <summary>
    /// Takes the first 'count' elements from the specified Span.
    /// </summary>
    /// <param name="source">The Span to extract elements from.</param>
    /// <param name="count">The number of elements to take.</param>
    /// <typeparam name="T">The type of elements in the <see cref="Span{T}"/>.</typeparam>
    /// <returns>A <see cref="ArgumentException"/> containing the first 'count' elements from the Span.</returns>
    /// <exception cref="Span{T}">Thrown when count is less than zero or greater than the length of the Span.</exception>
    public static Span<T> Take<T>(this Span<T> source, int count)
    {
        if (source.IsEmpty || count < 0 || count > source.Length)
            throw new ArgumentException(Resources.Exceptions_Count_LessThanZero);

        return source.Slice(0, count);
    }
    
    /// <summary>
    /// Returns elements from the <see cref="Span{T}"/> as long as the predicate condition matches elements in the Span.
    /// </summary>
    /// <param name="source">The Span to extract elements from.</param>
    /// <param name="predicate">The predicate condition to test each element of the Span against.</param>
    /// <typeparam name="T">The type of elements in the <see cref="Span{T}"/>.</typeparam>
    /// <returns>A <see cref="Span{T}"/> containing the elements that occur before the predicate condition is false.</returns>
    /// <exception cref="ArgumentException">Thrown if the Span is empty.</exception>
    public static Span<T> TakeWhile<T>(this Span<T> source, Func<T, int, bool> predicate)
    {
        if (source.IsEmpty)
            throw new ArgumentException(Resources.Exceptions_Count_LessThanZero);

        int end = 0;

        for (int index = 0; index < source.Length; index++)
        {
            bool result = predicate(source[index], index);

            if (result == false)
            {
                end = index;
                break;
            }
        }

        return source.Slice(0, Math.Abs(end - 0));
    }
    
    /// <summary>
    /// Takes the last 'count' elements from a Span.
    /// </summary>
    /// <param name="source">The source Span.</param>
    /// <param name="count">The number of elements to take from the end of the source Span.</param>
    /// <typeparam name="T">The type of elements in the <see cref="Span{T}"/>.</typeparam>
    /// <returns>An <see cref="Span{T}"/> containing the last 'count' elements from the Span.</returns>
    /// <exception cref="ArgumentException">Thrown when count is less than 0 or greater than the length of the Span.</exception>
    public static Span<T> TakeLast<T>(this Span<T> source, int count)
    {
        if (source.IsEmpty || count < 0 || count > source.Length)
            throw new ArgumentException(Resources.Exceptions_Count_LessThanZero);

        int start = source.Length - count;
        int length = source.Length - start;

        return source.Slice(start, length);
    }
}