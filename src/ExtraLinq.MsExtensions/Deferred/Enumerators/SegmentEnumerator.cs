using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Extensions.Primitives;

namespace ExtraLinq.MsExtensions.Deferred.Enumerators;

internal class SegmentEnumerator :  IEnumerator<char>
{
    private StringSegment _segment;
    
    private char _current;
    
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
                _current = _segment[_index];
                ++_index;
                return true;
            }
            else
            {
                _state = -1;
            }
        }
        
        return false;
    }

    public void Reset()
    {
        throw new NotSupportedException();
    }

    public char Current => _current;

    object? IEnumerator.Current => Current;

    public void Dispose()
    {
        _segment = StringSegment.Empty;
    }
}