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

namespace EnhancedLinq.Deferred.Enumerators.Ranges;

internal class InsertRangeEnumerator<T> : IEnumerator<T>
{
    private readonly int _indexToInsertAt;

    private readonly IEnumerator<T> _sourceEnumerator;
    private readonly IEnumerator<T> _toBeInsertedEnumerator;

    private int _state;
    private int _index;

    internal InsertRangeEnumerator(IEnumerable<T> source, int indexToInsertAt, IEnumerable<T> toBeInserted)
    {
        _indexToInsertAt = indexToInsertAt;
        _state = 1;
        _sourceEnumerator = source.GetEnumerator();
        _toBeInsertedEnumerator = toBeInserted.GetEnumerator();
        Current = _toBeInsertedEnumerator.Current;
    }
    
    public bool MoveNext()
    {
        if (_state == 1)
        {
            if (_indexToInsertAt == 0)
            {
                while (_toBeInsertedEnumerator.MoveNext())
                {
                    Current = _toBeInsertedEnumerator.Current;
                    _index++;
                    return true;
                }

                while (_sourceEnumerator.MoveNext())
                {
                    Current = _sourceEnumerator.Current;
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
                            Current = _toBeInsertedEnumerator.Current;
                            _index++;
                            return true;
                        }
                    }
                    else
                    {
                        Current = _sourceEnumerator.Current;
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

    public T Current { get; private set; }

    object? IEnumerator.Current => Current;

    public void Dispose()
    {
        _sourceEnumerator.Dispose();
        _toBeInsertedEnumerator.Dispose();
    }
}