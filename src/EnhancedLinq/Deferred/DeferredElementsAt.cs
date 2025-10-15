/*
    EnhancedLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/.
 */

using System;
using System.Collections.Generic;
using System.Linq;

using AlastairLundy.DotPrimitives.Collections.Enumerables;

using AlastairLundy.EnhancedLinq.Deferred.Enumerators;
using AlastairLundy.EnhancedLinq.Internals.Localizations;

namespace AlastairLundy.EnhancedLinq.Deferred;

/// <summary>
/// This static partial class contains Deferred Execution extension methods for the <see cref="IEnumerable{T}"/> interface.
/// </summary>
public static partial class EnhancedLinqDeferred
{
    /// <summary>
    /// Returns a sequence of elements from the specified source, 
    /// where the index of each element in the returned sequence corresponds to an index in the provided indices.
    /// </summary>
    /// <remarks>The order of the elements in the returned <see cref="IEnumerable{T}"/> is determined by their original position in the source,
    /// but the order within the returned <see cref="IEnumerable{T}"/> is based on the provided indexes.</remarks>
    /// <param name="source">The <see cref="IEnumerable{T}"/> from which to retrieve elements.</param>
    /// <param name="indices">A sequence of indices, where each index corresponds to an element in the source.</param>
    /// <typeparam name="TSource">The type of the elements in the source and returned <see cref="IEnumerable{T}"/>.</typeparam>
    /// <returns>A new <see cref="IEnumerable{T}"/> containing the elements at the specified indexes from the original source.</returns>
    public static IEnumerable<TSource> ElementsAt<TSource>(this IEnumerable<TSource> source,
        IEnumerable<int> indices)
    {
#if NET8_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(indices);
#endif
        
        return new CustomEnumeratorEnumerable<TSource>(
            new ElementsAtEnumerator<TSource>(source, indices));
    }


#if NET8_0_OR_GREATER
    /// <summary>
    /// Returns a sequence of elements from the specified source, 
    /// where the index of each element in the returned sequence corresponds to an index in the provided indices.
    /// </summary>
    /// <remarks>The order of the elements in the returned <see cref="IEnumerable{T}"/> is determined by their original position in the source,
    /// but the order within the returned <see cref="IEnumerable{T}"/> is based on the provided indexes.</remarks>
    /// <param name="source">The <see cref="IEnumerable{T}"/> from which to retrieve elements.</param>
    /// <param name="range">The range of indices to retrieve, where each index corresponds to an element in the source.</param>
    /// <typeparam name="TSource">The type of the elements in the source and returned <see cref="IEnumerable{T}"/>.</typeparam>
    /// <returns>A new <see cref="IEnumerable{T}"/> containing the elements at the specified indexes from the original source.</returns>
    public static IEnumerable<TSource> ElementsAt<TSource>(this IEnumerable<TSource> source, Range range)
        => ElementsAt(source, range.Start.Value, Math.Abs(range.Start.Value - range.End.Value));
#endif
    
    
    /// <summary>
    /// Returns a sequence of elements from the specified source, 
    /// where the index of each element in the returned sequence corresponds to an index in the provided indices.
    /// </summary>
    /// <remarks>The order of the elements in the returned <see cref="IEnumerable{T}"/> is determined by their original position in the source,
    /// but the order within the returned <see cref="IEnumerable{T}"/> is based on the provided indexes.</remarks>
    /// <param name="source">The <see cref="IEnumerable{T}"/> from which to retrieve elements.</param>
    /// <param name="startIndex">The first index to retrieve elements at.</param>
    /// <param name="count">The number of elements from the <see cref="startIndex"/> to retrieve.</param>
    /// <typeparam name="TSource">The type of the elements in the source and returned <see cref="IEnumerable{T}"/>.</typeparam>
    /// <returns>A new <see cref="IEnumerable{T}"/> containing the elements at the specified indexes from the original source.</returns>
    /// <exception cref="IndexOutOfRangeException">Thrown if the starting index is less than 0.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the count is less than or equal to 0.</exception>
    public static IEnumerable<TSource> ElementsAt<TSource>(this IEnumerable<TSource> source,
        int startIndex, int count)
    {
#if NET8_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(source);
#endif

        if(startIndex < 0)
            throw new IndexOutOfRangeException(Resources.Exceptions_IndexOutOfRange
                .Replace("{x}", startIndex.ToString())
                .Replace("{y}", "0")
                .Replace("{z}", source.Count().ToString()));

        if (count <= 0)
            throw new ArgumentOutOfRangeException(Resources.Exceptions_Count_LessThanZero
                .Replace("{x}", count.ToString()));
        
        IEnumerable<int> sequence = startIndex.GenerateNumberRange(count, 1);
       
        return ElementsAt(source, sequence);
    }
}