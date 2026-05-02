/*
    EnhancedLinq.Async
    Copyright (c) 2025-2026 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/.
*/

using System.Linq;
using System.Threading;
using EnhancedLinq.Async.Deferred.Enumerators;

namespace EnhancedLinq.Async.Deferred;

internal class AsyncNumberRangeNetStandardEnumerable : IAsyncEnumerable<int>
{
    private readonly List<int> _source;
    
    internal AsyncNumberRangeNetStandardEnumerable(int start, int count, int incrementor)
    {
        _source = [];
        
        int current = start;
        int end = start + count;
        
        while (current != end + incrementor)
        {
            _source.Add(current);
            
            current += incrementor;
        }
    }
    
    public IAsyncEnumerator<int> GetAsyncEnumerator(CancellationToken cancellationToken = new())
    {
        return new AsyncNetStandardNumberRangeEnumerator(_source.ToAsyncEnumerable());
    }
}