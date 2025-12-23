/*
    EnhancedLinq.Memory
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
*/

using EnhancedLinq.Memory.Immediate;

namespace EnhancedLinq.Memory.Deferred.Enumerators;

internal class MemoryWhereEnumerator<T> : IEnumerator<T>
{
    private readonly Func<T, bool> _predicate;
    private T _current;

    private int _state;
    private readonly IEnumerator<T> _enumerator;

    internal MemoryWhereEnumerator(Memory<T> source, Func<T, bool> predicate)
    {
        _predicate = predicate;
        _enumerator = source.AsEnumerable().GetEnumerator();
        _state = 0;
        _current = source.First();
    }
    
    internal MemoryWhereEnumerator(ReadOnlyMemory<T> source, Func<T, bool> predicate)
    {
        _enumerator = source.AsEnumerable().GetEnumerator();
        _predicate = predicate;
        _state = 0;
        _current = source.First();
    }
    
    public bool MoveNext()
    {
        if (_state == 0)
        {
            while (_enumerator.MoveNext())
            {
                if (_predicate(_enumerator.Current))
                {
                    _current = _enumerator.Current;
                    return true;
                }
            }

            _state = -1;
        }
        
        Dispose();
        return false;
    }

    public void Reset()
    {
        throw new NotSupportedException();
    }

    T IEnumerator<T>.Current => _current;

    object? IEnumerator.Current => _current;

    public void Dispose()
    {
        _enumerator.Dispose();
    }
}