/*
    EnhancedLinq.Memory
    Copyright (c) 2025-2026 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
    */

using EnhancedLinq.Memory.Deferred;

namespace EnhancedLinq.Memory.Immediate;

public static partial class EnhancedLinqMemoryImmediate
{
    /// <param name="source">The source <see cref="Span{T}"/>.</param>
    /// <typeparam name="T">The element type in the source <see cref="Span{T}"/>.</typeparam>
    extension<T>(Span<T> source)
    {
        /// <summary>
        /// Determines whether there are at least a specified number of elements in the <see cref="Span{T}"/>.
        /// </summary>
        /// <param name="countToLookFor">The minimum count to look for.</param>
        /// <returns><c>true</c> if there is at least the specified number of elements in the <see cref="Span{T}"/>; otherwise, <c>false</c>.</returns>
        public bool CountAtLeast(int countToLookFor)
        {
            if(countToLookFor != 0)
                InvalidOperationException.ThrowIfSpanIsEmpty(source);
            
            ArgumentOutOfRangeException.ThrowIfNegative(countToLookFor);
            
            return source.Length >= countToLookFor;
        }

        /// <summary>
        /// Determines whether there are at least a specified number of elements in the <see cref="Span{T}"/> that meet a given condition.
        /// </summary>
        /// <param name="predicate">The predicate condition to check elements against.</param>
        /// <param name="countToLookFor">The minimum count to look for.</param>
        /// <returns><c>true</c> if there are at least the specified number of elements that meet the condition; otherwise, <c>false</c>.</returns>
        public bool CountAtLeast(Func<T, bool> predicate,
            int countToLookFor)
        {
            if(countToLookFor != 0)
                InvalidOperationException.ThrowIfSpanIsEmpty(source);
            
            ArgumentOutOfRangeException.ThrowIfNegative(countToLookFor);
            ArgumentNullException.ThrowIfNull(predicate);

            int currentCount = 0;

            foreach (T obj in source)
            {
                if (predicate(obj))
                    currentCount += 1;
            
                if(currentCount >= countToLookFor)
                    return true;
            }

            return false;
        }
    }
    
    /// <param name="source">The source <see cref="ReadOnlySpan{T}"/>.</param>
    /// <typeparam name="T">The element type in the source <see cref="ReadOnlySpan{T}"/>.</typeparam>
    extension<T>(ReadOnlySpan<T> source)
    {
        /// <summary>
        /// Determines whether there are at least a specified number of elements in the <see cref="ReadOnlySpan{T}"/>.
        /// </summary>
        /// <param name="countToLookFor">The minimum count to look for.</param>
        /// <returns><c>true</c> if there is at least the specified number of elements in the <see cref="ReadOnlySpan{T}"/>; otherwise, <c>false</c>.</returns>
        public bool CountAtLeast(int countToLookFor)
        {
            if(countToLookFor != 0)
                InvalidOperationException.ThrowIfSpanIsEmpty(source);
            
            ArgumentOutOfRangeException.ThrowIfNegative(countToLookFor);
            
            return source.Length >= countToLookFor;
        }

        /// <summary>
        /// Determines whether there are at least a specified number of elements in the <see cref="ReadOnlySpan{T}"/> that meet a given condition.
        /// </summary>
        /// <param name="predicate">The predicate condition to check elements against.</param>
        /// <param name="countToLookFor">The minimum count to look for.</param>
        /// <returns><c>true</c> if there are at least the specified number of elements in the <see cref="ReadOnlySpan{T}"/>
        /// that meet the condition; otherwise, <c>false</c>.</returns>
        public bool CountAtLeast(Func<T, bool> predicate,
            int countToLookFor)
        {
            if(countToLookFor != 0)
                InvalidOperationException.ThrowIfSpanIsEmpty(source);
            
            ArgumentOutOfRangeException.ThrowIfNegative(countToLookFor);
            ArgumentNullException.ThrowIfNull(predicate);

            int currentCount = 0;

            foreach (T obj in source)
            {
                if (predicate(obj))
                    currentCount += 1;
            
                if(currentCount >= countToLookFor)
                    return true;
            }

            return false;
        }
    }
    
    /// <param name="source">The source <see cref="Memory{T}"/>.</param>
    /// <typeparam name="T">The element type in the source <see cref="Memory{T}"/>.</typeparam>
    extension<T>(Memory<T> source)
    {
        /// <summary>
        /// Determines whether there are at least a specified number of elements in the <see cref="Memory{T}"/>.
        /// </summary>
        /// <param name="countToLookFor">The minimum count to look for.</param>
        /// <returns><c>true</c> if there is at least the specified number of elements in the <see cref="Memory{T}"/>; otherwise, <c>false</c>.</returns>
        public bool CountAtLeast(int countToLookFor)
        {
            if(countToLookFor != 0)
                InvalidOperationException.ThrowIfMemoryIsEmpty(source);
            
            ArgumentOutOfRangeException.ThrowIfNegative(countToLookFor);
            
            return source.Length >= countToLookFor;
        }

        /// <summary>
        /// Determines whether there are at least a specified number of elements in the <see cref="Memory{T}"/> that meet a given condition.
        /// </summary>
        /// <param name="predicate">The predicate condition to check elements against.</param>
        /// <param name="countToLookFor">The minimum count to look for.</param>
        /// <returns><c>true</c> if there are at least the specified number of elements in the <see cref="Memory{T}"/> that meet the condition; otherwise, <c>false</c>.</returns>
        public bool CountAtLeast(Func<T, bool> predicate,
            int countToLookFor)
        {
            if(countToLookFor != 0)
                InvalidOperationException.ThrowIfMemoryIsEmpty(source);
            
            ArgumentOutOfRangeException.ThrowIfNegative(countToLookFor);
            ArgumentNullException.ThrowIfNull(predicate);

            int currentCount = 0;

            foreach (T obj in source.AsEnumerable())
            {
                if (predicate(obj))
                    currentCount += 1;
            
                if(currentCount >= countToLookFor)
                    return true;
            }

            return false;
        }
    }

    /// <param name="source">The source <see cref="ReadOnlyMemory{T}"/>.</param>
    /// <typeparam name="T">The element type in the source <see cref="ReadOnlyMemory{T}"/>.</typeparam>
    extension<T>(ReadOnlyMemory<T> source)
    {
        /// <summary>
        /// Determines whether there are at least a specified number of elements in the <see cref="ReadOnlyMemory{T}"/>.
        /// </summary>
        /// <param name="countToLookFor">The minimum count to look for.</param>
        /// <returns><c>true</c> if there is at least the specified number of elements in the <see cref="ReadOnlyMemory{T}"/>; otherwise, <c>false</c>.</returns>
        public bool CountAtLeast(int countToLookFor)
        {
            if(countToLookFor != 0)
                InvalidOperationException.ThrowIfMemoryIsEmpty(source);
            
            ArgumentOutOfRangeException.ThrowIfNegative(countToLookFor);
            
            return source.Length >= countToLookFor;
        }

        /// <summary>
        /// Determines whether there are at least a specified number of elements in the <see cref="ReadOnlyMemory{T}"/> that meet a given condition.
        /// </summary>
        /// <param name="predicate">The predicate condition to check elements against.</param>
        /// <param name="countToLookFor">The minimum count to look for.</param>
        /// <returns><c>true</c> if there are at least the specified number of elements in the <see cref="ReadOnlyMemory{T}"/> that meet the condition; otherwise, <c>false</c>.</returns>
        public bool CountAtLeast(Func<T, bool> predicate,
            int countToLookFor)
        {
            if(countToLookFor != 0)
                InvalidOperationException.ThrowIfMemoryIsEmpty(source);
            
            ArgumentOutOfRangeException.ThrowIfNegative(countToLookFor);
            ArgumentNullException.ThrowIfNull(predicate);

            int currentCount = 0;

            foreach (T obj in source.AsEnumerable())
            {
                if (predicate(obj))
                    currentCount += 1;
            
                if(currentCount >= countToLookFor)
                    return true;
            }

            return false;
        }
    }
}