/*
    EnhancedLinq.Memory
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
*/

using EnhancedLinq.Memory.Deferred.Enumerators;

namespace EnhancedLinq.Memory.Deferred.Enumerables;

internal class MemoryWhereEnumerable<T> : IEnumerable<T>
{
    private readonly Func<T, bool> _predicate;
    private readonly ReadOnlyMemory<T> _memory;
    
    internal MemoryWhereEnumerable(Memory<T> memory, Func<T, bool> predicate)
    {
        _predicate = predicate;
        _memory = memory;
    }

    internal MemoryWhereEnumerable(ReadOnlyMemory<T> memory, Func<T, bool> predicate)
    {
        _predicate = predicate;
        _memory = memory;
    }
    
    public IEnumerator<T> GetEnumerator() => new MemoryWhereEnumerator<T>(_memory, _predicate);

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}