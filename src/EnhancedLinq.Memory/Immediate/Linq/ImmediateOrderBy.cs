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
    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <typeparam name="TSource">The type of the elements of the source Span.</typeparam>
    extension<TSource>(Span<TSource> source)
    {
#if NET8_0_OR_GREATER
        /// <summary>
        /// Sorts the elements of a Span in ascending order by using a specified key selector.
        /// </summary>
        /// <typeparam name="TKey">The type of the key returned by the key selector function.</typeparam>
        /// <param name="predicate">The function to extract the key for each element.</param>
        /// <returns>A new Span containing the sorted elements.</returns>
        public Span<TSource> OrderBy<TKey>(Func<TSource, TKey> predicate)
            => OrderBy(source, predicate, Comparer<TKey>.Default);

        /// <summary>
        /// Sorts the elements of a Span in ascending order by using a specified key selector and optional comparer.
        /// </summary>
        /// <typeparam name="TKey">The type of the key returned by the key selector function.</typeparam>
        /// <param name="predicate">The function to extract the key for each element.</param>
        /// <param name="comparer">An optional comparer to define custom comparison logic. If null, the default comparer is used.</param>
        /// <returns>A new Span containing the sorted elements.</returns>
        public Span<TSource> OrderBy<TKey>(Func<TSource, TKey> predicate, IComparer<TKey>? comparer)
        {
            InvalidOperationException.ThrowIfSpanIsEmpty(source);
            ArgumentNullException.ThrowIfNull(predicate);
            
            comparer ??= Comparer<TKey>.Default;

            Span<(TSource Item, TKey Key)> keyItemPairs = source.Select(s => (s, predicate(s)));
              
            keyItemPairs.Sort((left, right) =>
                comparer.Compare(left.Key, right.Key));

            return keyItemPairs.Select(p => p.Item);
        }
#endif
    }
}