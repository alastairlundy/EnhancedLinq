/*
    EnhancedLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
    */

#if NET8_0_OR_GREATER

using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using EnhancedLinq.Deferred.Enumerators.NumberRanges;

namespace EnhancedLinq.Deferred.Enumerables.NumberRanges;

internal class NumberRangeEnumerable<TNumber> : IEnumerable<TNumber> where TNumber : INumber<TNumber>
{
    private readonly List<TNumber> _source;
    
    internal NumberRangeEnumerable(TNumber start, TNumber count, TNumber incrementor)
    {
        _source = new List<TNumber>();
        
        TNumber current = start;
        TNumber end = start + count;
        
        while (current != end + TNumber.One)
        {
            _source.Add(current);
            
            current += incrementor;
        }
    }
    
    public IEnumerator<TNumber> GetEnumerator()
    {
        return new NumberRangeEnumerator<TNumber>(_source);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
#endif