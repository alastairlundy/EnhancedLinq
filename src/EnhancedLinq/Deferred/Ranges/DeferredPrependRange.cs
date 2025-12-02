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

public static partial class EnhancedLinqDeferredRange
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="source">The sequence to prepend items to.</param>
    /// <typeparam name="TSource">The type of element in the sequence and elements being prepended.</typeparam>
    extension<TSource>(IEnumerable<TSource> source)
    {
        /// <summary>
        /// Prepends one sequence of elements to another specified sequence.
        /// </summary>
        /// <param name="toBePrepended">The elements to prepended to the sequence.</param>
        /// <returns>A new sequence made up of the prepended sequence and the source sequence.</returns>
        public IEnumerable<TSource> PrependRange(IEnumerable<TSource> toBePrepended)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(toBePrepended);
        
            return new PrependRangeEnumerable<TSource>(source, toBePrepended);
        }
    }
}