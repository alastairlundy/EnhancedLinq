/*
    EnhancedLinq 
    Copyright (c) 2025-2026 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
    */

#if NET8_0_OR_GREATER
using System.Collections;
using System.Numerics;

namespace EnhancedLinq.Deferred.Enumerators.NumberRanges;

internal class NumberRangeEnumerator<TNumber> : IEnumerator<TNumber> where TNumber : INumber<TNumber>
{
    private readonly TNumber _start;
    private readonly TNumber _count;
    private readonly TNumber _incrementor;

    private TNumber _current;
    private TNumber _emitted;

    private int _state;

    internal NumberRangeEnumerator(TNumber start, TNumber count, TNumber incrementor)
    {
        _start = start;
        _count = count;
        _incrementor = incrementor;
        _current = default!;
        _emitted = TNumber.Zero;
        _state = 1;
    }
    
    public bool MoveNext()
    {
        if (_state != 1)
        {
            return false;
        }

        try
        {
            if (_emitted >= _count)
            {
                Dispose();
                _state = -1;
                return false;
            }

            _current = _emitted == TNumber.Zero ? _start : _current + _incrementor;
            _emitted += TNumber.One;
            return true;
        }
        catch
        {
            Dispose();
            _state = -1;
            throw;
        }
    }

    public void Reset()
    {
        throw new NotSupportedException();
    }

    TNumber IEnumerator<TNumber>.Current => _current;

    object IEnumerator.Current => _current;

    public void Dispose()
    {
    }
}
#endif
