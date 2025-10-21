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
using AlastairLundy.EnhancedLinq.Internals.Localizations;

namespace AlastairLundy.EnhancedLinq.Immediate;

public static partial class EnhancedLinqImmediate
{
    /// <summary>
    /// Retrieves the element at a specified index from the sequence.
    /// </summary>
    /// <param name="source">The sequence to retrieve the element from.</param>
    /// <param name="index">The zero-based index of the element to retrieve.</param>
    /// <typeparam name="T">The type of elements in the sequence.</typeparam>
    /// <returns>The element at the specified index in the sequence, or throws an exception if no such element exists.</returns>
    /// <exception cref="ArgumentException">Thrown when no element is found at the specified index.</exception>
    public static T ElementAt<T>(this IEnumerable<T> source, int index)
    {
        if (source is IList<T> list)
        {
            return list[index];
        }
        
        int i = 0;

        foreach (T item in source)
        {
            if (i == index)
            {
                return item;
            }

            ++i;
        }

        throw new ArgumentException(Resources.Exceptions_ValueNotFound_AtIndex.Replace("{y}", nameof(source))
            .Replace("{x}",$"{index}"));
    }
}