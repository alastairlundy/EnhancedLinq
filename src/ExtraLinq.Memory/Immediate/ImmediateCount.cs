/*
    ExtraLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/.
 */

namespace ExtraLinq.Memory.Immediate;

public static class ImmediateCount
{
    /// <summary>
    /// Returns the number of elements in a given span that satisfy a condition.
    /// </summary>
    /// <param name="source">The span to search.</param>
    /// <param name="predicate">A func that takes an element and returns a boolean indicating whether it should be counted.</param>
    /// <typeparam name="TSource">The type of elements in the span.</typeparam>
    /// <returns>The number of elements that satisfy the predicate.</returns>
    public static int Count<TSource>(this Span<TSource> source,
        Func<TSource, bool> predicate)
    {
        int count = 0;

        foreach (TSource item in source)
        {
            if (predicate.Invoke(item))
            {
                count++;
            }
        }
        
        return count;
    }
}