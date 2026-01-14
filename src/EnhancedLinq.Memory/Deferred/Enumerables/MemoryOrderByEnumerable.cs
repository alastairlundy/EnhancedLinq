/*
    EnhancedLinq.Memory
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
*/

using System.Linq;
using EnhancedLinq.Memory.Deferred.Enumerators;

namespace EnhancedLinq.Memory.Deferred.Enumerables;

internal class MemoryOrderByEnumerable<TSource, TKey1> : IOrderedEnumerable<TSource>
{
    private readonly ReadOnlyMemory<TSource> _memory;
    private readonly Func<TSource, TKey1> _predicate;
    private readonly IComparer<TKey1> _comparer;
    private readonly bool _descending;

    internal MemoryOrderByEnumerable(Memory<TSource> source, Func<TSource, TKey1> predicate,
        IComparer<TKey1> comparer, bool descending)
    {
        _memory = source;
        _predicate = predicate;
        _comparer = comparer;
        _descending = descending;
    }
    internal MemoryOrderByEnumerable(ReadOnlyMemory<TSource> source, Func<TSource, TKey1> predicate,
        IComparer<TKey1> comparer, bool descending)
    {
        _memory = source;
        _predicate = predicate;
        _comparer = comparer;
        _descending = descending;
    }
    
    public IOrderedEnumerable<TSource> CreateOrderedEnumerable<TKey>(Func<TSource, TKey> keySelector,
        IComparer<TKey>? comparer, bool descending)
    {
        comparer ??= Comparer<TKey>.Default;
        
        return new MemoryOrderByEnumerable<TSource, TKey>(_memory,  keySelector, comparer, descending);
    }

    public IEnumerator<TSource> GetEnumerator() => 
        new MemoryOrderByEnumerator<TSource,TKey1>(_memory,  _predicate, _comparer, _descending);

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}