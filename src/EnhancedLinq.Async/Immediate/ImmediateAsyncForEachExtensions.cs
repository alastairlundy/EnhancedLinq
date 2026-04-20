/*
    EnhancedLinq.Async
    Copyright (c) 2025-2026 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
    */

namespace EnhancedLinq.Async.Immediate;

/// <summary>
/// 
/// </summary>
public static class ImmediateAsyncForEachExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="target">The sequence to apply the action for.</param>
    /// <typeparam name="T">The type of elements in the sequence.</typeparam>
    extension<T>(IAsyncEnumerable<T> target)
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="selector"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public async IAsyncEnumerable<T> ForEachAsync(Func<T, Task<T>> selector)
        {
            ArgumentNullException.ThrowIfNull(selector);
            
            await foreach (T item in target.ConfigureAwait(false))
            {
                T result = await selector.Invoke(item).ConfigureAwait(false);
               
                yield return result;
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="selector"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public async IAsyncEnumerable<TResult> ForEachAsync<TResult>(Func<T, Task<TResult>> selector)
        {
            ArgumentNullException.ThrowIfNull(selector);
            
            await foreach (T item in target.ConfigureAwait(false))
            {
                TResult result = await selector.Invoke(item).ConfigureAwait(false);
                
                yield return result;
            }
        }
    }
}