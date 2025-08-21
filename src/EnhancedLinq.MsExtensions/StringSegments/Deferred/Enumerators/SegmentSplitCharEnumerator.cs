/*
    EnhancedLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/.
 */

using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Extensions.Primitives;

namespace EnhancedLinq.MsExtensions.StringSegments.Deferred.Enumerators;

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