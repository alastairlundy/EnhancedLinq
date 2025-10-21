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

using Microsoft.Extensions.Primitives;

namespace AlastairLundy.EnhancedLinq.MsExtensions.StringSegments.Deferred.Enumerators;

internal class SegmentSplitPredicateEnumerator : IEnumerator<StringSegment>
{
    private readonly StringSegment _source;
    private readonly Func<char, bool> _predicate;
    
    private StringSegment _current;
    
    private int _index;
    private int _state;

    private int _currentStart;

    internal SegmentSplitPredicateEnumerator(StringSegment source, Func<char, bool> predicate)
    {
        _source = source;
        _predicate = predicate;
        _state = 1;
    }
    
    public bool MoveNext()
    {
        if (_state == 1)
        {
            _currentStart = -1;
            
            _state = 2;
        }
        if (_state == 2)
        {
            while (_index < _source.Length)
            {
                bool separate = _predicate(_source[_index]);

                if (_currentStart == -1) 
                    _currentStart = _index;

                if (separate)
                {
                    _current = _source.Subsegment(_currentStart, _index - _currentStart);
                    _currentStart = -1;
                    _index++;
                    return true;
                }
                else
                {
                    _index++;
                }
            }
            
            _state = -1;
        }

        _state = -1;
        Dispose();
        return false;
    }

    public void Reset()
    {
        throw new NotSupportedException();
    }

    public StringSegment Current => _current;

    object? IEnumerator.Current => Current;

    public void Dispose()
    {
        _current = StringSegment.Empty;
    }
}