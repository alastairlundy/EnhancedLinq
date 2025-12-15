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
    /// <param name="target">The Span to be searched.</param>
    /// <typeparam name="T">The type of items stored in the span.</typeparam>
    extension<T>(Span<T> target)
    {
        /// <summary>
        /// Returns a new Span with all items in the Span that match the predicate condition.
        /// </summary>
        /// <param name="predicate">The predicate func to be invoked on each item in the Span.</param>
        /// <returns>A new Span with the items that match the predicate condition.</returns>
        public Span<T> Where(Func<T, bool> predicate)
        {
            ArgumentNullException.ThrowIfNull(predicate);
            InvalidOperationException.ThrowIfSpanIsEmpty(target);
            
            List<T> list;

            if (target.Length <= 100)
                list = new(capacity: target.Length);
            else
                list = new();
        
            foreach (T item in target)
            {
                if (predicate.Invoke(item))
                {
                    list.Add(item);
                }
            }
        
            return new(list.ToArray());
        }
    }
    
    /// <param name="target">The <see cref="ReadOnlySpan{T}"/> to be searched.</param>
    /// <typeparam name="T">The type of items stored in the <see cref="ReadOnlySpan{T}"/></typeparam>
    extension<T>(ReadOnlySpan<T> target)
    {
        /// <summary>
        /// Returns a new <see cref="ReadOnlySpan{T}"/> with all items in the <see cref="ReadOnlySpan{T}"/> that match the predicate condition.
        /// </summary>
        /// <param name="predicate">The predicate func to be invoked on each item in the <see cref="ReadOnlySpan{T}"/>.</param>
        /// <returns>A new <see cref="ReadOnlySpan{T}"/> with the items that match the predicate condition.</returns>
        public ReadOnlySpan<T> Where(Func<T, bool> predicate)
        {
            ArgumentNullException.ThrowIfNull(predicate);
            InvalidOperationException.ThrowIfSpanIsEmpty(target);

            List<T> list;

            if (target.Length <= 100)
                list = new(capacity: target.Length);
            else
                list = new();
        
            foreach (T item in target)
            {
                if (predicate.Invoke(item))
                {
                    list.Add(item);
                }
            }
        
            return new(list.ToArray());
        }
    }
}