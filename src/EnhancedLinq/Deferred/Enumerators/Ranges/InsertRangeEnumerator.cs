/*
    EnhancedLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/.
 */

using System;
using System.Collections;
using System.Collections.Generic;

namespace AlastairLundy.EnhancedLinq.Deferred.Enumerators.Ranges;
    private readonly IEnumerable<T> _toBeInserted;
    
    private IEnumerator<T> _sourceEnumerator;
    private IEnumerator<T> _toBeInsertedEnumerator;
    
    private T _current;
    
    private int _state;
    private int _index;

    internal InsertRangeEnumerator(IEnumerable<T> source, int indexToInsertAt, IEnumerable<T> toBeInserted)
    {
        _source = source;
        _indexToInsertAt = indexToInsertAt;
        _toBeInserted = toBeInserted;
        _state = 1;
    }
    
    public bool MoveNext()
    {
        if (_state == 1)
        {
            _index = 0;

            _sourceEnumerator = _source.GetEnumerator();
            _toBeInsertedEnumerator = _toBeInserted.GetEnumerator();

            _state = 2;
        }
        if (_state == 2)
        {
            if (_indexToInsertAt == 0)
            {
                while (_toBeInsertedEnumerator.MoveNext())
                {
                    _current = _toBeInsertedEnumerator.Current;
                    _index++;
                    return true;
                }

                while (_sourceEnumerator.MoveNext())
                {
                    _current = _sourceEnumerator.Current;
                    _index++;
                    return true;
                }
            }
            else
            {
                while (_sourceEnumerator.MoveNext())
                {
                    if (_index == _indexToInsertAt)
                    {
                        while (_toBeInsertedEnumerator.MoveNext())
                        {
                            _current = _toBeInsertedEnumerator.Current;
                            _index++;
                            return true;
                        }
                    }
                    else
                    {
                        _current = _sourceEnumerator.Current;
                        _index++;
                        return true;
                    }
                }
            }
        }

        Dispose();
        _state = -1;
        return false;
    }

    public void Reset()
    {
        throw new NotSupportedException();
    }

    public T Current => _current;

    object? IEnumerator.Current => Current;

    public void Dispose()
    {
        _sourceEnumerator?.Dispose();
        _toBeInsertedEnumerator?.Dispose();
    }
}