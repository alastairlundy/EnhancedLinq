/*
    EnhancedLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/.
 */

using System;

using AlastairLundy.EnhancedLinq.Memory.Internals.Localizations;

namespace AlastairLundy.EnhancedLinq.Memory.Immediate;

public static partial class EnhancedLinqMemoryImmediate
{
    /// <summary>
    /// Determines whether there are at least a specified number of elements in the <see cref="Span{T}"/>.
    /// </summary>
    /// <param name="source">The source <see cref="Span{T}"/>.</param>
    /// <param name="countToLookFor">The minimum count to look for.</param>
    /// <typeparam name="T">The element type in the source <see cref="Span{T}"/>.</typeparam>
    /// <returns><c>true</c> if there are at least the specified number of elements in the <see cref="Span{T}"/>; otherwise, <c>false</c>.</returns>
    public static bool CountAtLeast<T>(this Span<T> source, int countToLookFor)
    {
        if(source.IsEmpty)
            throw new InvalidOperationException(Resources.Exceptions_InvalidOperation_EmptySpan);

        if (countToLookFor < 0)
            throw new ArgumentException(Resources.Exceptions_Count_LessThanZero.Replace("{x}", countToLookFor.ToString()));

        return source.Length >= countToLookFor;
    }

    /// <summary>
    /// Determines whether there are at least a specified number of elements in the <see cref="Span{T}"/> that meet a given condition.
    /// </summary>
    /// <param name="source">The source <see cref="Span{T}"/>.</param>
    /// <param name="predicate">The predicate condition to check elements against.</param>
    /// <param name="countToLookFor">The minimum count to look for.</param>
    /// <typeparam name="T">The element type in the source <see cref="Span{T}"/>.</typeparam>
    /// <returns><c>true</c> if there are at least the specified number of elements that meet the condition; otherwise, <c>false</c>.</returns>
    public static bool CountAtLeast<T>(this Span<T> source, Func<T, bool> predicate,
        int countToLookFor)
    {
        if(source.IsEmpty)
            throw new InvalidOperationException(Resources.Exceptions_InvalidOperation_EmptySpan);

        if (countToLookFor < 0)
            throw new ArgumentException(Resources.Exceptions_Count_LessThanZero.Replace("{x}", countToLookFor.ToString()));
        
        int currentCount = 0;

        foreach (T obj in source)
        {
            if (predicate(obj))
                currentCount += 1;
            
            if(currentCount >= countToLookFor)
                return true;
        }

        return false;
    }
}