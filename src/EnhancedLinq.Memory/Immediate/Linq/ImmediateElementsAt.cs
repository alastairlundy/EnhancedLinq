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
    /// <param name="source">The source <see cref="Memory{T}"/> .</param>
    /// <typeparam name="T">The type of items stored in the Memory.</typeparam>
    extension<T>(Memory<T> source)
    {
        /// <summary>
        /// Returns the element at the specified index in the source <see cref="Memory{T}"/>.
        /// </summary>
        /// <param name="index">The zero-based index of the element to be retrieved.</param>
        /// <returns>A new source <see cref="Memory{T}"/> containing a single element starting at the specified index in the Memory.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the source <see cref="Memory{T}"/> has no elements or the index is out of range.</exception>
        public T ElementAt(int index)
        {
            InvalidOperationException.ThrowIfMemoryIsEmpty(source);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(source.Length);
            ArgumentOutOfRangeException.ThrowIfNegative(index);

            Memory<T> items = source.ElementsAt(index, 1);

            return items.First();
        }

        /// <summary>
        /// Returns the element at the specified index in the source <see cref="Memory{T}"/>,
        /// or the default value of type <typeparamref name="T"/> if the index is out of range.
        /// </summary>
        /// <param name="index">The zero-based index of the element to be retrieved.</param>
        /// <returns>The element at the specified index in the source <see cref="Memory{T}"/> if found;
        /// otherwise, the default value of type <typeparamref name="T"/>.</returns>
        public T? ElementAtOrDefault(int index)
        {
            if (source.Length == 0 || index < 0)
                return default;

            try
            {
                Memory<T> items = source.ElementsAt(index, 1);

                return items.FirstOrDefault();
            }
            catch(ArgumentOutOfRangeException)
            {
                return default;
            }
        }
        
        /// <summary>
        /// Returns a new <see cref="Memory{T}"/> containing the specified number of elements starting at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the element to be retrieved.</param>
        /// <param name="count">The number of elements to include in the returned Memory.</param>
        /// <returns>A new <see cref="Memory{T}"/> containing the specified number of elements starting at the specified index.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the source <see cref="Memory{T}"/> has no elements or the index is out of range.</exception>
        public Memory<T> ElementsAt(int index, int count)
        {
            InvalidOperationException.ThrowIfMemoryIsEmpty(source);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(source.Length);
            ArgumentOutOfRangeException.ThrowIfNegative(index);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(count);

#if NET8_0_OR_GREATER
            return source[new(index, index + count)];
#else
            return source.Slice(index, index + count);
#endif
        }
    }
    
    /// <param name="source">The source <see cref="ReadOnlyMemory{T}"/> .</param>
    /// <typeparam name="T">The type of items stored in the Memory.</typeparam>
    extension<T>(ReadOnlyMemory<T> source)
    {
        /// <summary>
        /// Returns the element at the specified index in the source <see cref="ReadOnlyMemory{T}"/>.
        /// </summary>
        /// <param name="index">The zero-based index of the element to be retrieved.</param>
        /// <returns>A new source <see cref="ReadOnlyMemory{T}"/> containing a single element starting at the specified index in the Memory.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the source <see cref="ReadOnlyMemory{T}"/> has no elements or the index is out of range.</exception>
        public T ElementAt(int index)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(source.Length);
            ArgumentOutOfRangeException.ThrowIfNegative(index);
            
            return source.ElementsAt(index, 1)
                .First();
        }

        /// <summary>
        /// Returns the element at the specified index in the source <see cref="ReadOnlyMemory{T}"/>,
        /// or the default value of type <typeparamref name="T"/> if the index is out of range.
        /// </summary>
        /// <param name="index">The zero-based index of the element to be retrieved.</param>
        /// <returns>The element at the specified index in the source <see cref="ReadOnlyMemory{T}"/> if found;
        /// otherwise, the default value of type <typeparamref name="T"/>.</returns>
        public T? ElementAtOrDefault(int index)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(index);
            if (source.Length == 0)
                return default;

            try
            {
                return source.ElementsAt(index, 1)
                    .FirstOrDefault();
            }
            catch(ArgumentOutOfRangeException)
            {
                return default;
            }
        }
        
        /// <summary>
        /// Returns a new <see cref="ReadOnlyMemory{T}"/> containing the specified number of elements starting at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the element to be retrieved.</param>
        /// <param name="count">The number of elements to include in the returned Memory.</param>
        /// <returns>A new <see cref="ReadOnlyMemory{T}"/> containing the specified number of elements starting at the specified index.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the source <see cref="ReadOnlyMemory{T}"/> has no elements or the index is out of range.</exception>
        public ReadOnlyMemory<T> ElementsAt(int index, int count)
        {
            InvalidOperationException.ThrowIfMemoryIsEmpty(source);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(source.Length);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(index);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(count);

#if NET8_0_OR_GREATER
            return source[new(index, index + count)];
#else
            return source.Slice(index, index + count);
#endif
        }
    }

    /// <summary>
    /// Provides extension methods for working with <see cref="Span{T}"/> in an immediate context.
    /// </summary>
    /// <typeparam name="T">The type of items stored in the <see cref="Span{T}"/>.</typeparam>
    /// <param name="source">The source <see cref="Span{T}"/>.</param>
    extension<T>(Span<T> source)
    {
        /// <summary>
        /// Returns the element at the specified index in the source <see cref="Span{T}"/> or the default value of type <typeparamref name="T"/> if the index is out of range.
        /// </summary>
        /// <param name="index">The zero-based index of the element to retrieve.</param>
        /// <returns>The element at the specified index in the source <see cref="Span{T}"/>, or the default value of type <typeparamref name="T"/> if the index is out of range.</returns>
        public T? ElementAtOrDefault(int index)
        {
            try
            {
                InvalidOperationException.ThrowIfSpanIsEmpty(source);
                ArgumentOutOfRangeException.ThrowIfNegativeOrZero(index);
                
                return source[index];
            }
            catch (ArgumentOutOfRangeException)
            {
                return default;
            }
            catch (IndexOutOfRangeException)
            {
                return default;
            }
        }

        /// <summary>
        /// Returns a contiguous segment of elements from the source <see cref="Span{T}"/> starting at the specified index and containing the specified count of elements.
        /// </summary>
        /// <param name="index">The zero-based starting index of the segment to retrieve.</param>
        /// <param name="count">The number of elements to include in the segment.</param>
        /// <returns>A new <see cref="Span{T}"/> containing the specified range of elements from the source.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if the source <see cref="Span{T}"/> is empty, the starting index is negative, the count is negative,
        /// or the range specified by the starting index and count is out of bounds.
        /// </exception>
        public Span<T> ElementsAt(int index, int count)
        {
            InvalidOperationException.ThrowIfSpanIsEmpty(source);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(source.Length);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(index);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(count);

#if NET8_0_OR_GREATER
            return source[new Range(index, index + count)];
#else
            return source.Slice(index, index + count);
#endif
        }
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <typeparam name="T"></typeparam>
    extension<T>(ReadOnlySpan<T> source)
    {
        /// <summary>
        /// Returns the element at the specified index from the source <see cref="ReadOnlySpan{T}"/>, or the default value for the type if the index is out of range.
        /// </summary>
        /// <param name="index">The zero-based index of the element to retrieve.</param>
        /// <returns>The element at the specified index if it exists; otherwise, the default value for the type.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the specified index is negative or zero.</exception>
        /// <exception cref="IndexOutOfRangeException">Thrown if the specified index is out of bounds of the source span.</exception>
        public T? ElementAtOrDefault(int index)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(index);

            try
            {
                return source[index];
            }
            catch
            {
                return default;
            }
        }

        /// <summary>
        /// Retrieves a continuous range of elements starting at a specified index and spanning a specified count from the source <see cref="ReadOnlySpan{T}"/>.
        /// </summary>
        /// <param name="index">The zero-based starting index of the range to retrieve.</param>
        /// <param name="count">The number of elements in the range to retrieve.</param>
        /// <returns>A new <see cref="ReadOnlySpan{T}"/> containing the specified range of elements starting at the given index.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if the source <see cref="ReadOnlySpan{T}"/> is empty, or if the <paramref name="index"/> or <paramref name="count"/> values are less than or equal to zero.
        /// </exception>
        public ReadOnlySpan<T> ElementsAt(int index, int count)
        {
            InvalidOperationException.ThrowIfSpanIsEmpty(source);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(source.Length);
            ArgumentOutOfRangeException.ThrowIfNegative(index);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(count);

#if NET8_0_OR_GREATER
            return source[new Range(index, index + count)];
#else
            return source.Slice(index, index + count);
#endif
        }
    }
}