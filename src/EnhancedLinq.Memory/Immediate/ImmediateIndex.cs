/*
    EnhancedLinq.Memory
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
    */

namespace EnhancedLinq.Memory.Immediate;

public static partial class EnhancedLinqMemoryImmediate
{
    /// <param name="span">The span whose indices will be generated.</param>
    /// <typeparam name="T">The type of the elements in the span.</typeparam>
    extension<T>(Span<T> span)
    {
        /// <summary>
        /// Returns a collection of indices of all elements within the given span, starting from an initial index of zero.
        /// </summary>
        /// <returns>A collection of integers representing the indices of the elements in the span starting from index zero.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the span is empty.</exception>
        public ICollection<int> Index()
            => Index(span, 0);

        /// <summary>
        /// Returns a collection of indices of elements within the given span starting from the specified start index.
        /// </summary>
        /// <param name="startIndex">The starting index from which to generate the indices.</param>
        /// <returns>A collection of integers representing the indices of the elements in the span starting from the provided start index.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the startIndex is negative or exceeds the span length.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the span is empty.</exception>
        public ICollection<int> Index(int startIndex)
        {
            InvalidOperationException.ThrowIfSpanIsEmpty(span);
            ArgumentOutOfRangeException.ThrowIfNegative(startIndex);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(startIndex, span.Length);

            List<int> output = new();
        
            for (int i = 0; i < span.Length; i++)
            {
                if(i >= startIndex)
                    output.Add(i);
            }
        
            return output;
        }
    }
}