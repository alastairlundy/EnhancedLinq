/*
    EnhancedLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/.
 */

using System.Collections;
using System.Collections.Generic;
using AlastairLundy.EnhancedLinq.MsExtensions.StringSegments.Deferred.Enumerators;
using AlastairLundy.EnhancedLinq.MsExtensions.StringSegments.Immediate;
using Microsoft.Extensions.Primitives;

namespace AlastairLundy.EnhancedLinq.MsExtensions.StringSegments.Deferred.Enumerables;

internal class SegmentSplitEnumerable : IEnumerable<StringSegment>
{
    private readonly StringSegment _segment;
    private readonly StringSegment _separator;

    internal SegmentSplitEnumerable(StringSegment segment,StringSegment separator)
    {
        _segment = segment;
        _separator = separator;
    }
    
    public IEnumerator<StringSegment> GetEnumerator()
    {
        if(_separator.Length == 1)
            return new SegmentSplitCharEnumerator(_segment, _separator.First());
        else
            return new SegmentSplitEnumerator(_segment, _separator);
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}