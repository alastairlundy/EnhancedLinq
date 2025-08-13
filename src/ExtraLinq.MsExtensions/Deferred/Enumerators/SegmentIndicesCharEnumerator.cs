using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Extensions.Primitives;

namespace ExtraLinq.MsExtensions.Deferred.Enumerators;

internal class SegmentIndicesCharEnumerator : IEnumerator<int>
{
    private readonly StringSegment _segment;
    private readonly char _c;

    private int _current;

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
                    _current = _index;
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

    public int Current => _current;

    object? IEnumerator.Current => _current;

    public void Dispose()
    {
       
    }
}