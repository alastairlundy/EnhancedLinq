/*
    EnhancedLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/.
 */

using System;
using System.Collections.Generic;
using EnhancedLinq.Deferred.Enumerables;

namespace EnhancedLinq.Deferred.Ranges;

/// <summary>
/// This static partial class contains Deferred Execution Range related extension methods (such as <see cref="AppendRange{TSource}"/>
/// or <see cref="RemoveRange{TSource}"/>)
/// </summary>
public static partial class EnhancedLinqDeferredRange
{
    
    /// <summary>
    /// Appends one sequence of elements to the end of another specified sequence.
    /// </summary>
    /// <param name="source">The sequence to add items to.</param>
    /// <param name="toBeAppended">The elements to add to the sequence.</param>
    /// <typeparam name="TSource">The type of element in the sequence and elements being added.</typeparam>
    public static IEnumerable<TSource> AppendRange<TSource>(this IEnumerable<TSource> source,
        IEnumerable<TSource> toBeAppended)
    { 
        ArgumentNullException.ThrowIfNull(source, nameof(source));
        ArgumentNullException.ThrowIfNull(toBeAppended, nameof(toBeAppended));
        
        return new AppendRangeEnumerable<TSource>(source, toBeAppended);
    }
}