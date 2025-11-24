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

    /// <summary>
    /// Determines if none of the elements in a span match a predicate condition.
    /// </summary>
    /// <param name="span">The span to be searched.</param>
    /// <param name="predicate">The predicate to check elements against.</param>
    /// <typeparam name="TSource">The type of elements in the span.</typeparam>
    /// <returns>True if none of the elements matched the predicate, false otherwise.</returns>
    /// <exception cref="ArgumentNullException">Thrown if the predicate is null.</exception>
    /// <exception cref="ArgumentException">Thrown if the span is empty.</exception>
    public static bool None<TSource>(this Span<TSource> span, Func<TSource, bool> predicate)
        => CountAtMost(span, predicate, 0);

    /// <summary>
    /// Determines if none of the elements in a span match a predicate condition.
    /// </summary>
    /// <param name="span">The span to be searched.</param>
    /// <param name="predicate">The predicate to check elements against.</param>
    /// <typeparam name="TSource">The type of elements in the span.</typeparam>
    /// <returns>True if none of the elements matched the predicate, false otherwise.</returns>
    /// <exception cref="ArgumentNullException">Thrown if the predicate is null.</exception>
    /// <exception cref="ArgumentException">Thrown if the span is empty.</exception>
    public static bool None<TSource>(this ReadOnlySpan<TSource> span, Func<TSource, bool> predicate)
        => CountAtMost(span, predicate, 0);


    /// <summary>
    /// Determines if none of the elements in a Memory match a predicate condition.
    /// </summary>
    /// <param name="memory">The memory to be searched.</param>
    /// <param name="predicate">The predicate to check elements against.</param>
    /// <typeparam name="TSource">The type of elements in the Memory.</typeparam>
    /// <returns>True if none of the elements matched the predicate, false otherwise.</returns>
    /// <exception cref="ArgumentNullException">Thrown if the predicate is null.</exception>
    /// <exception cref="ArgumentException">Thrown if the Memory is empty.</exception>
    public static bool None<TSource>(this Memory<TSource> memory, Func<TSource, bool> predicate)
    {
        ArgumentNullException.ThrowIfNull(predicate);
        
        for (int i = 0; i < memory.Length; i++)
        {
             TSource item = memory.Span[i];
             
            if (predicate(item) == true)
                return false;
        }
        
        return true;
    }
    
    /// <summary>
    /// Determines if none of the elements in a Memory match a predicate condition.
    /// </summary>
    /// <param name="memory">The memory to be searched.</param>
    /// <param name="predicate">The predicate to check elements against.</param>
    /// <typeparam name="TSource">The type of elements in the Memory.</typeparam>
    /// <returns>True if none of the elements matched the predicate, false otherwise.</returns>
    /// <exception cref="ArgumentNullException">Thrown if the predicate is null.</exception>
    /// <exception cref="ArgumentException">Thrown if the Memory is empty.</exception>
    public static bool None<TSource>(this ReadOnlyMemory<TSource> memory, Func<TSource, bool> predicate)
    {
        ArgumentNullException.ThrowIfNull(predicate);
        
        for (int i = 0; i < memory.Length; i++)
        {
            TSource item = memory.Span[i];
             
            if (predicate(item) == true)
                return false;
        }
        
        return true;
    }
}