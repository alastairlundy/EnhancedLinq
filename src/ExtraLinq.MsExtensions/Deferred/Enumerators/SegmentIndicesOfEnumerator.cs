using System;
using System.Collections;
using System.Collections.Generic;

using AlastairLundy.DotExtensions.MsExtensions.StringSegments;

using Microsoft.Extensions.Primitives;

namespace ExtraLinq.MsExtensions.Deferred.Enumerators;

internal class SegmentIndicesOfEnumerator : IEnumerator<int>
{
    private readonly StringSegment _source;
    private readonly StringSegment _segment;

    private int _state;
    
    private IEnumerator<int> _segmentIndicesEnumerator;
    private int _segmentIndex;

    private int _current;
    
    internal SegmentIndicesOfEnumerator(StringSegment source, StringSegment segment)
    {
        _source = source;
        _segment = segment;
        _state = 1;
        _segmentIndex = 0;
    }
    
    public bool MoveNext()
    {
        if (_state == 1)
        {
            _segmentIndicesEnumerator = new SegmentIndicesCharEnumerator(_source, _segment.First());
        }

        if (_state == 2)
        {
            while (_segmentIndicesEnumerator.MoveNext())
            {
                _segmentIndex = _segmentIndicesEnumerator.Current;

                if (_segmentIndex == -1)
                    continue;
                    
                StringSegment indexSegment = _source.Subsegment(_segmentIndex, _segment.Length);

                ++_segmentIndex;
                    
                if (indexSegment.Equals(_segment))
                {
                   _current = _segmentIndex;
                   return true;
                }
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

    public int Current => _current;

    object? IEnumerator.Current => _current;

    public void Dispose()
    {
       _segmentIndicesEnumerator.Dispose();
    }
}