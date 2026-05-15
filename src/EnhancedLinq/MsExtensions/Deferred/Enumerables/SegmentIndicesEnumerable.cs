/*
    EnhancedLinq 
    Copyright (c) 2025-2026 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
    */

using System.Collections;

namespace EnhancedLinq.MsExtensions.Deferred;

internal class SegmentIndicesEnumerable : IEnumerable<int>
{
    private readonly StringSegment _source;
    private readonly StringSegment _segment;

    private readonly bool useCharEnumerator;

    internal SegmentIndicesEnumerable(StringSegment source, char c)
    {
        _source = source;
        _segment = new  StringSegment($"{c}");
        useCharEnumerator = true;
    }
    
    internal SegmentIndicesEnumerable(StringSegment source, StringSegment segment)
    {
        _source = source;
        _segment = segment;
        useCharEnumerator = false;
    }

    public IEnumerator<int> GetEnumerator()
    {
        if(useCharEnumerator)
            return new SegmentIndicesCharEnumerator(_source, _segment[0]);
        
        return new SegmentIndicesOfEnumerator(_source, _segment);
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}