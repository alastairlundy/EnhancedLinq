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

namespace AlastairLundy.EnhancedLinq.Immediate;

public static partial class EnhancedLinqImmediate
{
    /// <summary>
    /// Applies the given action for each element of this sequence.
    /// </summary>
    /// <param name="action">The action to apply to each element in the sequence.</param>
    /// <param name="target">The sequence to apply the action for.</param>
    /// <typeparam name="T">The type of elements in the sequence.</typeparam>
    public static void ForEach<T>(this IEnumerable<T> target, Action<T> action)
    {
#if NET8_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(target);
#endif
        
        foreach (T item in target)
        {
            action.Invoke(item);
        }
    }

    /// <summary>
    /// Applies the given predicate function to each element of this <see cref="ICollection{T}"/>.
    /// </summary>
    /// <param name="target">The <see cref="ICollection{T}"/> to apply the predicate to.</param>
    /// <param name="predicate">The func to apply to each element in the collection.</param>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <returns>A new <see cref="ICollection{T}"/> containing the result of applying each element to the predicate.</returns>
    public static ICollection<T> ForEach<T>(this ICollection<T> target, Func<T, T> predicate)
    {
#if NET8_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(target);
#endif
        
        List<T> output = new List<T>(capacity:target.Count);
        
        foreach (T item in target)
        {
            output.Add(predicate.Invoke(item));
        }
        
        return output;
    }
    
    /// <summary>
    /// Applies the given predicate function to each element of this <see cref="IList{T}"/>.
    /// </summary>
    /// <param name="target">The <see cref="IList{T}"/> to apply the selector to.</param>
    /// <param name="selector">The func to apply to each element in the <see cref="IList{T}"/>.</param>
    /// <typeparam name="T">The type of items in the <see cref="IList{T}"/>.</typeparam>
    public static void ForEach<T>(this IList<T> target, Func<T, T> selector)
    {
#if NET8_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(target);
#endif
        
        for (int index = 0; index < target.Count; index++)
        {
            target[index] = selector.Invoke(target[index]);
        }
    }
}