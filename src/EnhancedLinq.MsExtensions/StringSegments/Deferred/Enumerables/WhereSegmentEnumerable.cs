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
using EnhancedLinq.MsExtensions.StringSegments.Deferred.Enumerators;
using Microsoft.Extensions.Primitives;

namespace EnhancedLinq.MsExtensions.StringSegments.Deferred.Enumerables;

internal class WhereSegmentEnumerable : IEnumerable<char>
{
    private readonly StringSegment _segment;
    private readonly Func<char, bool> _selector;

    internal WhereSegmentEnumerable(StringSegment segment, Func<char, bool> selector)
    {
        _segment = segment;
        _selector = selector;
    }
    
    public IEnumerator<char> GetEnumerator() => new WhereSegmentEnumerator(_segment, _selector);

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}