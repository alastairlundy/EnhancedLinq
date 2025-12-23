/*
    EnhancedLinq.Memory
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
    */

using System.Linq; 
using DotPrimitives.Collections.Groupings;
using EnhancedLinq.Memory.Deferred;

namespace EnhancedLinq.Memory.Immediate;

/// <summary>
/// 
/// </summary>
public static partial class EnhancedLinqMemoryImmediate
{
    /// <param name="source">The source span to group elements from.</param>
    /// <typeparam name="TKey">The type of the key returned by the key predicate function.</typeparam>
    /// <typeparam name="TElement">The type of elements in the source span.</typeparam>
    extension<TKey, TElement>(Span<TElement> source) where TKey : notnull
    {
        /// <summary>
        /// Groups the elements of the source span by a specified key predicate function.
        /// </summary>
        /// <param name="keyPredicate">A function to extract the key for each element.</param>
        /// <returns>A span of groups, each containing a key and the elements that share that key.</returns>
        public Span<IGrouping<TKey, TElement>> GroupBy(Func<TElement, TKey> keyPredicate)
        {
            InvalidOperationException.ThrowIfSpanIsEmpty(source);
            ArgumentNullException.ThrowIfNull(keyPredicate);
        
            Dictionary<TKey, List<TElement>> dictionary = new();

            foreach (TElement item in source)
            {
                TKey key = keyPredicate.Invoke(item);
            
                if (dictionary.ContainsKey(key))
                {
                    dictionary[key].Add(item);
                }
                else
                {
                    dictionary.Add(key, new());
                    dictionary[key].Add(item);
                }
            }

            IEnumerable<IGrouping<TKey, TElement>> groups = (from kvp in dictionary
                select new GroupingEnumerable<TKey, TElement>(kvp.Key, kvp.Value));
        
            return new(groups.ToArray());
        }
    }
    
    /// <param name="source">The source <see cref="ReadOnlySpan{T}"/> to group elements from.</param>
    /// <typeparam name="TKey">The type of the key returned by the key predicate function.</typeparam>
    /// <typeparam name="TElement">The type of elements in the source <see cref="ReadOnlySpan{T}"/>.</typeparam>
    extension<TKey, TElement>(ReadOnlySpan<TElement> source) where TKey : notnull
    {
        /// <summary>
        /// Groups the elements of the source <see cref="ReadOnlySpan{T}"/> by a specified key predicate function.
        /// </summary>
        /// <param name="keyPredicate">A function to extract the key for each element.</param>
        /// <returns>A <see cref="ReadOnlySpan{T}"/> of groups, each containing a key and the elements that share that key.</returns>
        public ReadOnlySpan<IGrouping<TKey, TElement>> GroupBy(Func<TElement, TKey> keyPredicate)
        {
            InvalidOperationException.ThrowIfSpanIsEmpty(source);
            ArgumentNullException.ThrowIfNull(keyPredicate);
        
            Dictionary<TKey, List<TElement>> dictionary = new();

            foreach (TElement item in source)
            {
                TKey key = keyPredicate.Invoke(item);
            
                if (dictionary.ContainsKey(key))
                {
                    dictionary[key].Add(item);
                }
                else
                {
                    dictionary.Add(key, new());
                    dictionary[key].Add(item);
                }
            }

            IEnumerable<IGrouping<TKey, TElement>> groups = (from kvp in dictionary
                select new GroupingEnumerable<TKey, TElement>(kvp.Key, kvp.Value));
        
            return new(groups.ToArray());
        }
    }
    
    /// <param name="source">The source <see cref="Memory{T}"/> to group elements from.</param>
    /// <typeparam name="TKey">The type of the key returned by the key predicate function.</typeparam>
    /// <typeparam name="TElement">The type of elements in the source <see cref="Memory{T}"/>.</typeparam>
    extension<TKey, TElement>(Memory<TElement> source) where TKey : notnull
    {
        /// <summary>
        /// Groups the elements of the source <see cref="Memory{T}"/> by a specified key predicate function.
        /// </summary>
        /// <param name="keyPredicate">A function to extract the key for each element.</param>
        /// <returns>A <see cref="Memory{T}"/> of groups, each containing a key and the elements that share that key.</returns>
        public Memory<IGrouping<TKey, TElement>> GroupBy(Func<TElement, TKey> keyPredicate)
        {
            InvalidOperationException.ThrowIfMemoryIsEmpty(source);
            ArgumentNullException.ThrowIfNull(keyPredicate);
        
            Dictionary<TKey, List<TElement>> dictionary = new();

            foreach (TElement item in source.AsEnumerable())
            {
                TKey key = keyPredicate.Invoke(item);
            
                if (dictionary.ContainsKey(key))
                {
                    dictionary[key].Add(item);
                }
                else
                {
                    dictionary.Add(key, new());
                    dictionary[key].Add(item);
                }
            }

            IEnumerable<IGrouping<TKey, TElement>> groups = (from kvp in dictionary
                select new GroupingEnumerable<TKey, TElement>(kvp.Key, kvp.Value));
        
            return new(groups.ToArray());
        }
    }
}