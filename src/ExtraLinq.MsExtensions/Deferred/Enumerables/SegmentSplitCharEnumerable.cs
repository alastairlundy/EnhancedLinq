using System.Collections;
using System.Collections.Generic;
using ExtraLinq.MsExtensions.Deferred.Enumerators;
using Microsoft.Extensions.Primitives;

namespace ExtraLinq.MsExtensions.Deferred.Enumerables;

internal class SegmentSplitCharEnumerable : IEnumerable<StringSegment>
{
    private readonly StringSegment _source;
    private readonly char _separator;

    internal SegmentSplitCharEnumerable(StringSegment source, char separator)
    {
        _source = source;
        _separator = separator;
    }
    
    public IEnumerator<StringSegment> GetEnumerator()
    {
        return new SegmentSplitCharEnumerator(_source, _separator);
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}