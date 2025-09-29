/*
    EnhancedLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/.
 */

using System.Collections;
using System.Collections.Generic;

namespace AlastairLundy.EnhancedLinq.Deferred.Enumerables;

internal class AppendRangeEnumerable<TSource> : IEnumerable<TSource>
{
    private readonly IEnumerable<TSource> _source;
    private readonly IEnumerable<TSource> _toBeAppended;

    internal AppendRangeEnumerable(IEnumerable<TSource> source, IEnumerable<TSource> toBeAppended)
    {
        _source = source;
        _toBeAppended = toBeAppended;
    }
    
    public IEnumerator<TSource> GetEnumerator()
    {
        IEnumerator<TSource> sourceEnumerator = _source.GetEnumerator();
        IEnumerator<TSource> appendEnumerator = _toBeAppended.GetEnumerator();
        
        try
        {
            while (sourceEnumerator.MoveNext())
            {
                yield return sourceEnumerator.Current;
            }
            
            while (appendEnumerator.MoveNext())
            {
                yield return appendEnumerator.Current;
            }
        }
        finally
        {
            sourceEnumerator.Dispose();
            appendEnumerator.Dispose();
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}