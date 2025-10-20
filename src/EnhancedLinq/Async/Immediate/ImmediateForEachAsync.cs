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
    /// Applies the given action for each element of this sequence.
    /// </summary>
    /// <param name="action">The action to apply to each element in the sequence.</param>
    /// <param name="target">The sequence to apply the action for.</param>
    /// <typeparam name="T">The type of elements in the sequence.</typeparam>
    public static async Task ForEachAsync<T>(this IAsyncEnumerable<T> target, Action<T> action)
    {
#if NET8_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(target);
#endif
        
        await foreach (T item in target)
        {
            action.Invoke(item);
        }
    }
}