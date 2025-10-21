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

internal class SegmentIndicesCharEnumerator : IEnumerator<int>
{
    private readonly StringSegment _segment;
    private readonly char _c;

    private int _current;

    private int _state;
    private int _index;
    
    public SegmentIndicesCharEnumerator(StringSegment segment, char c)
    {
        _segment = segment;
        _c = c;
        _index = 0;
        _state = 1;
    }

    public bool MoveNext()
    {
        if (_state == 1)
        {
            while (_index < _segment.Length)
            {
                if (_segment[_index] == _c)
                {
                    _current = _index;
                    return true;
                }

                _index++;
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

    public int Current => _current;

    object? IEnumerator.Current => _current;

    public void Dispose()
    {
      
    }
}