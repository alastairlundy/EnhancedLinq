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
using System.Numerics;

namespace EnhancedLinq.Deferred.Enumerators.NumberRanges;

internal class NumberRangeEnumerator<TNumber> : IEnumerator<TNumber> where TNumber : INumber<TNumber>
{
    private readonly IEnumerable<TNumber> _source;   
    private IEnumerator<TNumber> _enumerator;
    
    private TNumber _current;

    private int _state;

    internal NumberRangeEnumerator(IEnumerable<TNumber> source)
    {
        _source = source;
        _current = TNumber.Zero;
        _state = 0;
    }
    
    public bool MoveNext()
    {
        if (_state == 1)
        {
            _enumerator = _source.GetEnumerator();
            _state = 2;
        }

        if (_state == 2)
        {
            try
            {
                while(_enumerator.MoveNext())
                {
                    _current = _enumerator.Current;
                    return true;
                }
            }
            catch
            {
                Dispose();
                throw;
            }
        }

        _state = -1;
        Dispose();
        return false;
    }

    public void Reset()
    {
        throw new NotSupportedException();
    }

    TNumber IEnumerator<TNumber>.Current => _current;

    object? IEnumerator.Current => _current;

    public void Dispose()
    {
        _enumerator.Dispose();
    }
}