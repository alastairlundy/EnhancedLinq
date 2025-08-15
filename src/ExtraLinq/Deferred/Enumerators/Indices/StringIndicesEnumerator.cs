/*
    ExtraLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/.
 */

using System.Collections;
using System.Collections.Generic;

namespace ExtraLinq.Deferred.Enumerators.Indices;

internal class StringIndicesEnumerator : IEnumerator<int>
{
    private int _current;

    public StringIndicesEnumerator(string str, char[] values)
    {
      
    }

    public bool MoveNext()
    {
        throw new System.NotImplementedException();
    }

    public void Reset()
    {
        throw new System.NotImplementedException();
    }

    int IEnumerator<int>.Current => _current;

    object? IEnumerator.Current => _current;

    public void Dispose()
    {
        throw new System.NotImplementedException();
    }
}