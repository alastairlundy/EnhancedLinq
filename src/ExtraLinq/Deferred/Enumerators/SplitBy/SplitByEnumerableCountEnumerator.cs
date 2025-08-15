/*
    ExtraLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/.
 */

using System;
using System.Collections;
using System.Collections.Generic;

namespace ExtraLinq.Deferred.Enumerators;

internal class SplitByEnumerableCountEnumerator<T> : IEnumerator<IEnumerable<T>>
{
    private readonly IEnumerable<T> _source;
    private readonly int _maxEnumerableCount;
    
    private IEnumerable<T> _current;

    private IEnumerator<T> _enumerator;

    private int _state;
    
    private List<T> _currentEnumerable;
    
    public SplitByEnumerableCountEnumerator(IEnumerable<T> source, int maxEnumerableCount)
    {
        _source = new List<T>(source);
        _maxEnumerableCount = maxEnumerableCount;
        _state = 1;
    }
    
    public bool MoveNext()
    {
        if (_state == 1)
        {
            _enumerator = _source.GetEnumerator();
            _currentEnumerable = new();

            _state = 2;
        }

        if (_state == 2)
        {
            while(_enumerator.MoveNext())
            {
                if (_currentEnumerable.Count <= _maxEnumerableCount)
                {
                    try
                    {
                        _currentEnumerable.Add(_enumerator.Current);
                    }
                    catch
                    {
                        Dispose();
                        throw;
                    }
                }
                else
                {
                    List<T> list = new List<T>();
                    list.AddRange(_currentEnumerable);
                
                    _current = list;
                    _currentEnumerable.Clear();
                    return true;
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

    IEnumerable<T> IEnumerator<IEnumerable<T>>.Current => _current;

    object? IEnumerator.Current => _current;

    public void Dispose()
    {
       _enumerator?.Dispose();
    }
}