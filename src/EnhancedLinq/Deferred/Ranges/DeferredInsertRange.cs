/*
    EnhancedLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/.
 */

using System;
using System.Collections.Generic;

using AlastairLundy.DotPrimitives.Collections.Enumerables;

using AlastairLundy.EnhancedLinq.Deferred.Enumerators.Ranges;

namespace AlastairLundy.EnhancedLinq.Deferred.Ranges;

public static partial class EnhancedLinqDeferredRange
{
    /// <summary>
    /// Inserts an element into a sequence at the specified index.
    /// </summary>
    /// <param name="source">The sequence to insert items into.</param>
    /// <param name="indexToInsertAt">The index at which to insert the element into the sequence</param>
    /// <param name="toBeInserted">The element to be inserted.</param>
    /// <typeparam name="TSource">The type of elements stored in the sequence.</typeparam>
    /// <returns>A new sequence with the elements of the original sequence, and the specified element inserted at the specified index. </returns>
    public static IEnumerable<TSource> Insert<TSource>(this IEnumerable<TSource> source, int indexToInsertAt,
        TSource toBeInserted)
        => InsertRange(source, indexToInsertAt, [toBeInserted]);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="source">The sequence to insert items into.</param>
    /// <param name="indexToInsertAt">The index at which to insert the elements into the sequence</param>
    /// <param name="toBeInserted">The sequence of elements to be inserted.</param>
    /// <typeparam name="TSource">The type of elements stored in the sequence.</typeparam>
    /// <returns>A new sequence with the elements of the original sequence, and the elements of the second sequence inserted at the specified index. </returns>
    public static IEnumerable<TSource> InsertRange<TSource>(this IEnumerable<TSource> source, int indexToInsertAt,
        IEnumerable<TSource> toBeInserted)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(toBeInserted);

        if (indexToInsertAt < 0)
            throw new IndexOutOfRangeException();
        
        return new CustomEnumeratorEnumerable<TSource>(
            new InsertRangeEnumerator<TSource>(source, indexToInsertAt, toBeInserted));
    }
}