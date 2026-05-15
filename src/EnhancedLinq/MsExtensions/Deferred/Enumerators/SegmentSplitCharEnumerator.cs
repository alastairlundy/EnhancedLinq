/*
    EnhancedLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
*/

using System.Collections;

namespace EnhancedLinq.MsExtensions.Deferred;

internal class SegmentSplitCharEnumerator : IEnumerator<StringSegment>
{
    private readonly StringSegment _segment;
    private readonly char _separator;
    
    private int _index;
    private int _state;
    private int _currentStart; // start index of the current segment
    
    internal SegmentSplitCharEnumerator(StringSegment segment, char separator)
    {
        _segment = segment;
        _separator = separator;
        _index = 0;
        _state = 1;
        _currentStart = 0;
    }
    
    public bool MoveNext()
    {
        if (_state == 1)
        {
            try
            {
                while (_index < _segment.Length)
                {
                    if (_segment[_index] == _separator)
                    {
                        // Return the segment from _currentStart up to the current index
                        Current = _segment.Subsegment(_currentStart, _index - _currentStart);
                        // Move start to the character after the separator for the next segment
                        _currentStart = _index + 1;
                        _index++;
                        return true;
                    }
                    _index++;
                }
                
                // Handle the final segment if we reached the end without a trailing separator
                if (_currentStart < _segment.Length)
                {
                    Current = _segment.Subsegment(_currentStart, _segment.Length - _currentStart);
                    return true;
                }
            }
            catch
            {
                Dispose();
                throw;
            }
            finally
            {
                // Only transition to the -1 state when we are truly exhausted
                if (_state == 1)
                {
                    _state = -1;
                }
            }
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
        Current = StringSegment.Empty;
        // No _currentChars to clear
    }
}