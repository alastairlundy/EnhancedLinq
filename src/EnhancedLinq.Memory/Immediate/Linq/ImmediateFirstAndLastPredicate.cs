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

namespace AlastairLundy.EnhancedLinq.Memory.Immediate;

public static partial class EnhancedLinqMemoryImmediate
{
    
    /// <summary>
    /// Returns the first element of a span that satisfies a specified condition.
    /// </summary>
    /// <param name="target">The span to search for the first element.</param>
    /// <param name="predicate">A function that defines the condition to be met.</param>
    /// <typeparam name="T">The type of elements in the span.</typeparam>
    /// <returns>The first element of the span that satisfies the condition.</returns>
    /// <exception cref="ArgumentException">Thrown when no element satisfies the condition.</exception>
    public static T First<T>(this Span<T> target, Func<T, bool> predicate)
    {
        for (int index = 0; index < target.Length; index++)
        {
            T item = target[index];
            if (predicate.Invoke(item))
            {
                return item;
            }
        }

        throw new ArgumentException();
    }

    /// <summary>
    /// Returns the first element of a span that satisfies a specified condition,
    /// or a default value if no such element is found.
    /// </summary>
    /// <param name="target">The span to search for the first element.</param>
    /// <param name="predicate">A function that defines the condition to be met.</param>
    /// <typeparam name="T">The type of elements in the span.</typeparam>
    /// <returns>The first element of the span that satisfies the condition, or null if no such element is found.</returns>
    /// <exception cref="ArgumentException">Thrown when the span is empty or no element satisfies the condition.</exception>
    public static T? FirstOrDefault<T>(this Span<T> target, Func<T, bool> predicate)
    {
        for (int index = 0; index < target.Length; index++)
        {
            T item = target[index];
            if (predicate.Invoke(item))
            {
                return item;
            }
        }

        return default;
    }
    
    /// <summary>
    /// Returns the last element of a span that satisfies a specified condition.
    /// </summary>
    /// <param name="target">The span to search for the last element.</param>
    /// <param name="predicate">A function that defines the condition to be met.</param>
    /// <typeparam name="T">The type of elements in the span.</typeparam>
    /// <returns>The last element of the span that satisfies the condition.</returns>
    /// <exception cref="ArgumentException">Thrown when no element satisfies the condition.</exception>
    public static T Last<T>(this Span<T> target, Func<T, bool> predicate)
    {
        Span<T> newTarget = target;
        newTarget.Reverse();
        
        for (int index = 0; index < target.Length; index++)
        {
            T item = newTarget[index];
            if (predicate.Invoke(item))
            {
                return item;
            }
        }

        throw new ArgumentException();
    }

    /// <summary>
    /// Returns the last element of a span that satisfies a specified condition, or a default value if no such element is found.
    /// </summary>
    /// <param name="target">The span to search for the last element.</param>
    /// <param name="predicate">A function that defines the condition to be met.</param>
    /// <typeparam name="T">The type of elements in the span.</typeparam>
    /// <returns>The last element of the span that satisfies the condition, or null if no such element is found.</returns>
    public static T? LastOrDefault<T>(this Span<T> target, Func<T,  bool> predicate)
    {
        Span<T> newTarget = target;
        newTarget.Reverse();
        
        for (int index = 0; index < target.Length; index++)
        {
            T item = newTarget[index];
            if (predicate.Invoke(item))
            {
                return item;
            }
        }

        return default;
    }
}