using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Extensions.Primitives;

namespace ExtraLinq.MsExtensions.Deferred.Enumerables;

internal class WhereSegmentEnumerable : IEnumerable<char>
{
    private readonly StringSegment _segment;
    private readonly Func<char, bool> _selector;

    internal WhereSegmentEnumerable(StringSegment segment, Func<char, bool> selector)
    {
        _segment = segment;
        _selector = selector;
    }
    
    public IEnumerator<char> GetEnumerator()
    {
        return new WhereSegmentEnumerator(_segment, _selector);
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}