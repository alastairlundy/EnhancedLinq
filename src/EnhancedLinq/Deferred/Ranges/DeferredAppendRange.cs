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

/// <summary>
/// This static partial class contains Deferred Execution Range related extension methods (such as <see cref="AppendRange{TSource}"/>
/// or <see cref="RemoveRange{TSource}"/>)
/// </summary>
public static partial class EnhancedLinqDeferredRange
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="source">The sequence to append items to.</param>
    /// <typeparam name="TSource">The type of element in the sequence and elements being appended.</typeparam>
    extension<TSource>(IEnumerable<TSource> source)
    {
        /// <summary>
        /// Appends one sequence of elements to the end of another specified sequence.
        /// </summary>
        /// <param name="toBeAppended">The elements to append to the sequence.</param>
        /// <returns>A new sequence made up of the source sequence and the appended sequence.</returns>
        public IEnumerable<TSource> AppendRange(IEnumerable<TSource> toBeAppended)
        { 
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(toBeAppended);
        
            return new AppendRangeEnumerable<TSource>(source, toBeAppended);
        }
    }
}