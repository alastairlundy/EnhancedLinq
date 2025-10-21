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

namespace AlastairLundy.EnhancedLinq.Deferred.Ranges;

/// <summary>
/// This static partial class contains Deferred Execution Range related extension methods (such as <see cref="AppendRange{TSource}"/> or <see cref="RemoveRange{TSource}"/>)
/// for the <see cref="IEnumerable{T}"/> interface.
/// </summary>
public static partial class EnhancedLinqDeferredRange
{
    
    /// <summary>
    /// Removes items from an IEnumerable.
    /// </summary>
    /// <param name="source">The IEnumerable to have items removed from.</param>
    /// <param name="itemsToBeRemoved">The items to be removed.</param>
    /// <typeparam name="TSource">The type of elements stored in the sequence.</typeparam>
    /// <returns>The new IEnumerable with the specified items removed.</returns>
    public static IEnumerable<TSource> RemoveRange<TSource>(this IEnumerable<TSource> source, IEnumerable<TSource> itemsToBeRemoved)
    {
        return from item in source
            where itemsToBeRemoved.Contains(item) == false
            select item;
    }
}