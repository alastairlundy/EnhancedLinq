/*
    ExtraLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/.
 */

using System.Collections;
using System.Collections.Generic;

using ExtraLinq.Deferred.Enumerators.Indices;

namespace ExtraLinq.Deferred.Enumerables;

internal class StringIndicesEnumerable : IEnumerable<int>
{
    private readonly string _str;
    private readonly string _segment;

    public StringIndicesEnumerable(string str, string segment)
    {
        _str = str;
        _segment = segment;
    }
    
    public IEnumerator<int> GetEnumerator()
    {
        return new StringIndicesEnumerator(_str, _segment);
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}