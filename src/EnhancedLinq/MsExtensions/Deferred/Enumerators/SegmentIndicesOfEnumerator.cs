/*
    EnhancedLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
    */

using System.Collections;
using EnhancedLinq.MsExtensions.Immediate;

namespace EnhancedLinq.MsExtensions.Deferred;

internal class SegmentIndicesOfEnumerator : IEnumerator<int>
{
    private readonly StringSegment _source;
    private readonly StringSegment _segment;

    private int _state;
    
    private readonly IEnumerator<int> _segmentIndicesEnumerator;
    private int _segmentIndex;

    internal SegmentIndicesOfEnumerator(StringSegment source, StringSegment segment)
    {
        _source = source;
        _segment = segment;
        _state = 1;
        _segmentIndex = 0;
        _segmentIndicesEnumerator = new SegmentIndicesCharEnumerator(_source, _segment.First());
    }
    
    public bool MoveNext()
    {
        if (_state == 1)
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
                   Current = _segmentIndex;
                   return true;
                }
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

    object IEnumerator.Current => Current;

    public void Dispose()
    {
       _segmentIndicesEnumerator.Dispose();
    }
}