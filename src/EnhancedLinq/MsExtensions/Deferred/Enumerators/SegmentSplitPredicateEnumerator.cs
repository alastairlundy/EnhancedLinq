/*
    EnhancedLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
    */

using System.Collections;

namespace EnhancedLinq.MsExtensions.Deferred;

internal class SegmentSplitPredicateEnumerator : IEnumerator<StringSegment>
{
    private readonly StringSegment _source;
    private readonly Func<char, bool> _predicate;

    private int _index;
    private int _state;
    private int _currentStart;

    internal SegmentSplitPredicateEnumerator(StringSegment source, Func<char, bool> predicate)
    {
        _source = source;
        _predicate = predicate;
        _state = 1;
        _currentStart = -1;
    }

    public bool MoveNext()
    {
        if (_state == 1)
        {
            // Handle empty source case
            if (_source.Length == 0)
            {
                Current = _source.Subsegment(0, 0);
                _state = -1;
                return true;
            }

            try
            {
                while (_index < _source.Length)
                {
                    bool separate = _predicate(_source[_index]);

                    // Initialize start index of current segment if not already set
                    if (_currentStart == -1)
                        _currentStart = _index;

                    if (separate)
                    {
                        // Return segment up to current index
                        Current = _source.Subsegment(_currentStart, _index - _currentStart);
                        _currentStart = -1;
                        _index++;
                        return true;
                    }
                    else
                    {
                        _index++;
                    }
                }

                // End of string: if there's a pending segment, return it
                if (_currentStart != -1)
                {
                    Current = _source.Subsegment(_currentStart);
                    _currentStart = -1;
                    _state = -1;
                    return true;
                }
            }
            catch
            {
                Dispose();
                throw;
            }

            // If we exhausted all characters without returning a segment,
            // mark as completed
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
        Current = StringSegment.Empty;
    }
}