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

namespace ExtraLinq.Deferred.Enumerators.Indices;

internal class StringIndicesEnumerator : IEnumerator<int>
{
    private readonly string _str;
    private readonly string _segment;
    
    private readonly IEnumerable<int> _indices;
    
    private IEnumerator<int> _indicesEnumerator;
    
    private int _index;
    private int _current;

    private int _state;

    internal StringIndicesEnumerator(string str, string segment)
    {
        _str = str;
        _segment = segment;
        _index = 0;

        _indices = str.IndicesOf(segment[0]);
    }

    public bool MoveNext()
    {
        if (_state == 1)
        {
            _indicesEnumerator = _indices.GetEnumerator();
            _state = 2;
        }

        if (_state == 2)
        {
            try
            {
                while(_indicesEnumerator.MoveNext())
                {
                    string compare = _str.Substring(_indicesEnumerator.Current,
                        _segment.Length);
                    
                    if (_segment.Equals(compare))
                    {
                        _current = _index;
                        return true;
                    }
                    _index++;
                }
            }
            catch
            {
                Dispose();
                throw;
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

    int IEnumerator<int>.Current => _current;

    object? IEnumerator.Current => _current;

    public void Dispose()
    {
      _indicesEnumerator.Dispose();
    }
}