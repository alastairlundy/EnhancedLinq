/*
    EnhancedLinq.Memory
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
*/

using System.Linq;
using EnhancedLinq.Memory.Deferred.Enumerables;

namespace EnhancedLinq.Memory.Deferred;

public static partial class EnhancedLinqMemoryDeferred
{
    /// <param name="source">The sequence of elements to sort.</param>
    /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
    extension<TSource>(Memory<TSource> source)
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <typeparam name="TKey"></typeparam>
        /// <returns></returns>
        public IOrderedEnumerable<TSource> OrderBy<TKey>(Func<TSource, TKey> predicate)
            => OrderBy(source, predicate, Comparer<TKey>.Default);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="comparer"></param>
        /// <typeparam name="TKey"></typeparam>
        /// <returns></returns>
        public IOrderedEnumerable<TSource> OrderBy<TKey>(Func<TSource, TKey> predicate, IComparer<TKey>? comparer)
        {
            ArgumentNullException.ThrowIfNull(predicate);
            comparer ??= Comparer<TKey>.Default;
            
            return new MemoryOrderByEnumerable<TSource,TKey>(source, predicate, comparer, false);
        }

        /// <summary>
        /// Sorts the elements of a sequence in descending order according to a specified key.
        /// </summary>
        /// <typeparam name="TKey">The type of the key returned by the keySelector function.</typeparam>
        /// <param name="predicate">A function that extracts the key for each element.</param>
        /// <returns>An <see cref="IOrderedEnumerable{TElement}"/> whose elements are sorted in descending order according to the specified key.</returns>
        public IOrderedEnumerable<TSource> OrderByDescending<TKey>(Func<TSource, TKey> predicate)
            => OrderByDescending(source, predicate, Comparer<TKey>.Default);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="comparer"></param>
        /// <typeparam name="TKey"></typeparam>
        /// <returns></returns>
        public IOrderedEnumerable<TSource> OrderByDescending<TKey>(Func<TSource, TKey> predicate,
            IComparer<TKey>? comparer)
        {
            ArgumentNullException.ThrowIfNull(predicate);
            comparer ??= Comparer<TKey>.Default;
            
            return new MemoryOrderByEnumerable<TSource,TKey>(source, predicate, comparer, true);
        }
    }
    
    /// <param name="source">The sequence of elements to sort.</param>
    /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
    extension<TSource>(ReadOnlyMemory<TSource> source)
    {
        /// <summary>
        /// Sorts the elements of a sequence in ascending order according to a specified key.
        /// </summary>
        /// <typeparam name="TKey">The type of the key returned by the keySelector function.</typeparam>
        /// <param name="predicate">A function that extracts the key for each element.</param>
        /// <returns>An <see cref="IOrderedEnumerable{TElement}"/> whose elements are sorted according to the specified key.</returns>
        public IOrderedEnumerable<TSource> OrderBy<TKey>(Func<TSource, TKey> predicate)
            => source.OrderBy(predicate, Comparer<TKey>.Default);

        /// <summary>
        /// Sorts the elements of a sequence in ascending order according to a specified key.
        /// </summary>
        /// <typeparam name="TKey">The type of the key returned by the keySelector function.</typeparam>
        /// <param name="predicate">A function that extracts the key for each element.</param>
        /// <param name="comparer">An optional comparer used to compare keys. If null, the default comparer is used.</param>
        /// <returns>An <see cref="IOrderedEnumerable{TElement}"/> whose elements are sorted according to the specified key.</returns>
        public IOrderedEnumerable<TSource> OrderBy<TKey>(Func<TSource, TKey> predicate,
            IComparer<TKey>? comparer)
        {
            ArgumentNullException.ThrowIfNull(predicate);
            comparer ??= Comparer<TKey>.Default;
            
            return new MemoryOrderByEnumerable<TSource,TKey>(source, predicate,
                comparer, false);
        }

        /// <summary>
        /// Sorts the elements of a sequence in descending order according to a specified key.
        /// </summary>
        /// <typeparam name="TKey">The type of the key returned by the keySelector function.</typeparam>
        /// <param name="predicate">A function that extracts the key for each element.</param>
        /// <returns>An <see cref="IOrderedEnumerable{TElement}"/> whose elements are sorted in descending order according to the specified key.</returns>
        public IOrderedEnumerable<TSource> OrderByDescending<TKey>(Func<TSource, TKey> predicate)
            => source.OrderByDescending(predicate, Comparer<TKey>.Default);

        /// <summary>
        /// Sorts the elements of a sequence in descending order according to a specified key.
        /// </summary>
        /// <typeparam name="TKey">The type of the key returned by the keySelector function.</typeparam>
        /// <param name="predicate">A function that extracts the key for each element.</param>
        /// <param name="comparer">
        /// An optional comparer to be used for comparing keys. If null, the default comparer for type <typeparamref name="TKey"/> is used.
        /// </param>
        /// <returns>An <see cref="IOrderedEnumerable{TSource}"/> whose elements are sorted in descending order according to the specified key.</returns>
        public IOrderedEnumerable<TSource> OrderByDescending<TKey>(Func<TSource, TKey> predicate,
            IComparer<TKey>? comparer)
        {
            ArgumentNullException.ThrowIfNull(predicate);
            comparer ??= Comparer<TKey>.Default;
            
            return new MemoryOrderByEnumerable<TSource,TKey>(source, predicate, comparer, true);
        }
    }
}