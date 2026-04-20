/*
    EnhancedLinq.Async
    Copyright (c) 2025-2026 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
    */

namespace EnhancedLinq.Async.Deferred;

/// <summary>
/// Provides a set of extension methods for performing deferred filtering operations
/// on asynchronous enumerable sequences.
/// </summary>
public static class DeferredAsyncWhereExtensions
{
    extension<T>(IAsyncEnumerable<T> source)
        where T : notnull
    {
        /// <summary>
        /// Filters the elements of an asynchronous sequence based on an asynchronous predicate.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the source sequence.</typeparam>
        /// <param name="selector">
        /// A function that represents the asynchronous predicate to test each element for a condition.
        /// </param>
        /// <returns>
        /// An asynchronous sequence that contains elements from the input sequence that satisfy the condition specified by the predicate.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when the <paramref name="selector"/> argument is null.
        /// </exception>
        public IAsyncEnumerable<T> WhereAsync(Func<T, Task<bool>> selector)
        {
            ArgumentNullException.ThrowIfNull(selector);

            return WhereInternalAsync(selector);

            async IAsyncEnumerable<T> WhereInternalAsync(Func<T, Task<bool>> selectorInternal)
            {
                await foreach (T item in source.ConfigureAwait(false))
                {
                    bool result = await selectorInternal(item).ConfigureAwait(false);

                    if (result)
                        yield return item;
                }
            }
        }

        /// <summary>
        /// Filters the elements of an asynchronous sequence based on an asynchronous predicate.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the source sequence.</typeparam>
        /// <param name="selector">
        /// A function that represents the asynchronous predicate to test each element for a condition.
        /// </param>
        /// <returns>
        /// An asynchronous sequence that contains elements from the input sequence that satisfy the condition specified by the predicate.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when the <paramref name="selector"/> argument is null.
        /// </exception>
        public IAsyncEnumerable<T> WhereAsync(Func<T, ValueTask<bool>> selector)
        {
            ArgumentNullException.ThrowIfNull(selector);

            return WhereInternalAsync(selector);

            async IAsyncEnumerable<T> WhereInternalAsync(Func<T, ValueTask<bool>> selectorInternal)
            {
                await foreach (T item in source.ConfigureAwait(false))
                {
                    bool result = await selectorInternal(item).ConfigureAwait(false);

                    if (result)
                        yield return item;
                }
            }
        }
    }
}