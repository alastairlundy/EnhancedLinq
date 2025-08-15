/*
    ExtraLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/.
 */

using ExtraLinq.Memory.Immediate.Ranges;
using ExtraLinq.Memory.Internals.Localizations;

namespace ExtraLinq.Memory.Immediate;

public static partial class ExtraLinqMemoryImmediate
{
        /// <summary>
    /// Returns a new Span with all the elements of the span except the specified first number of elements.
    /// </summary>
    /// <param name="target">The span to make a new span from.</param>
    /// <param name="count">The number of items to skip from the beginning of the span.</param>
    /// <typeparam name="T">The type of items stored in the span.</typeparam>
    /// <returns>A new Span with all the elements of the original span except the specified number of first elements to skip.</returns>
    public static Span<T> Skip<T>(this Span<T> target, int count)
    {
        if (count > target.Length)
            throw new ArgumentOutOfRangeException(Resources
                .Exceptions_SkipCount_TooLarge);
        
        return target.GetRange(start: count, end: target.Length - count);
    }

    /// <summary>
    /// Returns a new Span with all the elements of the span except the specified last number of elements.
    /// </summary>
    /// <param name="target">The span to make a new span from.</param>
    /// <param name="count">The number of items to skip from the end of the span.</param>
    /// <typeparam name="T">The type of items stored in the span.</typeparam>
    /// <returns>A new Span with all the elements of the original span except the specified last number of elements to skip.</returns>
    public static Span<T> SkipLast<T>(this Span<T> target, int count)
    {
        if (count > target.Length)
            throw new ArgumentOutOfRangeException(Resources
                .Exceptions_SkipCount_TooLarge);
            
        return target.GetRange(start: 0, end: target.Length - count);
    }

    /// <summary>
    /// Returns a new Span with all the elements of the original span that do not match the specified predicate func.
    /// </summary>
    /// <param name="target">The span to make a new span from.</param>
    /// <param name="predicate">The condition to use to determine whether to skip items or not in the span.</param>
    /// <typeparam name="T">The type of items stored in the span.</typeparam>
    /// <returns>A new Span with all the elements of the original Span that did not match the specified predicate func.</returns>
    public static Span<T> SkipWhile<T>(this Span<T> target, Func<T, bool> predicate)
    {
        return from item in target
            where predicate.Invoke(item) == false
            select item;
    }
}