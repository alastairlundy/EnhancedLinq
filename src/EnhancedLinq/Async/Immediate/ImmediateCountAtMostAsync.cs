/*
      EnhancedLinq 
      Copyright (c) 2025 Alastair Lundy
      
     Licensed under the Apache License, Version 2.0 (the "License");
     you may not use this file except in compliance with the License.
     You may obtain a copy of the License at

         http://www.apache.org/licenses/LICENSE-2.0

     Unless required by applicable law or agreed to in writing, software
     distributed under the License is distributed on an "AS IS" BASIS,
     WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
     See the License for the specific language governing permissions and
     limitations under the License.
 */

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlastairLundy.EnhancedLinq.Async.Immediate;

public static partial class EnhancedLinqAsyncImmediate
{
    /// <summary>
    /// Asynchronously counts elements in the source sequence until it finds at most 'countToLookFor' elements.
    /// </summary>
    /// <param name="source">The asynchronous enumerable source.</param>
    /// <param name="countToLookFor">The maximum count of elements to find.</param>
    /// <returns>A boolean indicating whether at most 'countToLookFor' elements were found.</returns>
    public static async Task<bool> CountAtMostAsync<T>(this IAsyncEnumerable<T> source, int countToLookFor)
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
    /// <param name="source">The asynchronous enumerable source.</param>
    /// <param name="countToLookFor">The maximum count of elements to find.</param>
    /// <param name="predicate">The predicate condition to use.</param>
    /// <returns>A boolean indicating whether at most 'countToLookFor' elements were found.</returns>
    public static async Task<bool> CountAtMostAsync<T>(this IAsyncEnumerable<T> source, Func<T, bool> predicate,
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