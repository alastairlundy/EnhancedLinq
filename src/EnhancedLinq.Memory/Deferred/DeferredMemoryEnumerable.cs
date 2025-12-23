/*
    EnhancedLinq.Memory
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
*/

using EnhancedLinq.Memory.Deferred.Enumerables;

namespace EnhancedLinq.Memory.Deferred;

/// <summary>
/// Provides LINQ-style extension methods for working with <see cref="Memory{T}"/>
/// in a deferred execution manner.
/// </summary>
public static partial class EnhancedLinqMemoryDeferred
{
    /// <typeparam name="TSource">The type of the elements in the source memory.</typeparam>
    /// <param name="source">The source <see cref="Memory{T}"/> to convert.</param>
    extension<TSource>(Memory<TSource> source)
    {
        /// <summary>
        /// Converts a <see cref="Memory{T}"/> to an <see cref="IEnumerable{T}"/> that can be enumerated.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{TSource}"/> representation of the elements contained in the provided <see cref="Memory{T}"/>.</returns>
        public IEnumerable<TSource> AsEnumerable() => new MemoryEnumerable<TSource>(source);
    }
    
    /// <typeparam name="TSource">The type of the elements in the source memory.</typeparam>
    /// <param name="source">The source <see cref="ReadOnlyMemory{T}"/> to convert.</param>
    extension<TSource>(ReadOnlyMemory<TSource> source)
    {
        /// <summary>
        /// Converts a <see cref="ReadOnlyMemory{T}"/> to an <see cref="IEnumerable{T}"/> that can be enumerated.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{TSource}"/> that represents the elements of the given <see cref="ReadOnlyMemory{T}"/>.</returns>
        public IEnumerable<TSource> AsEnumerable() => new MemoryEnumerable<TSource>(source);
    }
}