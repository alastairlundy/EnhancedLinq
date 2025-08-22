/*
    EnhancedLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/.
 */

using AlastairLundy.DotExtensions.Numbers;
using EnhancedLinq.Memory.Internals.Localizations;

namespace EnhancedLinq.Memory.Immediate.Ranges;

public static partial class EnhancedLinqMemoryImmediateRange
{
    
    /// <summary>
    /// Returns a new Span with the specified range of elements,
    /// starting from the given start index and ending at the given end index.
    /// </summary>
    /// <param name="target">The original span to extract the range of items from.</param>
    /// <param name="range">The <see cref="Range"/> containing the start and end indices.</param>
    /// <typeparam name="T">The type of elements in the span.</typeparam>
    /// <returns>A new span containing the specified range of elements.</returns>
    public static Span<T> GetRange<T>(this Span<T> target, Range range)
        => GetRange(target, range.Start.Value, range.End.Value);
    
    /// <summary>
    /// Returns a new Span with the specified range of elements,
    /// starting from the given start index and ending at the given end index.
    /// </summary>
    /// <param name="target">The original span to extract the range of items from.</param>
    /// <param name="start">The zero-based starting index of the range.</param>
    /// <param name="end">The one-based ending index of the range (inclusive).</param>
    /// <typeparam name="T">The type of elements in the span.</typeparam>
    /// <returns>A new span containing the specified range of elements.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the start or end indices are out of range for the span.</exception>
    /// <exception cref="IndexOutOfRangeException">Thrown if the start index is greater than the length of the span, or if the end index exceeds the span's capacity.</exception>
    public static Span<T> GetRange<T>(this Span<T> target, int start, int end)
    {
        if ((end - start) > target.Length)
            throw new ArgumentOutOfRangeException(
                Resources.Exceptions_SkipCount_TooLarge);

        if (start < 0 || start >= target.Length)
        {
            throw new IndexOutOfRangeException(Resources.Exceptions_IndexOutOfRange
                .Replace("{x}", $"{start}")
                .Replace("{y}", $"0")
                .Replace("{z}", $"{target.Length}"));
        }
        
        return target.Slice(start, end - start);
    }

        
    /// <summary>
    /// Retrieves a range of elements within the specified span.
    /// </summary>
    /// <param name="target">The initial span to search.</param>
    /// <param name="indices">A collection of indices specifying the positions of interest in the span.</param>
    /// <typeparam name="T">The type of the elements within the span.</typeparam>
    /// <returns>A new Span containing only the elements at the specified indices.</returns>
    /// <exception cref="IndexOutOfRangeException">Thrown if any index in indices is out of range for the target span.</exception>
    public static Span<T> GetRange<T>(this Span<T> target, ICollection<int> indices)
    {
        if (target == Span<T>.Empty)
            throw new ArgumentException();
        
        ArgumentNullException.ThrowIfNull(indices);
        
        if(indices.IsIncrementedNumberRange(1))
            return GetRange(target, indices.Min(), indices.Max());
        
        T[] array = new T[indices.Count];
        
        int newIndex = 0;
        
        foreach (int index in indices)
        {
            if (index < 0 || index >= target.Length)
            {
                throw new IndexOutOfRangeException(Resources.Exceptions_IndexOutOfRange
                    .Replace("{x}", $"{index}")
                    .Replace("{y}", $"0")
                    .Replace("{z}", $"{target.Length}"));
            }
                
            target[newIndex] = target[index];
            newIndex++;
        }
            
        return new Span<T>(array);
    }
}