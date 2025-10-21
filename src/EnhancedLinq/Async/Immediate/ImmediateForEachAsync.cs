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
    /// Applies the given action for each element of this sequence.
    /// </summary>
    /// <param name="action">The action to apply to each element in the sequence.</param>
    /// <param name="target">The sequence to apply the action for.</param>
    /// <typeparam name="T">The type of elements in the sequence.</typeparam>
    public static async Task ForEachAsync<T>(this IAsyncEnumerable<T> target, Action<T> action)
    {
#if NET8_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(target);
#endif
        
        await foreach (T item in target)
        {
            action.Invoke(item);
        }
    }
}