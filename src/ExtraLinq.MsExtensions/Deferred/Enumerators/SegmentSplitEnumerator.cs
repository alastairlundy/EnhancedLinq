/*
    ExtraLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/.
 */

using System;
using System.Collections;
using System.Collections.Generic;
using ExtraLinq.MsExtensions.Immediate;
using ExtraLinq.MsExtensions.Immediate.StringSegments;

using Microsoft.Extensions.Primitives;

namespace ExtraLinq.MsExtensions.Deferred.Enumerators;

internal class SegmentSplitEnumerator : IEnumerator<StringSegment>
{
    private readonly StringSegment _segment;
    private readonly StringSegment _separator;

    private List<char> _currentChars;
    
    private StringSegment _current;

    private int _index;
    private int _state;

    private IEnumerable<int> _separatorIndices;
    private IEnumerator<int> _separatorIndicesEnumerator;
    
    internal SegmentSplitEnumerator(StringSegment segment, StringSegment separator)
    {
        _segment = segment;
        _separator = separator;
        _currentChars = new List<char>();

        _index = 0;
        _state = 1;
    }

    public bool MoveNext()
    {
        if (_state == 1)
        {
            _separatorIndices = _segment.IndicesOf(_separator);
            _separatorIndicesEnumerator = _separatorIndices.GetEnumerator();
            
            _state = 2;
        }
        if (_state == 2)
        {
            while (_index < _segment.Length)
            {
                int currentSeparatorIndex = _separatorIndicesEnumerator.Current;

                if (currentSeparatorIndex != -1 && _index != currentSeparatorIndex)
                {
                    _currentChars.Add(_segment[_index]);
                }

                if (_index == _separatorIndicesEnumerator.Current)
                {
                    _current = new StringSegment(string.Join("", _currentChars));
            
                    _currentChars.Clear();
                    ++_index;
                    return true;
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
        _separatorIndicesEnumerator.Dispose();
    }
}