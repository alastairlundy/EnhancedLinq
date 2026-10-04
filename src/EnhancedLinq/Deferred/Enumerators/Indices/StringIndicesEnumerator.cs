/*
    EnhancedLinq 
    Copyright (c) 2025-2026 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
    */

using System.Collections;

namespace EnhancedLinq.Deferred.Enumerators.Indices;

internal class StringIndicesEnumerator : IEnumerator<int>
{
    private readonly string _str;
    private readonly string _substring;

    private readonly IEnumerator<int> _indicesEnumerator;
    
    private int _current;

    private int _state;

    internal StringIndicesEnumerator(string str, string substring)
    {
        _str = str;
        _substring = substring;
        _state = 1;

        IEnumerable<int> indices = str.IndicesOf(substring[0]);
        _indicesEnumerator = indices.GetEnumerator();
    }

    public bool MoveNext()
    {
        if (_state != 1)
        {
            return false;
        }

        try
        {
            while (_indicesEnumerator.MoveNext())
            {
                int candidateIndex = _indicesEnumerator.Current;

                if (candidateIndex + _substring.Length > _str.Length)
                {
                    continue;
                }

                string compare = _str.Substring(candidateIndex,
                    _substring.Length);

                if (_substring.Equals(compare))
                {
                    _current = candidateIndex;
                    return true;
                }
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

    int IEnumerator<int>.Current => _current;

    object IEnumerator.Current => _current;

    public void Dispose()
    {
        _indicesEnumerator.Dispose();
    }
}