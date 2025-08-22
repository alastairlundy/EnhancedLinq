/*
    EnhancedLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/.
 */

using System.Collections.Generic;
using System.Linq;

namespace EnhancedLinq.Deferred.Ranges;

/// <summary>
/// This static partial class contains Deferred Execution Range related extension methods (such as <see cref="AppendRange{TSource}"/> or <see cref="RemoveRange{TSource}"/>)
/// for the <see cref="IEnumerable{T}"/> interface.
/// </summary>
public static partial class EnhancedLinqDeferredRange
{
    /// <summary>
    /// Removes items from an IEnumerable.
    /// </summary>
    /// <param name="source">The IEnumerable to have items removed from.</param>
    /// <param name="itemsToBeRemoved">The items to be removed.</param>
    /// <typeparam name="TSource">The type of elements stored in the IEnumerable.</typeparam>
    /// <returns>The new IEnumerable with the specified items removed.</returns>
    public static IEnumerable<TSource> RemoveRange<TSource>(this IEnumerable<TSource> source, IEnumerable<TSource> itemsToBeRemoved)
    {
        return from item in source
            where itemsToBeRemoved.Contains(item) == false
            select item;
    }
}