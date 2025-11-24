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
    /// <param name="source">The asynchronous enumerable source.</param>
    extension<T>(IAsyncEnumerable<T> source)
    {
        /// <summary>
        /// Asynchronously counts elements in the source sequence until it finds at most 'countToLookFor' elements.
        /// </summary>
        /// <param name="countToLookFor">The maximum count of elements to find.</param>
        /// <returns>A boolean indicating whether at most 'countToLookFor' elements were found.</returns>
        public async Task<bool> CountAtMostAsync(int countToLookFor)
        {
            int count = 0;

            await foreach (T obj in source)
            {
                count++;

                if (count > countToLookFor)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Asynchronously counts elements in the source sequence until it finds at most 'countToLookFor' elements.
        /// </summary>
        /// <param name="countToLookFor">The maximum count of elements to find.</param>
        /// <param name="predicate">The predicate condition to use.</param>
        /// <returns>A boolean indicating whether at most 'countToLookFor' elements were found.</returns>
        public async Task<bool> CountAtMostAsync(Func<T, bool> predicate,
            int countToLookFor)
        {
            int count = 0;

            await foreach (T obj in source)
            {
                if (predicate(obj))
                    count++;

                if (count > countToLookFor)
                {
                    return false;
                }
            }

            return true;
        }
    }
}