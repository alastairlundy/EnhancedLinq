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
    
    private int _index;
    private int _state;
    private readonly IEnumerator<int> _separatorIndicesEnumerator;
    
    internal SegmentSplitEnumerator(StringSegment segment, StringSegment separator)
    {
        _segment = segment;
        _separator = separator;
        _index = 0;
        _state = 1;
        IEnumerable<int> separatorIndices = _segment.IndicesOf(_separator[0]);
        _separatorIndicesEnumerator = separatorIndices.GetEnumerator();
    }
    
    public bool MoveNext()
    {
        if (_state == 1)
        {
            try
            {
                // Attempt to find and process each separator occurrence
                while (_index < _segment.Length)
                {
                    if (!_separatorIndicesEnumerator.MoveNext())
                    {
                        // No more separators: return the remaining segment (including empty)
                        if (_index < _segment.Length)
                        {
                            Current = _segment.Subsegment(_index);
                            _state = -1;
                            return true;
                        }
                        // If at end, still return empty segment to match Split behavior
                        Current = new StringSegment(string.Empty);
                        _state = -1;
                        return true;
                    }
                    
                    int separatorIndex = _separatorIndicesEnumerator.Current;
                    
                    // Ensure we are progressing forward
                    if (separatorIndex < _index)
                        continue;
                    
                    // Verify the characters at separatorIndex match the separator
                    if (separatorIndex + _separator.Length <= _segment.Length &&
                        _segment.Subsegment(separatorIndex, _separator.Length)
                            .Equals(_separator, StringComparison.Ordinal))
                    {
                        // Return the segment that precedes the separator
                        if (_index < separatorIndex)
                        {
                            Current = _segment.Subsegment(_index, separatorIndex - _index);
                            _index = separatorIndex + _separator.Length;
                            return true;
                        }
                        // Handle case: separator at the very beginning of a segment
                        _index = separatorIndex + _separator.Length;
                        // Continue loop to find next segment
                    }
                    else
                    {
                        // Mismatch in subsequent characters - continue searching
                        continue;
                    }
                }
            }
            catch
            {
                Dispose();
                throw;
            }
            
            // If we exit the loop without returning, enumeration is exhausted
            Dispose();
            return false;
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
        _separatorIndicesEnumerator.Dispose();
    }
}