/*
    EnhancedLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/.
 */

using System.Collections;
using System.Collections.Generic;
using EnhancedLinq.MsExtensions.Deferred.Enumerators;
using Microsoft.Extensions.Primitives;

namespace EnhancedLinq.MsExtensions.Deferred.Enumerables;

internal class SegmentEnumerable : IEnumerable<char>
{
    private readonly StringSegment _segment;

    internal SegmentEnumerable(StringSegment segment)
    {
        _segment = segment;
    }
    
    public IEnumerator<char> GetEnumerator()
    {
        return new SegmentEnumerator(_segment);
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}