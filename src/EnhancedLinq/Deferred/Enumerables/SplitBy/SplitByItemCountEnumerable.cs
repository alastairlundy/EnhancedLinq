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

internal class SplitByItemCountEnumerable<T> : IEnumerable<IEnumerable<T>>
{
    private readonly IEnumerable<T> _source;
    private readonly int _maximumItemCount;

    public SplitByItemCountEnumerable(IEnumerable<T> source, int maximumItemCount)
    {
        _source = source;
        _maximumItemCount = maximumItemCount;
    }

    
    public IEnumerator<IEnumerable<T>> GetEnumerator()
    {
        return new SplitByItemCountEnumerator<T>(_source, _maximumItemCount);
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}