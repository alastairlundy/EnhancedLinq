/*
    EnhancedLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
    */

namespace EnhancedLinq.Memory.Immediate;

public static partial class EnhancedLinqMemoryImmediate
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="source">The source span from which to retrieve distinct elements.</param>
    /// <typeparam name="TSource">The type of the elements in the source span.</typeparam>
    extension<TSource>(Span<TSource> source)
    {
        /// <summary>
        /// Returns a new span containing only the distinct elements from the source span, determined by a specified key selector.
        /// </summary>
        /// <typeparam name="TKey">The type of the key extracted from each element.</typeparam>
        /// <param name="keySelector">A function to extract a key from an element.</param>
        /// <returns>A span that contains only the distinct elements from the source span, determined by the key selector.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the source span is empty.</exception>
        /// <exception cref="ArgumentNullException">Thrown when the key selector is null.</exception>
        public Span<TSource> DistinctBy<TKey>(Func<TSource, TKey> keySelector)
            => DistinctBy(source, keySelector, EqualityComparer<TKey>.Default);

        /// <summary>
        /// Returns a new span containing only the distinct elements from the source span, determined by a specified key selector.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source span.</typeparam>
        /// <typeparam name="TKey">The type of the key extracted from each element.</typeparam>
        /// <param name="keySelector">A function to extract a key from an element.</param>
        /// <param name="comparer">An equality comparer to compare keys. If null, the default equality comparer for the key type is used.</param>
        /// <returns>A span that contains only the distinct elements from the source span, determined by the key selector.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the source span is empty.</exception>
        /// <exception cref="ArgumentNullException">Thrown when the key selector is null.</exception>
        public Span<TSource> DistinctBy<TKey>(Func<TSource, TKey> keySelector, IEqualityComparer<TKey>? comparer)
        {
            comparer ??= EqualityComparer<TKey>.Default;

            InvalidOperationException.ThrowIfSpanIsEmpty(source);
            ArgumentNullException.ThrowIfNull(keySelector);
            
            List<TSource> allowedItems = new();
            HashSet<TKey> allowedKeys = new(comparer: comparer);

            foreach (TSource element in source)
            {
                TKey key = keySelector(element);

                if (allowedKeys.Add(key))
                {
                    allowedItems.Add(element);
                }
            }

            return allowedItems.ToArray();
        }
    }

    
    extension<TSource>(ReadOnlySpan<TSource> source)
    {
        /// <summary>
        /// Returns a new <see cref="ReadOnlySpan{T}"/> containing only the distinct elements from the source <see cref="ReadOnlySpan{T}"/>, determined by a specified key selector.
        /// </summary>
        /// <typeparam name="TKey">The type of the key extracted from each element.</typeparam>
        /// <param name="keySelector">A function to extract a key from an element.</param>
        /// <returns>A <see cref="ReadOnlySpan{T}"/> that contains only the distinct elements from the source <see cref="ReadOnlySpan{T}"/>, determined by the key selector.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the source <see cref="ReadOnlySpan{T}"/> is empty.</exception>
        /// <exception cref="ArgumentNullException">Thrown when the key selector is null.</exception>
        public ReadOnlySpan<TSource> DistinctBy<TKey>(Func<TSource, TKey> keySelector)
            => DistinctBy(source, keySelector, EqualityComparer<TKey>.Default);

        /// <summary>
        /// Returns a new <see cref="ReadOnlySpan{T}"/> containing only the distinct elements from the source <see cref="ReadOnlySpan{T}"/>, determined by a specified key selector and an optional equality comparer.
        /// </summary>
        /// <typeparam name="TKey">The type of the key extracted from each element.</typeparam>
        /// <param name="keySelector">A function to extract a key from an element of the source span.</param>
        /// <param name="comparer">An optional equality comparer to compare keys. If null, the default comparer is used.</param>
        /// <returns>A <see cref="ReadOnlySpan{T}"/> that contains only the distinct elements from the source <see cref="ReadOnlySpan{T}"/>, determined by the key selector and equality comparer.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the source <see cref="ReadOnlySpan{T}"/> is empty.</exception>
        /// <exception cref="ArgumentNullException">Thrown when the key selector is null.</exception>
        public ReadOnlySpan<TSource> DistinctBy<TKey>(Func<TSource, TKey> keySelector,
            IEqualityComparer<TKey>? comparer)
        {
            comparer ??= EqualityComparer<TKey>.Default;

            InvalidOperationException.ThrowIfSpanIsEmpty(source);
            ArgumentNullException.ThrowIfNull(keySelector);
            
            List<TSource> allowedItems = new();
            HashSet<TKey> allowedKeys = new(comparer: comparer);

            foreach (TSource element in source)
            {
                TKey key = keySelector(element);

                if (allowedKeys.Add(key))
                {
                    allowedItems.Add(element);
                }
            }

            return allowedItems.ToArray();
        }
    }

    extension<TSource>(Memory<TSource> source)
    {
        /// <summary>
        /// Returns a new memory segment containing only the distinct elements from the source memory, determined by a specified key selector.
        /// </summary>
        /// <typeparam name="TKey">The type of the key extracted from each element.</typeparam>
        /// <param name="keySelector">A function to extract a key from an element.</param>
        /// <returns>A memory segment that contains only the distinct elements from the source memory, determined by the key selector.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the source memory is empty.</exception>
        /// <exception cref="ArgumentNullException">Thrown when the key selector is null.</exception>
        public Memory<TSource> DistinctBy<TKey>(Func<TSource, TKey> keySelector)
            => DistinctBy(source, keySelector, EqualityComparer<TKey>.Default);

        /// <summary>
        /// Returns a new memory segment containing only the distinct elements from the source memory, determined by a specified key selector and an optional equality comparer.
        /// </summary>
        /// <typeparam name="TKey">The type of the key extracted from each element.</typeparam>
        /// <param name="keySelector">A function to extract a key from an element.</param>
        /// <param name="comparer">An optional comparer to determine the equality of keys. If null, the default equality comparer for the key type is used.</param>
        /// <returns>A memory segment containing only the distinct elements from the source memory, determined by the key selector and comparer.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the source memory is empty.</exception>
        /// <exception cref="ArgumentNullException">Thrown when the key selector is null.</exception>
        public Memory<TSource> DistinctBy<TKey>(Func<TSource, TKey> keySelector,
            IEqualityComparer<TKey>? comparer)
        {
            comparer ??= EqualityComparer<TKey>.Default;

            InvalidOperationException.ThrowIfMemoryIsEmpty(source);
            ArgumentNullException.ThrowIfNull(keySelector);
            
            List<TSource> allowedItems = new();
            HashSet<TKey> allowedKeys = new(comparer: comparer);

            foreach (TSource element in source.Span)
            {
                TKey key = keySelector(element);

                if (allowedKeys.Add(key))
                {
                    allowedItems.Add(element);
                }
            }

            return allowedItems.ToArray();
        }
    }

    extension<TSource>(ReadOnlyMemory<TSource> source)
    {
        /// <summary>
        /// Returns a new <see cref="ReadOnlyMemory{T}"/> block containing only the distinct elements from the source <see cref="ReadOnlyMemory{T}"/> block, determined by a specified key selector.
        /// </summary>
        /// <typeparam name="TKey">The type of the key extracted from each element.</typeparam>
        /// <param name="keySelector">A function to extract a key from an element.</param>
        /// <returns>A <see cref="ReadOnlyMemory{T}"/> block that contains only the distinct elements from the source <see cref="ReadOnlyMemory{T}"/> block, determined by the key selector.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the source <see cref="ReadOnlyMemory{T}"/> block is empty.</exception>
        /// <exception cref="ArgumentNullException">Thrown when the key selector is null.</exception>
        public ReadOnlyMemory<TSource> DistinctBy<TKey>(Func<TSource, TKey> keySelector)
            => DistinctBy(source, keySelector, EqualityComparer<TKey>.Default);

        /// <summary>
        /// Returns a new <see cref="ReadOnlyMemory{T}"/> containing only the distinct elements from the source <see cref="ReadOnlyMemory{T}"/>, determined by a specified key selector and optional equality comparer.
        /// </summary>
        /// <typeparam name="TKey">The type of the key extracted from each element.</typeparam>
        /// <param name="keySelector">A function to extract a key from an element of the source <see cref="ReadOnlyMemory{T}"/>.</param>
        /// <param name="comparer">
        /// An optional equality comparer to compare keys. If null, the default equality comparer for the type <typeparamref name="TKey"/> is used.
        /// </param>
        /// <returns>A <see cref="ReadOnlyMemory{T}"/> that contains only the distinct elements from the source <see cref="ReadOnlyMemory{T}"/>, determined by the key selector.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the source <see cref="ReadOnlyMemory{T}"/> is empty.</exception>
        /// <exception cref="ArgumentNullException">Thrown when the key selector is null.</exception>
        public ReadOnlyMemory<TSource> DistinctBy<TKey>(Func<TSource, TKey> keySelector,
            IEqualityComparer<TKey>? comparer)
        {
            comparer ??= EqualityComparer<TKey>.Default;

            InvalidOperationException.ThrowIfMemoryIsEmpty(source);
            ArgumentNullException.ThrowIfNull(keySelector);
            
            List<TSource> allowedItems = new();
            HashSet<TKey> allowedKeys = new(comparer: comparer);

            foreach (TSource element in source.Span)
            {
                TKey key = keySelector(element);

                if (allowedKeys.Add(key))
                {
                    allowedItems.Add(element);
                }
            }

            return allowedItems.ToArray();
        }
    }
}