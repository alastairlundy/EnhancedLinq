/*
    EnhancedLinq 
    Copyright (c) 2025-2026 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
    */

using System.Collections;

namespace EnhancedLinq.MsExtensions.Deferred;

internal class WhereSegmentEnumerator : IEnumerator<char>
{
    private readonly Func<char, bool> _selector;

    private readonly IEnumerator<char> _enumerator;

    private int _state;
    
    internal WhereSegmentEnumerator(StringSegment segment, Func<char, bool> selector)
    {
        _selector = selector;
        _state = 1;
        _enumerator = new SegmentEnumerator(segment);
    }

    public bool MoveNext()
    {
        if (_state == 1)
        {
            try
            {
                while(_enumerator.MoveNext())
                {
                    if (_selector(_enumerator.Current))
                    {
                        Current = _enumerator.Current;
                        return true;
                    }
                }
            }
            catch
            {
                Dispose();
                throw;
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

    public char Current { get; private set; }

    object? IEnumerator.Current => Current;

    public void Dispose()
    {
        _enumerator.Dispose();
    }
}