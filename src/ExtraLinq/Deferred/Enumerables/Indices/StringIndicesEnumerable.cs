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
    private readonly char[] _charArray;

    public StringIndicesEnumerable(string str, char[] charArray)
    {
        _str = str;
        _charArray = charArray;
    }
    
    public IEnumerator<int> GetEnumerator()
    {
        return new StringIndicesEnumerator(_str, _charArray);
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}