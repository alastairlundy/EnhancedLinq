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

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlastairLundy.EnhancedLinq.Async.Immediate;

public static partial class EnhancedLinqAsyncImmediate
{
    /// <summary>
    /// Asynchronously checks if an IAsyncEnumerable is empty.
    /// </summary>
    /// <param name="source">The IAsyncEnumerable to check for emptiness.</param>
    /// <typeparam name="TSource"></typeparam>
    /// <returns>A task representing the asynchronous operation. Returns true if the source is empty, false otherwise.</returns>
    public static async Task<bool> IsEmptyAsync<TSource>(this IAsyncEnumerable<TSource> source)
    {
        bool anyAsync = await source.AnyAsync();
        
        // ReSharper disable once RedundantBoolCompare
        return anyAsync == false;
    }
}