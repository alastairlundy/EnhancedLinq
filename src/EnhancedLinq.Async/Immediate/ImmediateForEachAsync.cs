/*
    EnhancedLinq.Async
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
    */

namespace EnhancedLinq.Async.Immediate;

public static partial class EnhancedLinqAsyncImmediate
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="target">The sequence to apply the action for.</param>
    /// <typeparam name="T">The type of elements in the sequence.</typeparam>
    extension<T>(IAsyncEnumerable<T> target)
    {
        /// <summary>
        /// Applies the given action for each element of this sequence.
        /// </summary>
        /// <param name="action">The action to apply to each element in the sequence.</param>

        public async Task ForEachAsync(Action<T> action)
        {
            ArgumentNullException.ThrowIfNull(target);
            ArgumentNullException.ThrowIfNull(action);

            
            await foreach (T item in target)
            {
                action.Invoke(item);
            }
        }
    }
}