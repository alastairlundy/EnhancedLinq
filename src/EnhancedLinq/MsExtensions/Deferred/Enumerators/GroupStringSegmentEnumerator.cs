/*
    EnhancedLinq 
    Copyright (c) 2025-2026 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
    */

using System.Collections;
using System.Linq;

namespace EnhancedLinq.MsExtensions.Deferred;

internal class GroupStringSegmentEnumerator<TKey> : IEnumerator<IGrouping<TKey, char>>
{
    private readonly Func<char, TKey> _selector;

    private readonly IEnumerator<char> _enumerator;
    
    private TKey _currentKey;
    
    private int _state;

    private GroupingCollection<TKey, char>  _groupingCollection;

    internal GroupStringSegmentEnumerator(StringSegment source, Func<char, TKey> selector)
    {
        _selector = selector;
        _state = 1;
        _enumerator = new SegmentEnumerator(source);
        
        // Set default values for compiler
        _currentKey = _selector(_enumerator.Current);
        _groupingCollection = new GroupingCollection<TKey, char>(_currentKey);
        Current = new GroupingCollection<TKey, char>(_currentKey);
    }

    public bool MoveNext()
    {
        if (_state == 1)
        {
            try
            {
                if (!_enumerator.MoveNext())
                {
                    _state = -1;
                    return false;
                }

                TKey key = _selector(_enumerator.Current);
                _currentKey = key;
                _groupingCollection = new GroupingCollection<TKey, char>(_currentKey) { _enumerator.Current };
                Current = _groupingCollection;
                _state = 2;
                return true;
            }
            catch
            {
                Dispose();
                throw;
            }
        }
        
        if (_state == 2)
        {
            try
            {
                while (_enumerator.MoveNext())
                {
                    TKey key = _selector(_enumerator.Current);
                    if (EqualityComparer<TKey>.Default.Equals(key, _currentKey))
                    {
                        _groupingCollection.Add(_enumerator.Current);
                    }
                    else
                    {
                        _currentKey = key;
                        _groupingCollection = new GroupingCollection<TKey, char>(_currentKey) { _enumerator.Current };
                        Current = _groupingCollection;
                        return true;
                    }
                }
                
                _state = -1;
                return false;
            }
            catch
            {
                Dispose();
                throw;
            }
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
        _enumerator.Dispose();
    }
}