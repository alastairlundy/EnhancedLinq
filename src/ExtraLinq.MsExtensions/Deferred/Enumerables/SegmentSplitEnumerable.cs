using System.Collections;
using System.Collections.Generic;

using AlastairLundy.DotExtensions.MsExtensions.StringSegments;

using ExtraLinq.MsExtensions.Deferred.Enumerators;

using Microsoft.Extensions.Primitives;

namespace ExtraLinq.MsExtensions.Deferred.Enumerables;

public class SegmentSplitEnumerable : IEnumerable<StringSegment>
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