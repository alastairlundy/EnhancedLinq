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

/// <summary>
/// Provides immediate mode asynchronous operations using Async methods.
/// </summary>
public static partial class EnhancedLinqAsyncImmediate
{
    /// <summary>
    /// Determines if none of the elements in the sequence match a predicate condition.
    /// </summary>
    /// <param name="source">The <see cref="IAsyncEnumerable{T}"/> to be searched.</param>
    /// <param name="predicate">The predicate to check elements against.</param>
    /// <typeparam name="TSource">The type of elements in the sequence.</typeparam>
    /// <returns>True if none of the elements matched the predicate, false otherwise.</returns>
    /// <exception cref="ArgumentNullException">Thrown if the source sequence or predicate are null.</exception>
    public static async Task<bool> NoneAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, bool> predicate)
        => await CountAtMostAsync(source, predicate, 0);
}