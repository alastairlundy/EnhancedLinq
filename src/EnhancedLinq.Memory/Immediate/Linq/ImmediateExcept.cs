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
    /// <param name="first">The first Span to search.</param>
    /// <typeparam name="T">The type of items stored in the span.</typeparam>
    extension<T>(Span<T> first) where T : IEquatable<T>
    {
        /// <summary>
        /// Returns a new Span with all the elements of two Spans that are only in one Span and not the other.
        /// </summary>
        /// <param name="second">The second Span to search.</param>
        /// <returns>A new Span with all the elements of Span One and Span Two that were not in the other Span.</returns>
        public Span<T> Except(Span<T> second)
        {
            InvalidOperationException.ThrowIfSpanIsEmpty(first);
            InvalidOperationException.ThrowIfSpanIsEmpty(second);
            
            T[] output = new  T[first.Length + second.Length];
            int index = 0;

            foreach (T item in first)
            {
                if (!second.Contains(item))
                {
                    output[index] = item;
                    index++;
                }
            }

            foreach (T item in second)
            {
                if(!first.Contains(item))
                {
                    output[index] = item;
                    index++;
                }
            }
        
            Array.Resize(ref output, index);

            return new(output);
        }
    }
    
    /// <param name="first">The first <see cref="ReadOnlySpan{T}"/> to search.</param>
    /// <typeparam name="T">The type of items stored in the <see cref="ReadOnlySpan{T}"/>.</typeparam>
    extension<T>(ReadOnlySpan<T> first) where T : IEquatable<T>
    {
        /// <summary>
        /// Returns a new <see cref="ReadOnlySpan{T}"/> with all the elements of two <see cref="ReadOnlySpan{T}"/> objects that are only in one <see cref="ReadOnlySpan{T}"/> and not the other.
        /// </summary>
        /// <param name="second">The second <see cref="ReadOnlySpan{T}"/> to search.</param>
        /// <returns>A new <see cref="ReadOnlySpan{T}"/> with all the elements of <see cref="ReadOnlySpan{T}"/> One and <see cref="ReadOnlySpan{T}"/> Two that were not in the other <see cref="ReadOnlySpan{T}"/>.</returns>
        public ReadOnlySpan<T> Except(ReadOnlySpan<T> second)
        {
            InvalidOperationException.ThrowIfSpanIsEmpty(first);
            InvalidOperationException.ThrowIfSpanIsEmpty(second);
            
            T[] output = new  T[first.Length + second.Length];
            int index = 0;

            foreach (T item in first)
            {
                if (!second.Contains(item))
                {
                    output[index] = item;
                    index++;
                }
            }

            foreach (T item in second)
            {
                if(!first.Contains(item))
                {
                    output[index] = item;
                    index++;
                }
            }
        
            Array.Resize(ref output, index);

            return new Span<T>(output);
        }
    }
}