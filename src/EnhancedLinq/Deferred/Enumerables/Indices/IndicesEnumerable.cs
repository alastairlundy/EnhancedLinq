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
using EnhancedLinq.Deferred.Enumerators.Indices;

namespace EnhancedLinq.Deferred.Enumerables;

internal class IndicesEnumerable<T> : IEnumerable<int>
{
    private readonly IEnumerable<T> _source;
    private readonly Func<T, bool> _predicate;

    public IndicesEnumerable(IEnumerable<T> source, Func<T, bool> predicate)
    {
        _source = source;
        _predicate = predicate;
    }

    public IEnumerator<int> GetEnumerator()
    {
        return new IndicesEnumerator<T>(_source, _predicate);
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}