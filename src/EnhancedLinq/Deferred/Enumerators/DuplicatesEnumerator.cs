/*
    EnhancedLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
    */

using System;
using System.Collections;
using System.Collections.Generic;

namespace EnhancedLinq.Deferred.Enumerators;

internal class DuplicatesEnumerator<TSource> : IEnumerator<TSource>
{
    private readonly HashSet<TSource> _hashSet;

    private int _state;
    
    private readonly IEnumerator<TSource> _enumerator;

    internal DuplicatesEnumerator(IEnumerable<TSource> source, IEqualityComparer<TSource> comparer)
    {
        _hashSet = new HashSet<TSource>(comparer);
        _state = 0;
        
        _enumerator = source.GetEnumerator();
        Current = _enumerator.Current;
    }
    
    public bool MoveNext()
    {
        if (_state == 1)
        {
            while(_enumerator.MoveNext())
            {
                bool isDuplicate =  _hashSet.Add(_enumerator.Current);

                if (isDuplicate)
                {
                    Current = _enumerator.Current;
                    return true;
                }
            }

            _state = 2;
        }
        
        Dispose();
        return false;
    }

    public void Reset()
    {
        throw new NotSupportedException();
    }

    public TSource Current { get; private set; }

    object? IEnumerator.Current => Current;

    public void Dispose()
    {
        _enumerator?.Dispose();
    }
}