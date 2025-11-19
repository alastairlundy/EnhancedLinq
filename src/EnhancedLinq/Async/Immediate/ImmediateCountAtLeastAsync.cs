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
    /// <param name="source">The asynchronous enumerable sequence to search.</param>
    /// <typeparam name="T">The type of elements in the source sequence.</typeparam>
    extension<T>(IAsyncEnumerable<T> source)
    {
        /// <summary>
        /// Asynchronously checks if a source sequence contains at least 'countToLookFor' items.
        /// </summary>
        /// <param name="countToLookFor">The minimum number of elements required for the result to be true.</param>
        /// <returns>A task that returns a boolean indicating whether at least 'countToLookFor' items were found.</returns>
        public async Task<bool> CountAtLeastAsync(int countToLookFor)
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
        /// <param name="countToLookFor">The minimum number of elements satisfying the predicate required for the result to be true.</param>
        /// <param name="predicate">The predicate condition to use.</param>
        /// <returns>A task that returns a boolean indicating whether at least 'countToLookFor' items satisfy the predicate.</returns>
        public async Task<bool> CountAtLeastAsync(Func<T, bool> predicate,
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
}