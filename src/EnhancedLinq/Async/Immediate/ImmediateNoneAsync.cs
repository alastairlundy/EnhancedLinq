/*
    EnhancedLinq
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/.
 */

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlastairLundy.EnhancedLinq.Async.Immediate;

public static partial class EnhancedLinqAsyncImmediate
{
    /// <summary>
    /// Determines if none of the elements in the sequence match a predicate condition.
    /// </summary>
    /// <param name="source">The <see cref="IAsyncEnumerable{T}"/> to be searched.</param>
    /// <param name="predicate">The predicate to check elements against.</param>
    /// <typeparam name="TSource">The type of elements in the sequence.</typeparam>
    /// <returns>True if none of the elements matched the predicate, false otherwise.</returns>
    /// <exception cref="ArgumentNullException">Thrown if the source sequence or predicate are null.</exception>
    public static async Task<bool> NoneAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, bool> predicate)
        => await CountAtMostAsync(source, predicate, 0);
}