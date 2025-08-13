using System.Collections;
using System.Collections.Generic;

using ExtraLinq.MsExtensions.Deferred.Enumerators;

using Microsoft.Extensions.Primitives;

namespace ExtraLinq.MsExtensions.Deferred.Enumerables;

internal class SegmentIndicesEnumerable : IEnumerable<int>
{
    private readonly StringSegment _segment;
    private readonly char _c;

    internal SegmentIndicesEnumerable(StringSegment segment, char c)
    {
        _segment = segment;
        _c = c;
    }
    
    public IEnumerator<int> GetEnumerator() => new SegmentIndicesEnumerator(_segment, _c);

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}