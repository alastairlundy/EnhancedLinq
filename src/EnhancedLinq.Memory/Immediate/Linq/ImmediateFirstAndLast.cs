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

/// <summary>
/// 
/// </summary>
public static partial class EnhancedLinqMemoryImmediate
{
      /// <summary>
    /// Returns the first element in the Span.
    /// </summary>
    /// <param name="target">The span to be searched.</param>
    /// <typeparam name="T">The type of items stored in the span.</typeparam>
    /// <returns>The first item in the span if any items are in the Span.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the Span contains zero items.</exception>
    public static T First<T>(this Span<T> target)
    {
        if (target.IsEmpty)
            throw new InvalidOperationException(Resources.Exceptions_InvalidOperation_EmptySpan);
        
        return target[0];
    }
      
    /// <summary>
    /// Returns the first element of a Memory sequence.
    /// </summary>
    /// <param name="source">The source Memory sequence.</param>
    /// <typeparam name="T">The type of elements in the Memory sequence.</typeparam>
    /// <returns>The first element of the Memory sequence.</returns>
    public static T First<T>(this Memory<T> source)
    {
        if (source.IsEmpty)
            throw new InvalidOperationException("The source Memory is empty.");
        
        return source.ElementAt(0);
    }

    /// <summary>
    /// Returns the first element of a span that satisfies a specified condition, or null if the Span is empty.
    /// </summary>
    /// <param name="target">The span to search for the first element.</param>
    /// <typeparam name="T">The type of elements in the span.</typeparam>
    /// <returns>The first element of the span that satisfies the condition, or null if the span is empty.</returns>
    public static T? FirstOrDefault<T>(this Span<T> target) 
        => target.IsEmpty == false ? target[0] : default;
    
    /// <summary>
    /// Returns the first element of a Memory sequence or default if it is empty.
    /// </summary>
    /// <param name="source">The source Memory sequence.</param>
    /// <typeparam name="T">The type of elements in the Memory sequence.</typeparam>
    /// <returns>The first element of the Memory or default if no elements were found.</returns>
    public static T? FirstOrDefault<T>(this Memory<T> source) 
        => source.IsEmpty ? default : source.ElementAt(0);

    /// <summary>
    /// Returns the last element in the Span.
    /// </summary>
    /// <param name="target">The span to be searched.</param>
    /// <typeparam name="T">The type of items stored in the span.</typeparam>
    /// <returns>The last item in the span if any items are in the Span.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the Span contains zero items.</exception>
    public static T Last<T>(this Span<T> target)
    {
        if (target.IsEmpty)
            throw new InvalidOperationException(Resources.Exceptions_InvalidOperation_EmptySpan);

        return target[^1];
    }
    
    /// <summary>
    /// Returns the last element of a Memory sequence.
    /// </summary>
    /// <param name="source">The source Memory sequence.</param>
    /// <typeparam name="T">The type of elements in the Memory sequence.</typeparam>
    /// <returns>The last element of the Memory sequence.</returns>
    public static T Last<T>(this Memory<T> source)
    {
        if(source.IsEmpty)
            throw new InvalidOperationException("The source Memory is empty.");

        return source.ElementAt(source.Length - 1);
    }

    /// <summary>
    /// Returns the last element of a span that satisfies a specified condition,
    /// or null if the Span is empty.
    /// </summary>
    /// <param name="target">The span to search for the last element.</param>
    /// <typeparam name="T">The type of elements in the span.</typeparam>
    /// <returns>The last element of the span, or null if the span is empty.</returns>
    public static T? LastOrDefault<T>(this Span<T> target)
    {
        if (target.IsEmpty)
            return default;
        
        return target[^1];
    }
    
    /// <summary>
    /// Returns the last element of a Memory sequence or default if it is empty.
    /// </summary>
    /// <param name="source">The source Memory sequence.</param>
    /// <typeparam name="T">The type of elements in the Memory sequence.</typeparam>
    /// <returns>The last element of the Memory or default if no elements were found.</returns>
    public static T? LastOrDefault<T>(this Memory<T> source)
    {
        if(source.IsEmpty)
            return default;

        return source.ElementAt(source.Length - 1);
    }
}