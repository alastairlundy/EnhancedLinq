/*
    EnhancedLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
    */


using System.Linq;
using AlastairLundy.DotExtensions.Numbers;

namespace AlastairLundy.EnhancedLinq.Memory.Immediate.Ranges;

/// <summary>
/// 
/// </summary>
public static partial class EnhancedLinqMemoryImmediateRange
{
#if NET8_0_OR_GREATER

    /// <param name="target">The original span to extract the range of items from.</param>
    /// <typeparam name="T">The type of elements in the span.</typeparam>
    extension<T>(Span<T> target)
    {
        /// <summary>
        /// Returns a new Span with the specified range of elements,
        /// starting from the given start index and ending at the given end index.
        /// </summary>
        /// <param name="range">The <see cref="Range"/> containing the start and end indices.</param>
        /// <returns>A new span containing the specified range of elements.</returns>
        public Span<T> GetRange(Range range)
            => GetRange(target, range.Start.Value, range.End.Value);
    }
#endif

    /// <param name="target">The original span to extract the range of items from.</param>
    /// <typeparam name="T">The type of elements in the span.</typeparam>
    extension<T>(Span<T> target)
    {
        /// <summary>
        /// Returns a new Span with the specified range of elements,
        /// starting from the given start index and ending at the given end index.
        /// </summary>
        /// <param name="start">The zero-based starting index of the range.</param>
        /// <param name="end">The one-based ending index of the range (inclusive).</param>
        /// <returns>A new span containing the specified range of elements.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the start or end indices are out of range for the span.</exception>
        /// <exception cref="IndexOutOfRangeException">Thrown if the start index is greater than the length of the span, or if the end index exceeds the span's capacity.</exception>
        public Span<T> GetRange(int start, int end)
        {
            InvalidOperationException.ThrowIfSpanIsEmpty(target);

            ArgumentOutOfRangeException.ThrowIfNegative(start);
            ArgumentOutOfRangeException.ThrowIfNegative(end);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(start, target.Length);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(end, target.Length);
            
            if ((end - start) > target.Length)
                throw new ArgumentOutOfRangeException(
                    Resources.Exceptions_SkipCount_TooLarge);
        
            return target.Slice(start, end - start);
        }
        
        /// <summary>
        /// Retrieves a range of elements within the specified span.
        /// </summary>
        /// <remarks>This method is more computationally expensive than the <see cref="ICollection{T}"/> overload for this method.
        /// Please use that overload instead if using a Collection.
        /// </remarks>
        /// <param name="indices">A sequence of indices specifying the positions of interest in the span.</param>
        /// <returns>A new Span containing only the elements at the specified indices.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the span is empty.</exception>
        /// <exception cref="IndexOutOfRangeException">Thrown if any index in indices is out of range for the target span.</exception>
        public Span<T> GetRange(IEnumerable<int> indices)
        {
            InvalidOperationException.ThrowIfSpanIsEmpty(target);
            ArgumentNullException.ThrowIfNull(indices);

            if(indices is ICollection<int> collection)
                return GetRange(target, collection);
        
            List<T> output = new();
        
            foreach (int index in indices)
            {
                ArgumentOutOfRangeException.ThrowIfNegative(index);
                ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index, target.Length);
                
                output.Add(target[index]);
            }
            
            return new Span<T>(output.ToArray());
        }
    }
        
    /// <summary>
    /// Retrieves a range of elements within the specified span.
    /// </summary>
    /// <param name="target">The span to search.</param>
    /// <param name="indices">A collection of indices specifying the positions of interest in the span.</param>
    /// <typeparam name="T">The type of the elements within the span.</typeparam>
    /// <returns>A new Span containing only the elements at the specified indices.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the span is empty.</exception>
    /// <exception cref="IndexOutOfRangeException">Thrown if any index in indices is out of range for the target span.</exception>
    public static Span<T> GetRange<T>(this Span<T> target, ICollection<int> indices)
    {
        InvalidOperationException.ThrowIfSpanIsEmpty(target);

        ArgumentNullException.ThrowIfNull(indices);
            
        if(indices.IsIncrementedNumberRange(1))
            return GetRange(target, indices.Min(), indices.Max());
        
        T[] array = new T[indices.Count];
        
        int newIndex = 0;
        
        foreach (int index in indices)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(index);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index, target.Length);

            target[newIndex] = target[index];
            newIndex++;
        }
            
        return new Span<T>(array);
    }
}