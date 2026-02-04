/*
    EnhancedLinq.Async
    Copyright (c) 2025-2026 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/.
*/

using System.Linq;

namespace EnhancedLinq.Async.Deferred;

public static partial class EnhancedLinqAsyncDeferred
{
    /// <summary>
    /// Extension methods for deferred asynchronous operations on <see cref="IAsyncEnumerable{T}"/>.
    /// </summary>
    extension<TSource>(IAsyncEnumerable<TSource> source)
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public IAsyncEnumerable<int> FirstNIndicesOf(TSource target, int count)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(count);
            ArgumentNullException.ThrowIfNull(source);
            
            return source.IndicesOfAsync(target).Take(count);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="selector"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public IAsyncEnumerable<int> FirstNIndicesOf(Func<TSource, bool> selector, int count)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(count);
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);

            
            return source.IndicesOfAsync(selector).Take(count);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public IAsyncEnumerable<int> LastNIndicesOf(TSource target, int count)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(count);
            ArgumentNullException.ThrowIfNull(source);
            
            return source.IndicesOfAsync(target).TakeLast(count);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="selector"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public IAsyncEnumerable<int> LastNIndicesOf(Func<TSource, bool> selector, int count)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(count);
            ArgumentNullException.ThrowIfNull(selector);
            ArgumentNullException.ThrowIfNull(source);
            
            return source.IndicesOfAsync(selector).TakeLast(count);
        }
    }
}