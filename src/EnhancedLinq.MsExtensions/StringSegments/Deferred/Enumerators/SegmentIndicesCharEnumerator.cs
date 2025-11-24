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

namespace AlastairLundy.EnhancedLinq.MsExtensions.StringSegments.Deferred.Enumerators;

internal class SegmentIndicesCharEnumerator : IEnumerator<int>
{
    private readonly StringSegment _segment;
    private readonly char _c;

    private int _state;
    private int _index;
    
    public SegmentIndicesCharEnumerator(StringSegment segment, char c)
    {
        _segment = segment;
        _c = c;
        _index = 0;
        _state = 1;
    }

    public bool MoveNext()
    {
        if (_state == 1)
        {
            while (_index < _segment.Length)
            {
                if (_segment[_index] == _c)
                {
                    Current = _index;
                    return true;
                }

                _index++;
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

    public int Current { get; private set; }

    object? IEnumerator.Current => Current;

    public void Dispose()
    {
      
    }
}