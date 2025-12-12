/*
    EnhancedLinq.Memory
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
    */

// ReSharper disable ConvertClosureToMethodGroup

using System.Linq;

namespace EnhancedLinq.Memory.Immediate.Ranges;

public static partial class EnhancedLinqMemoryImmediateRange
{
    /// <param name="target">The span to remove a range of items from.</param>
    /// <typeparam name="T">The type of elements in the span.</typeparam>
    extension<T>(Span<T> target) where T : IEquatable<T>?
    {
        /// <summary>
        /// Creates a new Span with all items of the original Span minus the items to be removed.
        /// </summary>
        /// <param name="indices">The indices of the items to be removed.</param>
        /// <returns>A new Span with all items of the original Span minus the items to be removed.</returns>
        public Span<T> RemoveRange(IEnumerable<int> indices)
        {
            InvalidOperationException.ThrowIfSpanIsEmpty(target);
            ArgumentNullException.ThrowIfNull(indices);
            
            IEnumerable<int> newIndices = target.Index().Select(i => i.Index)
                .SkipWhile(x => indices.Contains(x));
        
            return target.GetRange(newIndices);
        }

        /// <summary>
        /// Creates a new Span with all items of the original Span minus the items to be removed.
        /// </summary>
        /// <param name="startIndex">The zero-based index to start removing items at.</param>
        /// <param name="count">The number of items to remove from the Span from the start index.</param>
        /// <returns>A new Span with all items of the original Span minus the items to be removed.</returns>
        public Span<T> RemoveRange(int startIndex, int count)
        {
            InvalidOperationException.ThrowIfSpanIsEmpty(target);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(count);
            ArgumentOutOfRangeException.ThrowIfNegative(startIndex);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(count, target.Length);
            
            return RemoveRange(target, Enumerable.Range(startIndex, count));
        }
    }
    
#if NET8_0_OR_GREATER
    /// <param name="target">The span to remove a range of items from.</param>
    /// <typeparam name="T">The type of elements in the span.</typeparam>
    extension<T>(Span<T> target) where T : IEquatable<T>?
    {
        /// <summary>
        /// Creates a new Span with all items of the original Span minus the items to be removed.
        /// </summary>
        /// <param name="range">The index <see cref="Range"/> of items to be removed.</param>
        /// <returns>A new Span with all items of the original Span minus the items to be removed.</returns>
        public Span<T> RemoveRange(Range range) => RemoveRange(target, range.Start.Value, range.End.Value - range.Start.Value);
    }
#endif
}