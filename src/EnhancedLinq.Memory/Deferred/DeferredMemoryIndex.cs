/*
    EnhancedLinq.Memory
    Copyright (c) 2025-2026 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
*/

using System.Linq;

namespace EnhancedLinq.Memory.Deferred;

public static partial class EnhancedLinqMemoryDeferred
{
    /// <param name="memory">The <see cref="Memory{T}"/> whose indices will be generated.</param>
    /// <typeparam name="TSource">The type of the elements in the <see cref="Memory{T}"/>.</typeparam>
    extension<TSource>(Memory<TSource> memory)
    {
        /// <summary>
        /// Returns a sequence of indices of all elements within the given <see cref="Memory{T}"/>, starting from an initial index of zero.
        /// </summary>
        /// <returns>A sequence of integers representing the indices of the elements in the <see cref="Memory{T}"/> starting from index zero.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the <see cref="Memory{T}"/> is empty.</exception>
        public IEnumerable<(int Index, TSource Item)> Index()
        { 
            InvalidOperationException.ThrowIfMemoryIsEmpty(memory);
            
            int index = 0;

            return memory.AsEnumerable()
                .Select(x =>
                {
                    int tempIndex = index;
                    index++;

                    return (tempIndex, x);
                });
        }
    }
    
    /// <param name="memory">The <see cref="ReadOnlyMemory{T}"/> whose indices will be generated.</param>
    /// <typeparam name="TSource">The type of the elements in the <see cref="ReadOnlyMemory{T}"/>.</typeparam>
    extension<TSource>(ReadOnlyMemory<TSource> memory)
    {
        /// <summary>
        /// Returns a sequence of indices of all elements within the given <see cref="ReadOnlyMemory{T}"/>, starting from an initial index of zero.
        /// </summary>
        /// <returns>A sequence of integers representing the indices of the elements in the <see cref="ReadOnlyMemory{T}"/> starting from index zero.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the <see cref="ReadOnlyMemory{T}"/> is empty.</exception>
        public IEnumerable<(int Index, TSource Item)> Index()
        { 
            InvalidOperationException.ThrowIfMemoryIsEmpty(memory);
            
            int index = 0;

            return memory.AsEnumerable()
                .Select(x =>
                {
                    int tempIndex = index;
                    index++;

                    return (tempIndex, x);
                });
        }
    }
}