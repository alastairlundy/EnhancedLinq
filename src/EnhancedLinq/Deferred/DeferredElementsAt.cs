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
using System.Linq;

using AlastairLundy.EnhancedLinq.Deferred.Enumerators;
using AlastairLundy.EnhancedLinq.Internals.Localizations;

namespace AlastairLundy.EnhancedLinq.Deferred;

/// <summary>
/// This static partial class contains Deferred Execution extension methods for the <see cref="IEnumerable{T}"/> interface.
/// </summary>
public static partial class EnhancedLinqDeferred
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="source">The <see cref="IEnumerable{T}"/> from which to retrieve elements.</param>
    /// <typeparam name="TSource">The type of the elements in the source and returned <see cref="IEnumerable{T}"/>.</typeparam>
    extension<TSource>(IEnumerable<TSource> source)
    {
        /// <summary>
        /// Returns a sequence of elements from the specified source, 
        /// where the index of each element in the returned sequence corresponds to an index in the provided indices.
        /// </summary>
        /// <remarks>The order of the elements in the returned <see cref="IEnumerable{T}"/> is determined by their original position in the source,
        /// but the order within the returned <see cref="IEnumerable{T}"/> is based on the provided indexes.</remarks>
        /// <param name="indices">A sequence of indices, where each index corresponds to an element in the source.</param>
        /// <returns>A new sequence containing the elements at the specified indexes from the original source.</returns>
        public IEnumerable<TSource> ElementsAt(IEnumerable<int> indices)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(indices);
        
            return new Internals.Infra.CustomEnumeratorEnumerable<TSource>(
                new ElementsAtEnumerator<TSource>(source, indices));
        }
        
#if NET8_0_OR_GREATER || NETSTANDARD2_1
        /// <summary>
        /// Returns a sequence of elements from the specified source, 
        /// where the index of each element in the returned sequence corresponds to an index in the provided indices.
        /// </summary>
        /// <remarks>The order of the elements in the returned <see cref="IEnumerable{T}"/> is determined by their original position in the source,
        /// but the order within the returned <see cref="IEnumerable{T}"/> is based on the provided indexes.</remarks>
        /// <param name="range">The range of indices to retrieve, where each index corresponds to an element in the source.</param>
        /// <returns>A new sequence containing the elements at the specified indexes from the original source.</returns>
        public IEnumerable<TSource> ElementsAt(Range range)
            => ElementsAt(source, range.Start.Value, Math.Abs(range.Start.Value - range.End.Value));
#endif
        
        /// <summary>
        /// Returns a sequence of elements from the specified source, 
        /// where the index of each element in the returned sequence corresponds to an index in the provided indices.
        /// </summary>
        /// <remarks>The order of the elements in the returned <see cref="IEnumerable{T}"/> is determined by their original position in the source,
        /// but the order within the returned <see cref="IEnumerable{T}"/> is based on the provided indexes.</remarks>
        /// <param name="startIndex">The first index to retrieve elements at.</param>
        /// <param name="count">The number of elements from the <see cref="startIndex"/> to retrieve.</param>
        /// <returns>A new <see cref="IEnumerable{T}"/> containing the elements at the specified indexes from the original source.</returns>
        /// <exception cref="IndexOutOfRangeException">Thrown if the starting index is less than 0.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the count is less than or equal to 0.</exception>
        public IEnumerable<TSource> ElementsAt(int startIndex, int count)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(count);

            if(startIndex < 0)
                throw new IndexOutOfRangeException(Resources.Exceptions_IndexOutOfRange
                    .Replace("{x}", startIndex.ToString())
                    .Replace("{y}", "0")
                    .Replace("{z}", source.Count().ToString()));
        
            IEnumerable<int> sequence = startIndex.GenerateNumberRange(count, 1);
       
            return ElementsAt(source, sequence);
        }
    }
}