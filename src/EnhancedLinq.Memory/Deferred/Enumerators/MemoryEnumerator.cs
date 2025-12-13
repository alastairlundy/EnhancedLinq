/*
    EnhancedLinq.Memory
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
*/

using EnhancedLinq.Memory.Immediate;

namespace EnhancedLinq.Memory.Deferred.Enumerators;

internal class MemoryEnumerator<TSource> : IEnumerator<TSource>
{
    private readonly ReadOnlyMemory<TSource> _memory;

    private int _state;

    private int _currentIndex;
    
    internal MemoryEnumerator(Memory<TSource> memory)
    {
        _memory = memory;
        _state = 0;
        _currentIndex = 0;
        
        if(_memory.Length > 0)
            Current = _memory.First();
    }
    
    internal MemoryEnumerator(ReadOnlyMemory<TSource> memory)
    {
        _memory = memory;
        _state = 0;
        _currentIndex = 0;
        
        if(_memory.Length > 0)
            Current = _memory.First();
    }
    
    public bool MoveNext()
    {
        if (_state == 0)
        {
            bool moveNext = _currentIndex <= _memory.Length - 1 && _memory.Length > 0;

            if (moveNext)
            {
                Current = _memory.Span[_currentIndex];
                _currentIndex++;

                return true;
            }
            
            _state = -1;
        }

        Dispose();
        return false;
    }

    public void Reset()
    {
        _state = 0;
        _currentIndex = 0;
    }

    public TSource Current
    {
        get => field ?? throw new ArgumentNullException(nameof(_memory));
        private set
        {
            if (value is not null)
            {
                field = value;
            }
        }
    }

    object? IEnumerator.Current => Current;

    public void Dispose()
    {
        _state = -1;
    }
}