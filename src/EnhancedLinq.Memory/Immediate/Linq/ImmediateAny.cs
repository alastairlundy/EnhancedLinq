/*
    EnhancedLinq.Memory
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
    */

using System.Linq;
using EnhancedLinq.Memory.Deferred;

namespace EnhancedLinq.Memory.Immediate;

public static partial class EnhancedLinqMemoryImmediate
{
    /// <param name="target">The Span to be searched.</param>
    /// <typeparam name="T">The type of items stored in the span.</typeparam>
    extension<T>(Span<T> target)
    {
        /// <summary>
        /// Returns whether there are any items in the span.
        /// </summary>
        /// <returns></returns>
        public bool Any() => target.Length > 0;

        /// <summary>
        /// Returns whether any item in a Span matches the predicate condition.
        /// </summary>
        /// <param name="predicate">The predicate func to be invoked on each item in the Span.</param>
        /// <returns>True if any item in the span matches the predicate; false otherwise.</returns>
        public bool Any(Func<T, bool> predicate)
        {
            InvalidOperationException.ThrowIfSpanIsEmpty(target);
            ArgumentNullException.ThrowIfNull(predicate);
            
            Span<bool> groups = (from c in target
                group c by predicate.Invoke(c)
                into g
                where g.Key
                select g.Any());

            bool? result = groups.FirstOrDefault();

            return result ?? false;
        }
    }
    
    /// <param name="target">The <see cref="ReadOnlySpan{T}"/> to be searched.</param>
    /// <typeparam name="T">The type of items stored in the <see cref="ReadOnlySpan{T}"/>.</typeparam>
    extension<T>(ReadOnlySpan<T> target)
    {
        /// <summary>
        /// Returns whether there are any items in the <see cref="ReadOnlySpan{T}"/>.
        /// </summary>
        /// <returns></returns>
        public bool Any() => target.Length > 0;

        /// <summary>
        /// Returns whether any item in a <see cref="ReadOnlySpan{T}"/> matches the predicate condition.
        /// </summary>
        /// <param name="predicate">The predicate func to be invoked on each item in the <see cref="ReadOnlySpan{T}"/>.</param>
        /// <returns>True if any item in the <see cref="ReadOnlySpan{T}"/> matches the predicate; false otherwise.</returns>
        public bool Any(Func<T, bool> predicate)
        {
            InvalidOperationException.ThrowIfSpanIsEmpty(target);
            ArgumentNullException.ThrowIfNull(predicate);
            
            ReadOnlySpan<bool> groups = (from c in target
                group c by predicate.Invoke(c)
                into g
                where g.Key
                select g.Any());

            bool? result = groups.FirstOrDefault();

            return result ?? false;
        }
    }

    /// <param name="target">The <see cref="Memory{T}"/> to be searched.</param>
    /// <typeparam name="T">The type of items stored in the <see cref="Memory{T}"/>.</typeparam>
    extension<T>(Memory<T> target)
    {
        /// <summary>
        /// Returns whether there are any items in the <see cref="Memory{T}"/>.
        /// </summary>
        /// <returns></returns>
        public bool Any() => target.Length > 0;

        /// <summary>
        /// Returns whether any item in a <see cref="Memory{T}"/> matches the predicate condition.
        /// </summary>
        /// <param name="predicate">The predicate func to be invoked on each item in the <see cref="Memory{T}"/>.</param>
        /// <returns>True if any item in the <see cref="Memory{T}"/> matches the predicate; false otherwise.</returns>
        public bool Any(Func<T, bool> predicate)
        {
            InvalidOperationException.ThrowIfMemoryIsEmpty(target);
            ArgumentNullException.ThrowIfNull(predicate);
            
            IEnumerable<bool> groups = (from c in target
                group c by predicate.Invoke(c)
                into g
                where g.Key
                select g.Any());

            bool? result = groups.FirstOrDefault();

            return result ?? false;
        }
    }
    
    /// <param name="target">The <see cref="ReadOnlyMemory{T}"/> to be searched.</param>
    /// <typeparam name="T">The type of items stored in the <see cref="ReadOnlyMemory{T}"/>.</typeparam>
    extension<T>(ReadOnlyMemory<T> target)
    {
        /// <summary>
        /// Returns whether there are any items in the <see cref="ReadOnlyMemory{T}"/>.
        /// </summary>
        /// <returns></returns>
        public bool Any() => target.Length > 0;

        /// <summary>
        /// Returns whether any item in a <see cref="ReadOnlyMemory{T}"/> matches the predicate condition.
        /// </summary>
        /// <param name="predicate">The predicate func to be invoked on each item in the <see cref="ReadOnlyMemory{T}"/>.</param>
        /// <returns>True if any item in the <see cref="ReadOnlyMemory{T}"/> matches the predicate; false otherwise.</returns>
        public bool Any(Func<T, bool> predicate)
        {
            InvalidOperationException.ThrowIfMemoryIsEmpty(target);
            ArgumentNullException.ThrowIfNull(predicate);
            
            IEnumerable<bool> groups = (from c in target
                group c by predicate.Invoke(c)
                into g
                where g.Key
                select g.Any());

            bool? result = groups.FirstOrDefault();

            return result ?? false;
        }
    }
}