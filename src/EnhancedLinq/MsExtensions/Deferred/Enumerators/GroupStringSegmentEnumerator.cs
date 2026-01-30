/*
    EnhancedLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
    */

using System.Collections;
using System.Linq;
using DotPrimitives.Collections.Groupings;

namespace EnhancedLinq.MsExtensions.Deferred;

internal class GroupStringSegmentEnumerator<TKey> : IEnumerator<IGrouping<TKey, char>>
{
    private readonly Func<char, TKey> _selector;

    private readonly IEnumerator<char> _enumerator;
    
    private TKey? _currentKey;
    
    private int _state;

    private readonly GroupingCollection<TKey, char>  _groupingCollection;

    internal GroupStringSegmentEnumerator(StringSegment source, Func<char, TKey> selector)
    {
        _selector = selector;
        _state = 1;
        _enumerator = new SegmentEnumerator(source);
        _currentKey = _selector(_enumerator.Current);
        Current = new GroupingCollection<TKey, char>(_currentKey);
        _groupingCollection = new GroupingCollection<TKey, char>(_currentKey);
    }

    public bool MoveNext()
    {
        if (_state == 1)
        {
            try
            {
                while(_enumerator.MoveNext())
                {
                    if (_currentKey is not null && _currentKey.Equals(default(TKey)) ||
                        _currentKey is null)
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
                        Current = new GroupingCollection<TKey, char>
                            (_currentKey, _groupingCollection);
                        
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

        Dispose();
        return false;
    }

    public void Reset()
    {
        throw new NotSupportedException();
    }

    public IGrouping<TKey, char> Current { get; private set; }

    object IEnumerator.Current => Current;

    public void Dispose()
    {
        _enumerator?.Dispose();
    }
}