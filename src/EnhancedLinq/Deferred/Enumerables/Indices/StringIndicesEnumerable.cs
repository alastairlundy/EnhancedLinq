/*
    EnhancedLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/.
 */

using System.Collections;
using System.Collections.Generic;
using AlastairLundy.EnhancedLinq.Deferred.Enumerators.Indices;

namespace AlastairLundy.EnhancedLinq.Deferred.Enumerables;

internal class StringIndicesEnumerable : IEnumerable<int>
{
    private readonly string _str;
    private readonly string _substring;

    public StringIndicesEnumerable(string str, string substring)
    {
        _str = str;
        _substring = substring;
    }
    
    public IEnumerator<int> GetEnumerator()
    {
        return new StringIndicesEnumerator(_str, _substring);
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}