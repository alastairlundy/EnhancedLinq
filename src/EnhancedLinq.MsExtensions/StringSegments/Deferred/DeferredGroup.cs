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

using AlastairLundy.EnhancedLinq.MsExtensions.StringSegments.Deferred.Enumerables;

using Microsoft.Extensions.Primitives;

namespace AlastairLundy.EnhancedLinq.MsExtensions.StringSegments.Deferred;

/// <summary>
/// 
/// </summary>
public static partial class EnhancedLinqSegmentDeferred
{

    /// <summary>
    /// Groups the characters in the specified <see cref="StringSegment"/> according to a specified key selector function.
    /// </summary>
    /// <typeparam name="TKey">The type of the key returned by <paramref name="selector"/>.</typeparam>
    /// <param name="target">The <see cref="StringSegment"/> whose characters to group.</param>
    /// <param name="selector">A function to extract the key for each character.</param>
    /// <returns>A sequence where each <see cref="IGrouping{TKey,TElement}"/> contains a sequence of characters that share the same key.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="target"/> is null or empty.</exception>
    public static IEnumerable<IGrouping<TKey, char>> GroupBy<TKey>(this StringSegment target, 
        Func<char, TKey> selector)
    {
        if(StringSegment.IsNullOrEmpty(target))
            throw new ArgumentNullException(nameof(target));
        
        return new GroupStringSegmentEnumerable<TKey>(target, selector);
    }
}