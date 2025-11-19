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
using System.Diagnostics.CodeAnalysis;

namespace AlastairLundy.EnhancedLinq.Memory.Immediate;

public static partial class EnhancedLinqMemoryImmediate
{
    /// <summary>
    /// Transforms elements of a Span according to behaviour defined by the predicate.
    /// </summary>
    /// <param name="source">The span to search.</param>
    /// <param name="predicate">The predicate to use.</param>
    /// <typeparam name="TSource">The type of elements in the source Span.</typeparam>
    /// <typeparam name="TResult">The type of elements the predicate transforms elements into.</typeparam>
    /// <returns>The newly created Span with the elements transformed by the predicate.</returns>
    public static Span<TResult> Select<TSource, TResult>(
#if NET8_0_OR_GREATER
        [NotNull]
#endif
        this Span<TSource> source,
#if NET8_0_OR_GREATER
        [NotNull]
#endif
        Func<TSource, TResult> predicate)
    {
        TResult[] array = new  TResult[source.Length];
        
        int index = 0;
        
        source.ForEach(x =>
        {
            array[index] = predicate.Invoke(x);
            index++;
        });

        return new Span<TResult>(array);
    }

    /// <summary>
    /// Transforms elements of a Span into a new Span using the provided predicate.
    /// </summary>
    /// <param name="source">The source span containing elements to be transformed.</param>
    /// <param name="predicate">The transformation function to apply to each element in the span.</param>
    /// <typeparam name="TSource">The type of elements in the source span.</typeparam>
    /// <typeparam name="TResult">The type of elements in the transformed span.</typeparam>
    /// <returns>A new Span containing the elements transformed by the predicate.</returns>
    public static Memory<TResult> Select<TSource, TResult>(
#if NET8_0_OR_GREATER
        [NotNull]
#endif
        this Memory<TSource> source,
#if NET8_0_OR_GREATER
        [NotNull]
#endif
        Func<TSource, TResult> predicate)
    {
        TResult[] array = new  TResult[source.Length];
        
        int index = 0;
        
        source.ForEach(x =>
        {
            array[index] = predicate.Invoke(x);
            index++;
        });
        
        return new Memory<TResult>(array);
    }
}