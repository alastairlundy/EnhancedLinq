/*
    EnhancedLinq.Async
    Copyright (c) 2025-2026 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/.
*/

#if NET8_0_OR_GREATER
using System.Linq;
using System.Threading;

using System.Numerics;
using EnhancedLinq.Async.Deferred.Enumerators;

namespace EnhancedLinq.Async.Deferred;

internal class AsyncNumberRangeEnumerable<TNumber> : IAsyncEnumerable<TNumber> where TNumber : INumber<TNumber>
{
    private readonly List<TNumber> _source;
    
    internal AsyncNumberRangeEnumerable(TNumber start, TNumber count, TNumber incrementor)
    {
        _source = [];
        
        TNumber current = start;
        TNumber end = start + count;
        
        while (current != end + incrementor)
        {
            _source.Add(current);
            
            current += incrementor;
        }
    }
    
    public IAsyncEnumerator<TNumber> GetAsyncEnumerator(CancellationToken cancellationToken = new())
    {
        return new AsyncNumberRangeEnumerator<TNumber>(_source.ToAsyncEnumerable());
    }
}
#endif