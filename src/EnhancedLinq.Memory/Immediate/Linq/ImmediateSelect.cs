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
    /// Transforms elements of a <see cref="ReadOnlySpan{T}"/> according to the defined predicate.
    /// </summary>
    /// <param name="source">The <see cref="ReadOnlySpan{T}"/> to transform.</param>
    /// <param name="predicate">The function to apply to each element of the <see cref="ReadOnlySpan{T}"/>.</param>
    /// <typeparam name="TSource">The type of elements in the source <see cref="ReadOnlySpan{T}"/>.</typeparam>
    /// <typeparam name="TResult">The type of elements in the resulting <see cref="ReadOnlySpan{T}"/> after transformation.</typeparam>
    /// <returns>A new <see cref="ReadOnlySpan{T}"/> with the elements transformed by the predicate.</returns>
    public static ReadOnlySpan<TResult> Select<TSource, TResult>(
#if NET8_0_OR_GREATER
        [NotNull]
#endif
        this ReadOnlySpan<TSource> source,
#if NET8_0_OR_GREATER
        [NotNull]
#endif
        Func<TSource, TResult> predicate)
    {
        TResult[] array = new  TResult[source.Length];
        
        int index = 0;

        foreach (TSource item in source)
        {
            array[index] = predicate.Invoke(item);
            index++;
        }

        return new Span<TResult>(array);
    }

    /// <summary>
    /// Transforms elements of a <see cref="Memory{T}"/> into a new <see cref="Memory{T}"/> using the defined predicate.
    /// </summary>
    /// <param name="source">The source <see cref="Memory{T}"/> containing elements to be transformed.</param>
    /// <param name="predicate">The transformation function to apply to each element in the <see cref="Memory{T}"/>.</param>
    /// <typeparam name="TSource">The type of elements in the source <see cref="Memory{T}"/>.</typeparam>
    /// <typeparam name="TResult">The type of elements in the transformed <see cref="Memory{T}"/>.</typeparam>
    /// <returns>A new <see cref="Memory{T}"/> containing the elements transformed by the predicate.</returns>
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

    /// <summary>
    /// Transforms elements of a <see cref="ReadOnlyMemory{T}"/> according using the defined predicate.
    /// </summary>
    /// <param name="source">The <see cref="ReadOnlyMemory{T}"/> containing the elements to be transformed.</param>
    /// <param name="predicate">The transformation function to apply to each element in the <see cref="ReadOnlyMemory{T}"/></param>
    /// <typeparam name="TSource">The type of the elements in the source <see cref="ReadOnlyMemory{T}"/>.</typeparam>
    /// <typeparam name="TResult">The type of the elements in the resulting <see cref="ReadOnlyMemory{T}"/>.</typeparam>
    /// <returns>A new <see cref="ReadOnlyMemory{T}"/> containing the transformed elements.</returns>
    public static ReadOnlyMemory<TResult> Select<TSource, TResult>(
#if NET8_0_OR_GREATER
        [NotNull]
#endif
        this ReadOnlyMemory<TSource> source,
#if NET8_0_OR_GREATER
        [NotNull]
#endif
        Func<TSource, TResult> predicate)
    {
        TResult[] array = new TResult[source.Length];

        int index = 0;

        foreach (TSource item in source.Span)
        {
            array[index] = predicate.Invoke(item);
            index++;
        }
        
        return new Memory<TResult>(array);
    }
}