/*
    EnhancedLinq 
    Copyright (c) 2025-2026 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
    */

using System.Collections;

namespace EnhancedLinq.Deferred.Enumerators.Indices;

internal class IndicesEnumerator<T> : IEnumerator<int>
{
    private readonly Func<T, bool> _predicate;

    private readonly IEnumerator<T> _enumerator;

    private int _index;
    private int _current;

    private int _state;

    public IndicesEnumerator(IEnumerable<T> source, Func<T, bool> predicate)
    {
        _predicate = predicate;
        
        _index = 0;
        _state = 1;
        _enumerator = source.GetEnumerator();
    }

    public bool MoveNext()
    {
        if (_state != 1)
        {
            return false;
        }

        try
        {
            while (_enumerator.MoveNext())
            {
                int currentIndex = _index;
                _index++;

                if (_predicate(_enumerator.Current))
                {
                    _current = currentIndex;
                    return true;
                }
            }
        }
        catch
        {
            Dispose();
            _state = -1;
            throw;
        }

        Dispose();
        _state = -1;
        return false;
    }

    public void Reset()
    {   
        throw new NotSupportedException();
    }

    int IEnumerator<int>.Current => _current;

    object IEnumerator.Current => _current;

    public void Dispose()
    {
        _enumerator.Dispose();
    }
}