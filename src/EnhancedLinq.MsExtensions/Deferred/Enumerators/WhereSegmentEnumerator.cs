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
using Microsoft.Extensions.Primitives;

namespace EnhancedLinq.MsExtensions.Deferred.Enumerators;

internal class WhereSegmentEnumerator : IEnumerator<char>
{
    private readonly StringSegment _segment;
    private readonly Func<char, bool> _selector;

    private IEnumerator<char> _enumerator;
    
    private char _current;
    
    private int _state;
    
    internal WhereSegmentEnumerator(StringSegment segment, Func<char, bool> selector)
    {
        _segment = segment;
        _selector = selector;
        _state = 1;
    }

    public bool MoveNext()
    {
        if (_state == 1)
        {
            _enumerator = new SegmentEnumerator(_segment);
        }
        if (_state == 2)
        {
            try
            {
                while(_enumerator.MoveNext())
                {
                    if (_selector(_enumerator.Current))
                    {
                        _current = _enumerator.Current;
                        return true;
                    }
                }
            }
            catch
            {
                Dispose();
                throw;
            }

            _state = -1;
        }
        return false;
    }

    public void Reset()
    {
        throw new NotSupportedException();
    }

    public char Current => _current;

    object? IEnumerator.Current => _current;

    public void Dispose()
    {
        throw new NotImplementedException();
    }
}