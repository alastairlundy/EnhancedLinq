/*
    EnhancedLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/.
 */

using System;
using System.Collections.Generic;
using AlastairLundy.EnhancedLinq.Deferred.Enumerables;

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
    public static IEnumerable<TSource> ElementsAt<TSource>(this IEnumerable<TSource> source, IEnumerable<int> indices)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(indices);
        
        return new ElementsAtEnumerable<TSource>(source, indices);
    }
}