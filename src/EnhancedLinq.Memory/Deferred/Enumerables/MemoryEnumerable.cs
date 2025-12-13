/*
    EnhancedLinq.Memory
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
*/

using EnhancedLinq.Memory.Deferred.Enumerators;

namespace EnhancedLinq.Memory.Deferred.Enumerables;

internal class MemoryEnumerable<TSource> : IEnumerable<TSource>
{
    private readonly ReadOnlyMemory<TSource> _memory;
    
    internal MemoryEnumerable(Memory<TSource> source)
    {
        _memory = source;
    }

    internal MemoryEnumerable(ReadOnlyMemory<TSource> source)
    {
        _memory = source;
    }
    
    public IEnumerator<TSource> GetEnumerator()
        => new MemoryEnumerator<TSource>(_memory);

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}