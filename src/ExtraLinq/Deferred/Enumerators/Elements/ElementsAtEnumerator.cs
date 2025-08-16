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

using ExtraLinq.Immediate;

namespace ExtraLinq.Deferred.Enumerators;

internal class ElementsAtEnumerator<TSource> : IEnumerator<TSource>
{
    private IEnumerator<int> _indicesEnumerator;
    
    private readonly IEnumerable<TSource> _source;
    private readonly IEnumerable<int> _indices;

    private int _state;
    
    private TSource _current;
    
    internal ElementsAtEnumerator(IEnumerable<TSource> source, IEnumerable<int> indices)
    {
        _source = source;
        _indices = indices;
        _state = 1;
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
                if(_indicesEnumerator.MoveNext())
                {
                    _current = _source.ElementAt(_indicesEnumerator.Current);
                    return true;
                }

                return false;
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

    public TSource Current => _current;

    object? IEnumerator.Current => _current;

    public void Dispose()
    {
        _indicesEnumerator?.Dispose();
    }
}