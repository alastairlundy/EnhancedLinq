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

internal class SegmentSplitEnumerator : IEnumerator<StringSegment>
{
    private readonly StringSegment _segment;
    private readonly StringSegment _separator;

    private List<char> _currentChars;
    
    private StringSegment _current;

    private int _index;
    private int _state;

    private readonly IEnumerable<int> _separatorIndices;
    private IEnumerator<int> _separatorIndicesEnumerator;
    
    internal SegmentSplitEnumerator(StringSegment segment, StringSegment separator)
    {
        _segment = segment;
        _separator = separator;
        _currentChars = new List<char>();

        _index = 0;
        _state = 1;
        
        _separatorIndices = _segment.IndicesOf(_separator[0]);
    }

    public bool MoveNext()
    {
        if (_state == 1)
        {
            _separatorIndicesEnumerator = _separatorIndices.GetEnumerator();
            
            _state = 2;
        }
        if (_state == 2)
        {
            while (_index < _segment.Length)
            {
                int currentSeparatorIndex = _separatorIndicesEnumerator.Current;

                if (currentSeparatorIndex == -1)
                    break;
                
                StringSegment comparison = _segment.Subsegment(currentSeparatorIndex, _separator.Length);

                if (_index == _separatorIndicesEnumerator.Current && comparison.Equals(_segment))
                {
                    _current = new StringSegment(string.Join("", _currentChars));
            
                    _currentChars.Clear();
                    ++_index;
                    return true;
                }
                else
                {
                    _currentChars.Add(_segment[_index]);
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

    object? IEnumerator.Current => _current;

    public void Dispose()
    {
        _currentChars.Clear();
        _separatorIndicesEnumerator.Dispose();
    }
}