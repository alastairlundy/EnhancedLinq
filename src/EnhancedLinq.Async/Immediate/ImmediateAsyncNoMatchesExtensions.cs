/*
    EnhancedLinq.Async
    Copyright (c) 2025-2026 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
    */

namespace EnhancedLinq.Async.Immediate;

/// <summary>
/// An extension method for asynchronous operations that checks if a specific condition is met within an immediate sequence of elements.
/// </summary>
public static class ImmediateAsyncNoMatchesExtensions
{
    /// <param name="source">The <see cref="IAsyncEnumerable{T}"/> to be searched.</param>
    /// <typeparam name="TSource">The type of elements in the sequence.</typeparam>
    extension<TSource>(IAsyncEnumerable<TSource> source)
    {
        /// <summary>
        /// Determines if none of the elements in the sequence match a predicate condition.
        /// </summary>
        /// <param name="predicate">The predicate to check elements against.</param>
        /// <returns>True if none of the elements matched the predicate, false otherwise.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the source sequence or predicate are null.</exception>
        public async Task<bool> HasNoMatchesAsync(Func<TSource, bool> predicate)
            => await source.CountAtMostAsync(predicate, 0).ConfigureAwait(false);
    }
}