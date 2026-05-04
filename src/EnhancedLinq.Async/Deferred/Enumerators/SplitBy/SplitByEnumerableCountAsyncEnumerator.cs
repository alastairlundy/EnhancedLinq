/*
    EnhancedLinq.Async
    Copyright (c) 2025-2026 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
*/

#nullable disable
using System.Linq;

namespace EnhancedLinq.Async.Deferred.Enumerators.SplitBy;

internal class SplitByEnumerableCountAsyncEnumerator<T> : IAsyncEnumerator<IAsyncEnumerable<T>>
{
    private readonly SplitByItemCountAsyncEnumerator<T> _enumerator;
    
    public SplitByEnumerableCountAsyncEnumerator(IAsyncEnumerable<T> source, int maxEnumerableCount)
    {
        Task<T[]> arrayTask = source.ToArrayAsync().AsTask();
        arrayTask.Wait();
        
        int itemCountTask = arrayTask.Result.Length;
        
        double maxItems = Convert.ToDouble(itemCountTask / maxEnumerableCount);
        int maxItemCount;
        
        if (maxItems % 1 != 0)
        {
            maxItemCount = Convert.ToInt32(maxItems) + 1;
        }
        else
        {
            maxItemCount = Convert.ToInt32(maxItems);
        }
        
        _enumerator = new SplitByItemCountAsyncEnumerator<T>(arrayTask.Result.ToAsyncEnumerable(),
            maxItemCount, maxEnumerableCount);
    }

    public void Reset()
    {
        throw new NotSupportedException();
    }

    public async ValueTask<bool> MoveNextAsync()
    {
        return await _enumerator.MoveNextAsync().ConfigureAwait(false);
    }

    public IAsyncEnumerable<T> Current => _enumerator.Current;

    public async ValueTask DisposeAsync()
    {
        await _enumerator.DisposeAsync().ConfigureAwait(false);
    }
}