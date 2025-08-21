/*
    EnhancedLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/.
 */

using System.Collections;
using System.Collections.Generic;
using EnhancedLinq.Deferred.Enumerators;

namespace EnhancedLinq.Deferred.Enumerables;

internal class SplitByEnumerableCountEnumerable<T> : IEnumerable<IEnumerable<T>>
{
    private readonly IEnumerable<T> _source;
    private readonly int _maxEnumerableCount;

    public SplitByEnumerableCountEnumerable(IEnumerable<T> source, int maxEnumerableCount)
    {
        _source = source;
        _maxEnumerableCount = maxEnumerableCount;
    }

    public IEnumerator<IEnumerable<T>> GetEnumerator()
    {
        return new SplitByEnumerableCountEnumerator<T>(_source, _maxEnumerableCount);
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}