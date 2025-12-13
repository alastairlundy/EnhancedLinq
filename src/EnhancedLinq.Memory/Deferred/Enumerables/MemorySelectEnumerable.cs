/*
    EnhancedLinq.Memory
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
*/

using EnhancedLinq.Memory.Deferred.Enumerators;

namespace EnhancedLinq.Memory.Deferred.Enumerables;

internal class MemorySelectEnumerable<TSource, TResult> : IEnumerable<TResult>
{
    private readonly Func<TSource, TResult> _predicate;
    private readonly ReadOnlyMemory<TSource> _source;

    internal MemorySelectEnumerable(Memory<TSource> source, Func<TSource, TResult> predicate)
    {
        _predicate = predicate;
        _source = source;
    }
    internal MemorySelectEnumerable(ReadOnlyMemory<TSource> source, Func<TSource, TResult> predicate)
    {
        _predicate = predicate;
        _source = source;
    }
    
    public IEnumerator<TResult> GetEnumerator()
    {
        return new MemorySelectEnumerator<TSource, TResult>(_source, _predicate);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}