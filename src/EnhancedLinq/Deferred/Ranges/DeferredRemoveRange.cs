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

namespace EnhancedLinq.Deferred.Ranges;

/// <summary>
/// This static partial class contains Deferred Execution Range related extension methods (such as <see cref="AppendRange{TSource}"/> or <see cref="RemoveRange{TSource}"/>)
/// for the <see cref="IEnumerable{T}"/> interface.
/// </summary>
public static partial class EnhancedLinqDeferredRange
{
    /// <param name="source">The IEnumerable to have items removed from.</param>
    /// <typeparam name="TSource">The type of elements stored in the sequence.</typeparam>
    extension<TSource>(IEnumerable<TSource> source)
    {
        /// <summary>
        /// Removes items from an IEnumerable.
        /// </summary>
        /// <param name="itemsToBeRemoved">The items to be removed.</param>
        /// <returns>The new IEnumerable with the specified items removed.</returns>
        public IEnumerable<TSource> RemoveRange(IEnumerable<TSource> itemsToBeRemoved)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(itemsToBeRemoved);
            
            return from item in source
                where itemsToBeRemoved.Contains(item) == false
                select item;
        }
    }
}