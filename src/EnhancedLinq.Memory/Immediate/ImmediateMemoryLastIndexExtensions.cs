/*
    EnhancedLinq.Memory
    Copyright (c) 2025-2026 Alastair Lundy
 
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/.
*/

namespace EnhancedLinq.Memory.Immediate;

/// <summary>
/// Provides extension methods for determining the last index of elements in various memory-like collections.
/// </summary>
public static class ImmediateMemoryLastIndexExtensions
{
    /// <param name="span">The span to search.</param>
    /// <typeparam name="T">The type of elements in the span.</typeparam>
    extension<T>(Span<T> span)
    {
        /// <summary>
        /// Retrieves the last index of elements within a <see cref="Span{T}" />.
        /// </summary>
        /// <returns>The index of the last element in the span.</returns>
        /// <exception cref="InvalidOperationException">Thrown if the span is empty.</exception>
        public int LastIndex
        {
            get
            {
                if (span.IsEmpty)
                    throw new InvalidOperationException(Resources.Exceptions_InvalidOperation_EmptySpan);
                return span.Length - 1;
            }
        }
    }

    /// <param name="span">The read-only span to search.</param>
    /// <typeparam name="T">The type of elements in the span.</typeparam>
    extension<T>(ReadOnlySpan<T> span)
    {
        /// <summary>
        /// Retrieves the last index of elements within a <see cref="ReadOnlySpan{T}" />.
        /// </summary>
        /// <returns>The index of the last element in the span.</returns>
        /// <exception cref="InvalidOperationException">Thrown if the span is empty.</exception>
        public int LastIndex
        {
            get
            {
                if (span.IsEmpty)
                    throw new InvalidOperationException(Resources.Exceptions_InvalidOperation_EmptySpan);
                return span.Length - 1;
            }
        }
    }

    /// <param name="memory">The memory to search.</param>
    /// <typeparam name="T">The type of elements in the memory.</typeparam>
    extension<T>(Memory<T> memory)
    {
        /// <summary>
        /// Retrieves the last index of elements within a <see cref="Memory{T}" />.
        /// </summary>
        /// <returns>The index of the last element in the memory.</returns>
        /// <exception cref="InvalidOperationException">Thrown if the memory is empty.</exception>
        public int LastIndex
        {
            get
            {
                if (memory.IsEmpty)
                    throw new InvalidOperationException(Resources.Exceptions_InvalidOperation_EmptyMemory);
                return memory.Length - 1;
            }
        }
    }

    /// <param name="memory">The read-only memory to search.</param>
    /// <typeparam name="T">The type of elements in the memory.</typeparam>
    extension<T>(ReadOnlyMemory<T> memory)
    {
        /// <summary>
        /// Retrieves the last index of elements within a <see cref="ReadOnlyMemory{T}" />.
        /// </summary>
        /// <returns>The index of the last element in the memory.</returns>
        /// <exception cref="InvalidOperationException">Thrown if the memory is empty.</exception>
        public int LastIndex
        {
            get
            {
                if (memory.IsEmpty)
                    throw new InvalidOperationException(Resources.Exceptions_InvalidOperation_EmptyMemory);
                return memory.Length - 1;
            }
        }
    }
}