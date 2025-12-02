/*
    EnhancedLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
    */

using System.Collections;
using System.Collections.Generic;

namespace EnhancedLinq.Deferred.Enumerables;

internal class PrependRangeEnumerable<TSource> : IEnumerable<TSource>
{
    private readonly IEnumerable<TSource> _source;
    private readonly IEnumerable<TSource> _toBePrepended;

    internal PrependRangeEnumerable(IEnumerable<TSource> source, IEnumerable<TSource> toBePrepended)
    {
        _source = source;
        _toBePrepended = toBePrepended;
    }
    
    public IEnumerator<TSource> GetEnumerator()
    {
        IEnumerator<TSource> sourceEnumerator = _source.GetEnumerator();
        IEnumerator<TSource> prependEnumerator = _toBePrepended.GetEnumerator();
        
        try
        {
            while (prependEnumerator.MoveNext())
            {
                yield return prependEnumerator.Current;
            }
            
            while (sourceEnumerator.MoveNext())
            {
                yield return sourceEnumerator.Current;
            }
        }
        finally
        {
            sourceEnumerator.Dispose();
            prependEnumerator.Dispose();
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}