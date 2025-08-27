/*
    EnhancedLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/.
 */

using AlastairLundy.EnhancedLinq.Memory.Internals.Localizations;

// ReSharper disable ConvertClosureToMethodGroup

namespace AlastairLundy.EnhancedLinq.Memory.Immediate.Ranges;

public static partial class EnhancedLinqMemoryImmediateRange
{
    
    /// <summary>
    /// Creates a new Span with all items of the original Span minus the items to be removed.
    /// </summary>
    /// <param name="target">The span to remove a range of items from.</param>
    /// <param name="indices">The indices of the items to be removed.</param>
    /// <typeparam name="T">The type of elements in the span.</typeparam>
    /// <returns>A new Span with all items of the original Span minus the items to be removed.</returns>
    public static Span<T> RemoveRange<T>(this Span<T> target, IEnumerable<int> indices) where T : IEquatable<T>?
    {
        if (target.IsEmpty)
            throw new ArgumentException(Resources.Exceptions_InvalidOperation_EmptySpan);
        
        IEnumerable<int> newIndices = target.Index().SkipWhile(x => indices.Contains(x));
        
        return target.GetRange(newIndices);
    }
    
    /// <summary>
    /// Creates a new Span with all items of the original Span minus the items to be removed.
    /// </summary>
    /// <param name="target">The span to remove a range of items from.</param>
    /// <param name="range">The index <see cref="Range"/> of items to be removed.</param>
    /// <typeparam name="T">The type of elements in the span.</typeparam>
    /// <returns>A new Span with all items of the original Span minus the items to be removed.</returns>
    public static Span<T> RemoveRange<T>(this Span<T> target, Range range) where T : IEquatable<T>? 
        => RemoveRange(target, range.Start.Value, range.End.Value - range.Start.Value);

    /// <summary>
    /// Creates a new Span with all items of the original Span minus the items to be removed.
    /// </summary>
    /// <param name="target">The span to remove a range of items from.</param>
    /// <param name="startIndex">The zero-based index to start removing items at.</param>
    /// <param name="count">The number of items to remove from the Span from the start index.</param>
    /// <typeparam name="T">The type of elements in the span.</typeparam>
    /// <returns>A new Span with all items of the original Span minus the items to be removed.</returns>
    public static Span<T> RemoveRange<T>(this Span<T> target, int startIndex, int count) where T : IEquatable<T>?
    {
        if (target.IsEmpty)
            throw new ArgumentException(Resources.Exceptions_InvalidOperation_EmptySpan);
        
        if (startIndex < 0 || startIndex > target.Length)
            throw new IndexOutOfRangeException();
        
        if(count < 0 || count > target.Length)
            throw new ArgumentOutOfRangeException(nameof(count));

        return RemoveRange(target, Enumerable.Range(startIndex, count));
    }
}