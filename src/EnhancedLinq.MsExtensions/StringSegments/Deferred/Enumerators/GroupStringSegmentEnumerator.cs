/*
      EnhancedLinq 
      Copyright (c) 2025 Alastair Lundy
      
     Licensed under the Apache License, Version 2.0 (the "License");
     you may not use this file except in compliance with the License.
     You may obtain a copy of the License at

         http://www.apache.org/licenses/LICENSE-2.0

     Unless required by applicable law or agreed to in writing, software
     distributed under the License is distributed on an "AS IS" BASIS,
     WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
     See the License for the specific language governing permissions and
     limitations under the License.
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using AlastairLundy.DotPrimitives.Collections.Groupings;

using Microsoft.Extensions.Primitives;

namespace AlastairLundy.EnhancedLinq.MsExtensions.StringSegments.Deferred.Enumerators;

internal class GroupStringSegmentEnumerator<TKey> : IEnumerator<IGrouping<TKey, char>>
{
    private readonly StringSegment _source;
    private readonly Func<char, TKey> _selector;

    private IEnumerator<char> _enumerator;
    
    private TKey? _currentKey;
    
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
            _currentKey = _selector(_enumerator.Current);
            _currentGrouping = new GroupingCollection<TKey, char>(_currentKey);
            _groupingCollection = new GroupingCollection<TKey, char>(_currentKey);
        }

        if (_state == 2)
        {
            try
            {
                while(_enumerator.MoveNext())
                {
                    if (_currentKey is not null && _currentKey.Equals(default(TKey)) || _currentKey is null)
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

    public IGrouping<TKey, char> Current => _currentGrouping;

    object? IEnumerator.Current => Current;

    public void Dispose()
    {
        _enumerator?.Dispose();
    }
}