/*
    EnhancedLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
    */

using System.Collections;

namespace EnhancedLinq.MsExtensions.Deferred;

internal class SegmentEnumerator :  IEnumerator<char>
{
    private StringSegment _segment;

    private int _state;
    private int _index;

    internal SegmentEnumerator(StringSegment segment)
    {
        _segment = segment;
        _state = 1;
        _index = 0;
    }
    
    public bool MoveNext()
    {
        if (_state == 1)
        {
            if (_index <= _segment.Length - 1)
            {
                Current = _segment[_index];
                ++_index;
                return true;
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

    public char Current { get; private set; }

    object? IEnumerator.Current => Current;

    public void Dispose()
    {
        _segment = StringSegment.Empty;
    }
}