/*
    ExtraLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/.
 */

using System.Collections;
using System.Collections.Generic;

using AlastairLundy.DotExtensions.MsExtensions.StringSegments;

using ExtraLinq.MsExtensions.Deferred.Enumerators;

using Microsoft.Extensions.Primitives;

namespace ExtraLinq.MsExtensions.Deferred.Enumerables;

internal class SegmentIndicesEnumerable : IEnumerable<int>
{
    private readonly StringSegment _source;
    private readonly StringSegment _segment;

    internal SegmentIndicesEnumerable(StringSegment source, char c)
    {
        _source = source;
        _segment = new  StringSegment("{c}");
    }
    
    internal SegmentIndicesEnumerable(StringSegment source, StringSegment segment)
    {
        _source = source;
        _segment = segment;
    }

    public IEnumerator<int> GetEnumerator()
    {
        if(_segment.Length == 1)
            return new SegmentIndicesCharEnumerator(_source, _segment.First());
        else
            return new SegmentIndicesOfEnumerator(_source, _segment);
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}