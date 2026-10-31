/*
    EnhancedLinq 
    Copyright (c) 2025-2026 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
    */

using System.Collections;

namespace EnhancedLinq.Deferred.Enumerators;

internal class ElementsAtEnumerator<TSource> : IEnumerator<TSource>
{
    private readonly IEnumerable<TSource> _source;

    private readonly IEnumerable<int> _indices;

    private IEnumerator<int>? _indicesEnumerator;

    private IList<TSource>? _items;

    private int _state;

    internal ElementsAtEnumerator(IEnumerable<TSource> source, IEnumerable<int> indices)
    {
        _state = 1;

        _source = source;
        _indices = indices;
    }

    public bool MoveNext()
    {
        if (_state != 1)
        {
            return false;
        }

        try
        {
            _items ??= _source as IList<TSource> ?? new List<TSource>(_source);
            _indicesEnumerator ??= _indices.GetEnumerator();

            if (_indicesEnumerator.MoveNext())
            {
                int index = _indicesEnumerator.Current;

                ArgumentOutOfRangeException.ThrowIfNegative(index);
                ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index, _items.Count);

                Current = _items[index];
                return true;
            }
        }
        catch
        {
            Dispose();
            _state = -1;
            throw;
        }

        Dispose();
        _state = -1;
        return false;
    }

    public void Reset()
    {
        throw new NotSupportedException();
    }

    public TSource Current { get; private set; } = default!;

    object? IEnumerator.Current => Current;

    public void Dispose()
    {
        _indicesEnumerator?.Dispose();
    }
}
