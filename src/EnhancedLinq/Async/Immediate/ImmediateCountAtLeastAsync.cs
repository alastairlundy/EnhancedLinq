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
    /// Asynchronously checks if a source sequence contains at least 'countToLookFor' items.
    /// </summary>
    /// <param name="source">The asynchronous enumerable sequence to search.</param>
    /// <param name="countToLookFor">The minimum number of elements required for the result to be true.</param>
    /// <typeparam name="T">The type of elements in the source sequence.</typeparam>
    /// <returns>A task that returns a boolean indicating whether at least 'countToLookFor' items were found.</returns>
    public static async Task<bool> CountAtLeastAsync<T>(this IAsyncEnumerable<T> source, int countToLookFor)
    {
        int count = 0;

        await foreach (T item in source)
        {
            count++;

            if (count >= countToLookFor)
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Asynchronously checks if a source sequence contains at least 'countToLookFor' items that satisfy the provided predicate.
    /// </summary>
    /// <param name="source">The asynchronous enumerable sequence to search.</param>
    /// <param name="countToLookFor">The minimum number of elements satisfying the predicate required for the result to be true.</param>
    /// <param name="predicate">The predicate condition to use.</param>
    /// <typeparam name="T">The type of elements in the source sequence.</typeparam>
    /// <returns>A task that returns a boolean indicating whether at least 'countToLookFor' items satisfy the predicate.</returns>
    public static async Task<bool> CountAtLeastAsync<T>(this IAsyncEnumerable<T> source, Func<T, bool> predicate,
        int countToLookFor)
    {
        int count = 0;

        await foreach (T item in source)
        {
            if (predicate(item))
                count++;

            if (count >= countToLookFor)
            {
                return true;
            }
        }

        return false;
    }
}