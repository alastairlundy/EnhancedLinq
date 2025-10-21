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

internal class SegmentEnumerator :  IEnumerator<char>
{
    private StringSegment _segment;
    
    private char _current;
    
    private int _state;
    private int _index;

    internal SegmentEnumerator(StringSegment segment)
    {
        _segment = segment;
        _state = 1;
        _index = 0;
    }
    
    public bool MoveNext()
    {
        if (_state == 1)
        {
            if (_index <= _segment.Length - 1)
            {
                _current = _segment[_index];
                ++_index;
                return true;
            }
            else
            {
                _state = -1;
            }
        }
        
        return false;
    }

    public void Reset()
    {
        throw new NotSupportedException();
    }

    public char Current => _current;

    object? IEnumerator.Current => Current;

    public void Dispose()
    {
        _segment = StringSegment.Empty;
    }
}