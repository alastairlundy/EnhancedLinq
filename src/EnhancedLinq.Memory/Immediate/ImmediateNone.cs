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

namespace AlastairLundy.EnhancedLinq.Memory.Immediate;

public static partial class EnhancedLinqMemoryImmediate
{

    /// <summary>
    /// Determines if none of the elements in a span match a predicate condition.
    /// </summary>
    /// <param name="span">The span to be searched.</param>
    /// <param name="predicate">The predicate to check elements against.</param>
    /// <typeparam name="TSource">The type of elements in the span.</typeparam>
    /// <returns>True if none of the elements matched the predicate, false otherwise.</returns>
    /// <exception cref="ArgumentNullException">Thrown if the predicate is null.</exception>
    /// <exception cref="ArgumentException">Thrown if the span is empty.</exception>
    public static bool None<TSource>(this Span<TSource> span, Func<TSource, bool> predicate)
        => CountAtMost(span, predicate, 0);

    /// <summary>
    /// Determines if none of the elements in a span match a predicate condition.
    /// </summary>
    /// <param name="span">The span to be searched.</param>
    /// <param name="predicate">The predicate to check elements against.</param>
    /// <typeparam name="TSource">The type of elements in the span.</typeparam>
    /// <returns>True if none of the elements matched the predicate, false otherwise.</returns>
    /// <exception cref="ArgumentNullException">Thrown if the predicate is null.</exception>
    /// <exception cref="ArgumentException">Thrown if the span is empty.</exception>
    public static bool None<TSource>(this ReadOnlySpan<TSource> span, Func<TSource, bool> predicate)
        => CountAtMost(span, predicate, 0);


    /// <summary>
    /// Determines if none of the elements in a Memory match a predicate condition.
    /// </summary>
    /// <param name="memory">The memory to be searched.</param>
    /// <param name="predicate">The predicate to check elements against.</param>
    /// <typeparam name="TSource">The type of elements in the Memory.</typeparam>
    /// <returns>True if none of the elements matched the predicate, false otherwise.</returns>
    /// <exception cref="ArgumentNullException">Thrown if the predicate is null.</exception>
    /// <exception cref="ArgumentException">Thrown if the Memory is empty.</exception>
    public static bool None<TSource>(this Memory<TSource> memory, Func<TSource, bool> predicate)
    {
#if NET8_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(predicate, nameof(predicate));
#endif
        
        for (int i = 0; i < memory.Length; i++)
        {
             TSource item = memory.Span[i];
             
            if (predicate(item) == true)
                return false;
        }
        
        return true;
    }
    
    /// <summary>
    /// Determines if none of the elements in a Memory match a predicate condition.
    /// </summary>
    /// <param name="memory">The memory to be searched.</param>
    /// <param name="predicate">The predicate to check elements against.</param>
    /// <typeparam name="TSource">The type of elements in the Memory.</typeparam>
    /// <returns>True if none of the elements matched the predicate, false otherwise.</returns>
    /// <exception cref="ArgumentNullException">Thrown if the predicate is null.</exception>
    /// <exception cref="ArgumentException">Thrown if the Memory is empty.</exception>
    public static bool None<TSource>(this ReadOnlyMemory<TSource> memory, Func<TSource, bool> predicate)
    {
#if NET8_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(predicate, nameof(predicate));
#endif
        
        for (int i = 0; i < memory.Length; i++)
        {
            TSource item = memory.Span[i];
             
            if (predicate(item) == true)
                return false;
        }
        
        return true;
    }
}