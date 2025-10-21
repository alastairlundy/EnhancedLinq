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
    /// Gets the first index of the first element that matches the predicate condition.
    /// </summary>
    /// <param name="source">The <see cref="IEnumerable{T}"/> to be searched.</param>
    /// <param name="predicate">The predicate condition to check elements of the sequence against.</param>
    /// <typeparam name="T">The type of elements in the sequence.</typeparam>
    /// <returns>The first index of the first element in the sequence to match the predicate condition,
    /// if the sequence contains any elements that match the predicate condition, returns -1 otherwise.
    /// </returns>
    public static int IndexOf<T>(this IEnumerable<T> source, Func<T, bool> predicate)
    {
#if NET8_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(source, nameof(source));
        ArgumentNullException.ThrowIfNull(predicate, nameof(predicate));
     #endif
        
        int index = 0;

        foreach (T item in source)
        {
            if (predicate(item))
                return index;

            index++;
        }

        return -1;
    }
    
    /// <summary>
    /// Gets the first index of an element in a sequence.
    /// </summary>
    /// <param name="source">The sequence to be searched.</param>
    /// <param name="obj">The element to get the index of.</param>
    /// <typeparam name="T">The type of elements in the sequence.</typeparam>
    /// <returns>The first index of an element in a sequence, if the sequence contains the element, returns -1 otherwise.</returns>
    public static int IndexOf<T>(this IEnumerable<T> source, T obj)
    {
#if NET8_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(source, nameof(source));
#endif
        
        if (source is IList<T> list)
        {
            return list.IndexOf(obj);
        }
        
        int index = 0;
                
        foreach (T item in source)
        {
            if (item is not null && item.Equals(obj))
            {
                return index;
            }
                    
            index++;
        }
        
        return -1;
    }
}