/*
    EnhancedLinq.Async
    Copyright (c) 2025-2026 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/.
*/

using EnhancedLinq.Async.Deferred.Enumerators;

namespace EnhancedLinq.Async.Deferred;

/// <summary>
/// 
/// </summary>
public static class DeferredAsyncElementsAtExtensions
{
    /// <param name="source">The <see cref="IAsyncEnumerable{T}"/> from which to retrieve elements.</param>
    /// <typeparam name="TSource">The type of the elements in the source and returned <see cref="IAsyncEnumerable{T}"/>.</typeparam>
    extension<TSource>(IAsyncEnumerable<TSource> source)
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="indices"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public IAsyncEnumerable<TSource> ElementsAt(IAsyncEnumerable<int> indices)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(indices);
        
            return new CustomAsyncEnumerable<TSource>(
                new AsyncElementsAtEnumerator<TSource>(source, indices));
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="range"></param>
        /// <returns></returns>
        public IAsyncEnumerable<TSource> ElementsAt(Range range)
            =>source.ElementsAt(range.Start.Value, Math.Abs(range.Start.Value - range.End.Value));
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public IAsyncEnumerable<TSource> ElementsAt(int startIndex, int count)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(count);
        
            IAsyncEnumerable<int> sequence = startIndex.GenerateNumberRange(count, 1);
       
            return source.ElementsAt(sequence);
        }
    }
}