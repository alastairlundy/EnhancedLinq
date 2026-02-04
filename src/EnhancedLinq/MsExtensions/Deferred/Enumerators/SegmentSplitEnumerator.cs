/*
    EnhancedLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
    */

using System.Collections;

namespace EnhancedLinq.MsExtensions.Deferred;

internal class SegmentSplitEnumerator : IEnumerator<StringSegment>
{
    private readonly StringSegment _segment;
    private readonly StringSegment _separator;

    private readonly List<char> _currentChars;

    private int _index;
    private int _state;

    private readonly IEnumerator<int> _separatorIndicesEnumerator;
    
    internal SegmentSplitEnumerator(StringSegment segment, StringSegment separator)
    {
        _segment = segment;
        _separator = separator;
        _currentChars = new List<char>();

        _index = 0;
        _state = 1;
        
        IEnumerable<int> separatorIndices = _segment.IndicesOf(_separator[0]);
        _separatorIndicesEnumerator = separatorIndices.GetEnumerator();
    }

    public bool MoveNext()
    {
        if (_state == 1)
        {
            while (_index < _segment.Length)
            {
                int currentSeparatorIndex = _separatorIndicesEnumerator.Current;

                if (currentSeparatorIndex == -1)
                    break;
                
                StringSegment comparison = _segment.Subsegment(currentSeparatorIndex, _separator.Length);

                if (_index == _separatorIndicesEnumerator.Current && comparison.Equals(_segment))
                {
                    Current = new StringSegment(string.Join("", _currentChars));
            
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

        Dispose();
        return false;
    }

    public void Reset()
    {
        throw new NotSupportedException();
    }

    public StringSegment Current { get; private set; }

    object IEnumerator.Current => Current;

    public void Dispose()
    {
        _currentChars.Clear();
        _separatorIndicesEnumerator.Dispose();
    }
}