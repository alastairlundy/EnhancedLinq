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

    private IEnumerator<char> _enumerator;
    
    private TKey _currentKey;
    
    private int _state;

    private GroupingCollection<TKey, char>  _groupingCollection;

    private IGrouping<TKey, char> _currentGrouping;
    
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
            _enumerator = new SegmentEnumerator(_source);
        }

        if (_state == 2)
        {
            try
            {
                while(_enumerator.MoveNext())
                {
                    if (_currentKey is not null && _currentKey.Equals(default(TKey)))
                    {
                        _currentKey = _selector(_enumerator.Current);
                    }
                    
                    TKey key = _selector(_enumerator.Current);
                    
                    if (key is not null && key.Equals(_currentKey))
                    {
                        _groupingCollection.Add(_enumerator.Current);
                    }
                    else
                    {
                        _currentGrouping = new GroupingCollection<TKey, char>
                            (_currentKey, _groupingCollection, false);
                        
                        _groupingCollection.Clear();

                        _currentKey = default(TKey);
                        
                        return true;
                    }
                }
            }
            catch
            {
                Dispose();
                throw;
            }

            _state = -1;
        }

        return false;
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