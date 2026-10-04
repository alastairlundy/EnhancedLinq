/*
    EnhancedLinq 
    Copyright (c) 2025-2026 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
    */

using System.Collections;

namespace EnhancedLinq.Deferred.Enumerators.Ranges;

internal class InsertRangeEnumerator<T> : IEnumerator<T>
{
    private readonly int _indexToInsertAt;

    private readonly IEnumerator<T> _sourceEnumerator;
    private readonly IEnumerator<T> _toBeInsertedEnumerator;

    private int _state;
    private int _index;
    private bool _insertsDone;

    internal InsertRangeEnumerator(IEnumerable<T> source, int indexToInsertAt, IEnumerable<T> toBeInserted)
    {
        _indexToInsertAt = indexToInsertAt;
        _state = 1;
        _sourceEnumerator = source.GetEnumerator();
        _toBeInsertedEnumerator = toBeInserted.GetEnumerator();
    }
    
    public bool MoveNext()
    {
        if (_state != 1)
        {
            return false;
        }

        // Phase 1: source elements before the insertion point.
        if (_index < _indexToInsertAt)
        {
            if (_sourceEnumerator.MoveNext())
            {
                Current = _sourceEnumerator.Current;
                _index++;
                return true;
            }
            // Source exhausted before insertion point: fall through to inserts (append).
        }

        // Phase 2: inserted elements.
        if (!_insertsDone)
        {
            if (_toBeInsertedEnumerator.MoveNext())
            {
                Current = _toBeInsertedEnumerator.Current;
                return true;
            }

            _insertsDone = true;
        }

        // Phase 3: remaining source elements.
        if (_sourceEnumerator.MoveNext())
        {
            Current = _sourceEnumerator.Current;
            _index++;
            return true;
        }

        Dispose();
        _state = -1;
        return false;
    }

    public void Reset()
    {
        throw new NotSupportedException();
    }

    public T Current { get; private set; } = default!;

    object? IEnumerator.Current => Current;

    public void Dispose()
    {
        _sourceEnumerator.Dispose();
        _toBeInsertedEnumerator.Dispose();
    }
}