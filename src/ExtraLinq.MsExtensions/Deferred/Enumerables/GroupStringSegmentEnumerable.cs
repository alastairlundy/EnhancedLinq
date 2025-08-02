using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ExtendedLinq.MsExtensions.Deferred.Enumerators;
using Microsoft.Extensions.Primitives;

namespace ExtendedLinq.MsExtensions.Deferred.Enumerables;

internal class GroupStringSegmentEnumerable<TKey> : IEnumerable<IGrouping<IEnumerable<char>, TKey>>
{
    private readonly StringSegment _source;
    private readonly Func<char, TKey> _selector;

    internal GroupStringSegmentEnumerable(StringSegment source, Func<char, TKey> selector)
    {
        _source = source;
        _selector = selector;
    }
    
    public IEnumerator<IGrouping<IEnumerable<char>, TKey>> GetEnumerator()
    {
        return new GroupStringSegmentEnumerator<TKey>(_source, _selector);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}