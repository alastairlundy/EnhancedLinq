/*
    EnhancedLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
    */

using System.Linq;
using System.Threading.Tasks;

namespace EnhancedLinq.Async.Immediate;

public static partial class EnhancedLinqAsyncImmediate
{
    /// <param name="source">The IAsyncEnumerable to check for emptiness.</param>
    /// <typeparam name="TSource"></typeparam>
    extension<TSource>(IAsyncEnumerable<TSource> source)
    {
        /// <summary>
        /// Asynchronously checks if an IAsyncEnumerable is empty.
        /// </summary>
        /// <returns>A task representing the asynchronous operation. Returns true if the source is empty, false otherwise.</returns>
        public async ValueTask<bool> IsEmptyAsync()
        {
            ArgumentNullException.ThrowIfNull(source);

            bool anyAsync = await source.AnyAsync();
            return !anyAsync;
        }
    }
}