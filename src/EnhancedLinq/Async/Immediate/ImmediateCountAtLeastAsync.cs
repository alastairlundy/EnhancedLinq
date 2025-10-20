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
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="countToLookFor"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
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
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="predicate"></param>
    /// <param name="countToLookFor"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
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