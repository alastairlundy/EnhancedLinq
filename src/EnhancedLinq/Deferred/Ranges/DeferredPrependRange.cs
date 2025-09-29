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

namespace AlastairLundy.EnhancedLinq.Deferred.Ranges;

public static partial class EnhancedLinqDeferredRange
{
    
    /// <summary>
    /// Prepends one sequence of elements to another specified sequence.
    /// </summary>
    /// <param name="source">The sequence to prepend items to.</param>
    /// <param name="toBePrepended">The elements to prepended to the sequence.</param>
    /// <typeparam name="TSource">The type of element in the sequence and elements being prepended.</typeparam>
    /// <returns>A new sequence made up of the prepended sequence and the source sequence.</returns>
    public static IEnumerable<TSource> PrependRange<TSource>(this IEnumerable<TSource> source,
        IEnumerable<TSource> toBePrepended)
    {
        ArgumentNullException.ThrowIfNull(source, nameof(source));
        ArgumentNullException.ThrowIfNull(toBePrepended, nameof(toBePrepended));
        
        return new PrependRangeEnumerable<TSource>(source, toBePrepended);
    }
}