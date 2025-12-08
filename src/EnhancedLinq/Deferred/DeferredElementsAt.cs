/*
    EnhancedLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
    */

using EnhancedLinq.Deferred.Enumerators;

namespace EnhancedLinq.Deferred;

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
        /// <param name="count">The number of elements from the start index to retrieve.</param>
        /// <returns>A new <see cref="IEnumerable{T}"/> containing the elements at the specified indexes from the original source.</returns>
        /// <exception cref="IndexOutOfRangeException">Thrown if the starting index is less than 0.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the count is less than or equal to 0.</exception>
        public IEnumerable<TSource> ElementsAt(int startIndex, int count)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(count);
        
            IEnumerable<int> sequence = startIndex.GenerateNumberRange(count, 1);
       
            return ElementsAt(source, sequence);
        }
    }
}