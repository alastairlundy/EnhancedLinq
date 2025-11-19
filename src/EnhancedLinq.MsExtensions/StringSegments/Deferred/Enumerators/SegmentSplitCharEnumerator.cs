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

internal class SegmentSplitCharEnumerator : IEnumerator<StringSegment>
{
    private readonly StringSegment _segment;
    private readonly char _separator;
    
    private int _index;
    private int _state;

    private readonly List<char> _currentChars;
    private StringSegment _current;
    
    internal SegmentSplitCharEnumerator(StringSegment segment, char separator)
    {
        _segment = segment;
        _separator = separator;

        _index = 0;
        _state = 1;
        
        _currentChars = new List<char>();
    }
    
    public bool MoveNext()
    {
        if (_state == 1)
        {
            while (_index < _segment.Length)
            {
                ++_index;
                
                if (_segment[_index] != _separator)
                {
                    _currentChars.Add(_segment[_index]);
                }
                else
                {
                    _current = new StringSegment(string.Join("",  _currentChars));
                    _currentChars.Clear();

                    return true;
                }
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

    public StringSegment Current => _current;

    object? IEnumerator.Current => _current;

    public void Dispose()
    {
        _current = StringSegment.Empty;
        _currentChars.Clear();
    }
}