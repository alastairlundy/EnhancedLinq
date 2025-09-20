/*
    EnhancedLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/.
 */

using System;
using System.Diagnostics.CodeAnalysis;

namespace AlastairLundy.EnhancedLinq.Memory.Immediate;

public static partial class EnhancedLinqMemoryImmediate
{
    /// <summary>
    /// Transforms elements of a Span according to behaviour defined by the predicate.
    /// </summary>
    /// <param name="source">The span to search.</param>
    /// <param name="predicate">The predicate to use.</param>
    /// <typeparam name="TSource">The type of elements in the source Span.</typeparam>
    /// <typeparam name="TResult">The type of elements the predicate transforms elements into.</typeparam>
    /// <returns>The newly created Span with the elements transformed by the predicate.</returns>
    public static Span<TResult> Select<TSource, TResult>(
        [NotNull]
        this Span<TSource> source,
        [NotNull]
        Func<TSource, TResult> predicate)
    {
        TResult[] array = new  TResult[source.Length];
        
        int index = 0;
        
        source.ForEach(x =>
        {
            array[index] = predicate.Invoke(x);
            index++;
        });

        return new Span<TResult>(array);
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="predicate"></param>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <returns></returns>
    public static Memory<TResult> Select<TSource, TResult>(
        [NotNull]
        this Memory<TSource> source,
        [NotNull]
        Func<TSource, TResult> predicate)
    {
        TResult[] array = new  TResult[source.Length];
        
        int index = 0;
        
        source.ForEach(x =>
        {
            array[index] = predicate.Invoke(x);
            index++;
        });
        
        return new Memory<TResult>(array);
    }
}