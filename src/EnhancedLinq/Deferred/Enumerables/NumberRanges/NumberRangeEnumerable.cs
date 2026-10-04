/*
    EnhancedLinq 
    Copyright (c) 2025-2026 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
    */

#if NET8_0_OR_GREATER
using System.Collections;
using System.Numerics;
using EnhancedLinq.Deferred.Enumerators.NumberRanges;

namespace EnhancedLinq.Deferred.Enumerables.NumberRanges;

internal class NumberRangeEnumerable<TNumber> : IEnumerable<TNumber> where TNumber : INumber<TNumber>
{
    private readonly TNumber _start;
    private readonly TNumber _count;
    private readonly TNumber _incrementor;
    
    internal NumberRangeEnumerable(TNumber start, TNumber count, TNumber incrementor)
    {
        _start = start;
        _count = count;
        _incrementor = incrementor;
    }
    
    public IEnumerator<TNumber> GetEnumerator() => new NumberRangeEnumerator<TNumber>(_start, _count, _incrementor);

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
#endif