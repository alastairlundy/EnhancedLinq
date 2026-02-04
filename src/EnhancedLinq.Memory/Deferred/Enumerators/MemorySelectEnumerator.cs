/*
    EnhancedLinq.Memory
    Copyright (c) 2025-2026 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
*/
#nullable disable
namespace EnhancedLinq.Memory.Deferred.Enumerators;

internal class MemorySelectEnumerator<TSource, TResult> : IEnumerator<TResult>
{
    private readonly Func<TSource, TResult> _predicate;
    private readonly IEnumerator<TSource> _enumerator;

    private int _state;
    
    internal MemorySelectEnumerator(Memory<TSource> source, Func<TSource, TResult> predicate)
    {
        _predicate = predicate;
        _enumerator = new MemoryEnumerator<TSource>(source);
        _state = 0;
    }

    internal MemorySelectEnumerator(ReadOnlyMemory<TSource> source, Func<TSource, TResult> predicate)
    {
        _predicate = predicate;
        _enumerator = new MemoryEnumerator<TSource>(source);
        _state = 0;
    }
    
    public bool MoveNext()
    {
        if (_state == 0)
        {
            bool moveNext =  _enumerator.MoveNext();
            if (moveNext)
            {
                Current = _predicate(_enumerator.Current);
                return moveNext;
            }
            
            _state = -1;
        }
        
        Dispose();
        return false;
    }

    public void Reset()
    {
        try
        {
            _enumerator.Reset();
        }
        catch 
        {
            throw new NotSupportedException();
        }
    }
    
    public TResult Current { get; private set; }

    object IEnumerator.Current => Current;

    public void Dispose()
    {
        _enumerator.Dispose();
    }
}