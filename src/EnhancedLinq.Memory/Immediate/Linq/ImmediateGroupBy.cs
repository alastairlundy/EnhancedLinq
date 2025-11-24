/*
    EnhancedLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
    */

using System.Diagnostics.CodeAnalysis;
using System.Linq;
using AlastairLundy.DotPrimitives.Collections.Groupings;

namespace AlastairLundy.EnhancedLinq.Memory.Immediate;

/// <summary>
/// 
/// </summary>
public static partial class EnhancedLinqMemoryImmediate
{
    /// <summary>
    /// Groups the elements of the source span by a specified key predicate function.
    /// </summary>
    /// <param name="source">The source span to group elements from.</param>
    /// <param name="keyPredicate">A function to extract the key for each element.</param>
    /// <typeparam name="TKey">The type of the key returned by the key predicate function.</typeparam>
    /// <typeparam name="TElement">The type of elements in the source span.</typeparam>
    /// <returns>A span of groups, each containing a key and the elements that share that key.</returns>
    public static Span<IGrouping<TKey, TElement>> GroupBy<TKey, TElement>(
#if NET8_0_OR_GREATER
        [NotNull]
#endif
        this Span<TElement> source,
#if NET8_0_OR_GREATER
        [NotNull]
#endif
        Func<TElement, TKey> keyPredicate) where TKey : notnull
    {
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
                dictionary.Add(key, new List<TElement>());
                dictionary[key].Add(item);
            }
        }

        IEnumerable<IGrouping<TKey, TElement>> groups = (from kvp in dictionary
            select new GroupingEnumerable<TKey, TElement>(kvp.Key, kvp.Value));
        
        return new  Span<IGrouping<TKey, TElement>>(groups.ToArray());
    }
}