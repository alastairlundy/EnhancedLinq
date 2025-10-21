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
    /// Determines whether there are at least a specified number of elements in the sequence.
    /// </summary>
    /// <param name="source">The source sequence.</param>
    /// <param name="countToLookFor">The minimum count to look for.</param>
    /// <typeparam name="T">The element type in the source sequence.</typeparam>
    /// <returns><c>true</c> if there are at least the specified number of elements in the sequence; otherwise, <c>false</c>.</returns>
    public static bool CountAtLeast<T>(this IEnumerable<T> source, int countToLookFor)
    {
#if NET8_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(source);
        #endif
        
        if (source is ICollection<T> collection)
        {
            return collection.Count >= countToLookFor;
        }
        
        if (countToLookFor < 0)
            throw new ArgumentException(Resources.Exceptions_Count_LessThanZero.Replace("{x}", countToLookFor.ToString()));

        int currentCount = 0;

        foreach (T obj in source)
        {
            if(currentCount >= countToLookFor)
                return true;

            currentCount += 1;
        }

        return false;
    }

    /// <summary>
    /// Determines whether there are at least a specified number of elements in the sequence that meet a given condition.
    /// </summary>
    /// <param name="source">The source sequence.</param>
    /// <param name="predicate">The predicate condition to check elements against.</param>
    /// <param name="countToLookFor">The minimum count to look for.</param>
    /// <typeparam name="T">The element type in the source sequence.</typeparam>
    /// <returns><c>true</c> if there are at least the specified number of elements that meet the condition; otherwise, <c>false</c>.</returns>
    public static bool CountAtLeast<T>(this IEnumerable<T> source, Func<T, bool> predicate,
        int countToLookFor)
    {
#if NET8_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(source);
#endif
        
        if (countToLookFor < 0)
            throw new ArgumentException(Resources.Exceptions_Count_LessThanZero.Replace("{x}", countToLookFor.ToString()));
        
        int currentCount = 0;

        foreach (T obj in source)
        {
            if (predicate(obj))
                currentCount += 1;
            
            if(currentCount >= countToLookFor)
                return true;
        }

        return false;
    }
}