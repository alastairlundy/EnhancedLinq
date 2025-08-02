using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AlastairLundy.DotPrimitives.Collections.Groupings;
using Microsoft.Extensions.Primitives;

namespace ExtendedLinq.MsExtensions.Deferred.Enumerators;

public class GroupStringSegmentEnumerator<TKey> : IEnumerator<IGrouping<IEnumerable<char>, TKey>>
{
    private readonly StringSegment _source;
    private readonly Func<char, TKey> _selector;

    private int _state;

    private GroupingCollection<char, TKey>  _groupingCollection;
    
    public GroupStringSegmentEnumerator(StringSegment source, Func<char, TKey> selector)
    {
        _source = source;
        _selector = selector;
        _state = 1;
    }

    public bool MoveNext()
    {
        if (_state == 1)
        {
            
        }

        if (_state == 2)
        {
            
        }
        
    }

    public void Reset()
    {
        throw new NotSupportedException();
    }

    public IGrouping<IEnumerable<char>, TKey> Current { get; }

    object? IEnumerator.Current => Current;

    public void Dispose()
    {
        throw new NotImplementedException();
    }
}