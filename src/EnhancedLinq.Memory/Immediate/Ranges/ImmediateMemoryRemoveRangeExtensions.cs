/*
    EnhancedLinq.Memory
    Copyright (c) 2025-2026 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/.
    */

// ReSharper disable ConvertClosureToMethodGroup

using System.Linq;

namespace EnhancedLinq.Memory.Immediate.Ranges;

/// <summary>
/// </summary>
public static class ImmediateMemoryRemoveRangeExtensions
{
    /// <param name="target">The span to remove a range of items from.</param>
    /// <typeparam name="T">The type of elements in the span.</typeparam>
    extension<T>(Span<T> target) where T : IEquatable<T>?
    {
        /// <summary>
        ///     Creates a new Span with all items of the original Span minus the items to be removed.
        /// </summary>
        /// <param name="indices">The indices of the items to be removed.</param>
        /// <returns>A new Span with all items of the original Span minus the items to be removed.</returns>
        public Span<T> RemoveRange(IEnumerable<int> indices)
        {
            ArgumentNullException.ThrowIfNull(indices);

            int targetLength = target.Length;
            HashSet<int> toRemove = new(indices.Where(i => i >= 0 && i < targetLength));

            T[] buffer = new T[targetLength - toRemove.Count > 0 ? targetLength - toRemove.Count : 0];
            int newIndex = 0;

            for (int i = 0; i < target.Length; i++)
            {
                if (!toRemove.Contains(i))
                {
                    buffer[newIndex] = target[i];
                    newIndex++;
                }
            }

            if (newIndex != buffer.Length)
                Array.Resize(ref buffer, newIndex);

            return new Span<T>(buffer);
        }

        /// <summary>
        ///     Creates a new Span with all items of the original Span minus the items to be removed.
        /// </summary>
        /// <param name="startIndex">The zero-based index to start removing items at.</param>
        /// <param name="count">The number of items to remove from the Span from the start index.</param>
        /// <returns>A new Span with all items of the original Span minus the items to be removed.</returns>
        public Span<T> RemoveRange(int startIndex, int count)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(startIndex);
            ArgumentOutOfRangeException.ThrowIfNegative(count);

            if (startIndex + count > target.Length)
                throw new ArgumentOutOfRangeException(nameof(count),
                    Resources.Exceptions_SkipCount_TooLarge);

            return target.RemoveRange(Enumerable.Range(startIndex, count));
        }
    }

#if NET8_0_OR_GREATER
    /// <param name="target">The span to remove a range of items from.</param>
    /// <typeparam name="T">The type of elements in the span.</typeparam>
    extension<T>(Span<T> target) where T : IEquatable<T>?
    {
        /// <summary>
        ///     Creates a new Span with all items of the original Span minus the items to be removed.
        /// </summary>
        /// <param name="range">The index <see cref="Range" /> of items to be removed.</param>
        /// <returns>A new Span with all items of the original Span minus the items to be removed.</returns>
        public Span<T> RemoveRange(Range range)
        {
            (int offset, int length) = range.GetOffsetAndLength(target.Length);
            return target.RemoveRange(offset, length);
        }
    }
#endif
}