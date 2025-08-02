using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AlastairLundy.DotPrimitives.Collections.Groupings;
using Microsoft.Extensions.Primitives;

namespace ExtraLinq.MsExtensions.Deferred.Enumerators;

public class GroupStringSegmentEnumerator<TKey> : IEnumerator<IGrouping<TKey, char>>
{
    private readonly StringSegment _source;
    private readonly Func<char, TKey> _selector;

    private int _state;

    private GroupingCollection<TKey, char>  _groupingCollection;
    
    internal GroupStringSegmentEnumerator(StringSegment source, Func<char, TKey> selector)
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

    public IGrouping<TKey, char> Current { get; }

    object? IEnumerator.Current => Current;

    public void Dispose()
    {
        throw new NotImplementedException();
    }
}