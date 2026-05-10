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
                while (_index < _segment.Length)
                {
                    if (!_separatorIndicesEnumerator.MoveNext())
                    {
                        Current = _segment.Subsegment(_index);
                        _index = _segment.Length;
                        _state = -1;
                        return true;
                    }

                    int separatorIndex = _separatorIndicesEnumerator.Current;

                    if (separatorIndex >= _index)
                    {
                        if (separatorIndex + _separator.Length <= _segment.Length &&
                            _segment.Subsegment(separatorIndex, _separator.Length)
                                .Equals(_separator, StringComparison.Ordinal))
                        {
                            Current = _segment.Subsegment(_index, separatorIndex - _index);
                            _index = separatorIndex + _separator.Length;
                            return true;
                        }
                    }
                }

                if (_index == _segment.Length)
                {
                    Current = new StringSegment(string.Empty);
                    _state = -1;
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
                _state = -1;
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
        _separatorIndicesEnumerator.Dispose();
    }
}