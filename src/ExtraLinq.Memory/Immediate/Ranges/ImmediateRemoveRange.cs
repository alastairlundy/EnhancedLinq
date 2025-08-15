/*
    ExtraLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/.
 */

namespace ExtraLinq.Memory.Immediate.Ranges;

public static partial class ExtraLinqMemoryImmediateRange
{
    /// <summary>
    /// Creates a new Span with all items of the original Span minus the items to be removed.
    /// </summary>
    /// <param name="target">The span to remove a range of items from.</param>
    /// <param name="indices">The indices of the items to be removed.</param>
    /// <typeparam name="T">The type of elements in the span.</typeparam>
    /// <returns>A new Span with all items of the original Span minus the items to be removed.</returns>
    public static Span<T> RemoveRange<T>(this Span<T> target, ICollection<int> indices) where T : IEquatable<T>?
    {
        T[] elements = target.GetRange(indices)
            .ToArray();

        return (from item in target
            where elements.Contains(item) == false
            select item);
    }
    
    /// <summary>
    /// Creates a new Span with all items of the original Span minus the items to be removed.
    /// </summary>
    /// <param name="target">The span to remove a range of items from.</param>
    /// <param name="range">The index <see cref="Range"/> of items to be removed.</param>
    /// <typeparam name="T">The type of elements in the span.</typeparam>
    /// <returns>A new Span with all items of the original Span minus the items to be removed.</returns>
    public static Span<T> RemoveRange<T>(this Span<T> target, Range range)
        => RemoveRange(target, range.Start.Value, range.End.Value);

    /// <summary>
    /// Creates a new Span with all items of the original Span minus the items to be removed.
    /// </summary>
    /// <param name="target">The span to remove a range of items from.</param>
    /// <param name="startIndex">The zero-based index to start removing items at.</param>
    /// <param name="count">The number of items to remove from the Span from the start index.</param>
    /// <typeparam name="T">The type of elements in the span.</typeparam>
    /// <returns>A new Span with all items of the original Span minus the items to be removed.</returns>
    public static Span<T> RemoveRange<T>(this Span<T> target, int startIndex, int count)
    {
        if (target.IsEmpty)
            throw new ArgumentException();
        
        if (startIndex < 0 || startIndex > target.Length)
            throw new IndexOutOfRangeException();
        
        if(count < 0 || count > target.Length)
            throw new ArgumentOutOfRangeException(nameof(count));
        
        T[] output = new T[target.Length - (startIndex + count)];
        int index = 0;

        for (int i = 0; i < target.Length; i++)
        {
            if (i < startIndex && i > startIndex + count)
            {
                output[index] = target[i];
                index++;
            }
        }
        
        return new Span<T>(output);
    }
}