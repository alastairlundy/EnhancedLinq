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
    /// Determines whether there are at most a maximum number elements in the source <see cref="Span{T}"/>.
    /// </summary>
    /// <param name="source">The source <see cref="Span{T}"/> to search through.</param>
    /// <param name="countToLookFor">The maximum number of elements that can meet the condition.</param>
    /// <typeparam name="T">The element type of the source <see cref="Span{T}"/>.</typeparam>
    /// <returns>True if there are at most <paramref name="countToLookFor"/> number of elements, false otherwise.</returns>
    public static bool CountAtMost<T>(this Span<T> source, int countToLookFor)
    {
        return source.Length <= countToLookFor;
    }


    /// <summary>
    /// Determines whether there are at most a maximum number elements in the source <see cref="Span{T}"/> that satisfy the given condition.
    /// </summary>
    /// <param name="source">The source <see cref="Span{T}"/> to search through.</param>
    /// <param name="selector">The predicate condition to check elements against.</param>
    /// <param name="countToLookFor">The maximum number of elements that can meet the condition.</param>
    /// <typeparam name="T">The element type of the source <see cref="Span{T}"/>.</typeparam>
    /// <returns>True if there are at most <paramref name="countToLookFor"/> number of elements that satisfy the condition, false otherwise.</returns>
    public static bool CountAtMost<T>(this Span<T> source, Func<T, bool> selector,
        int countToLookFor)
    {
        if(source.IsEmpty)
            throw new InvalidOperationException(Resources.Exceptions_InvalidOperation_EmptySpan);

        if (countToLookFor < 0)
            throw new ArgumentException(Resources.Exceptions_Count_LessThanZero.Replace("{x}", countToLookFor.ToString()));

        int currentCount = 0;

        foreach (T obj in source)
        {
            if (selector(obj))
                currentCount += 1;
            
            if(currentCount >= countToLookFor)
                return false;
        }

        return true;
    }
}