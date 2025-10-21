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
using AlastairLundy.EnhancedLinq.Memory.Internals.Localizations;

namespace AlastairLundy.EnhancedLinq.Memory.Immediate;

public static partial class EnhancedLinqMemoryImmediate
{
    /// <summary>
    /// Returns a new Span with all the elements of the span except the specified first number of elements.
    /// </summary>
    /// <param name="target">The span to make a new span from.</param>
    /// <param name="count">The number of items to skip from the beginning of the span.</param>
    /// <typeparam name="T">The type of items stored in the span.</typeparam>
    /// <returns>A new Span with all the elements of the original span except the specified number of first elements to skip.</returns>
    public static Span<T> Skip<T>(this Span<T> target, int count)
    {
        if (count > target.Length)
            throw new ArgumentOutOfRangeException(Resources
                .Exceptions_SkipCount_TooLarge);
        
        return target.Slice(start: count, target.Length - count);
    }

    /// <summary>
    /// Returns a new Span with all the elements of the span except the specified last number of elements.
    /// </summary>
    /// <param name="target">The span to make a new span from.</param>
    /// <param name="count">The number of items to skip from the end of the span.</param>
    /// <typeparam name="T">The type of items stored in the span.</typeparam>
    /// <returns>A new Span with all the elements of the original span except the specified last number of elements to skip.</returns>
    public static Span<T> SkipLast<T>(this Span<T> target, int count)
    {
        if (count > target.Length)
            throw new ArgumentOutOfRangeException(Resources
                .Exceptions_SkipCount_TooLarge);
            
        return target.Slice(start: 0, target.Length - count);
    }

    /// <summary>
    /// Returns a new Span with all the elements of the original span that do not match the specified predicate func.
    /// </summary>
    /// <param name="target">The span to make a new span from.</param>
    /// <param name="predicate">The condition to use to determine whether to skip items or not in the span.</param>
    /// <typeparam name="T">The type of items stored in the span.</typeparam>
    /// <returns>A new Span with all the elements of the original Span that did not match the specified predicate func.</returns>
    public static Span<T> SkipWhile<T>(this Span<T> target, Func<T, bool> predicate)
    {
        return from item in target
            where predicate.Invoke(item) == false
            select item;
    }
}