/*
    EnhancedLinq.Async
    Copyright (c) 2025-2026 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/.
*/

namespace EnhancedLinq.Async.Deferred.Enumerators;

internal class AsyncDuplicatesEnumerator<TSource> : IAsyncEnumerator<TSource>
{
    private readonly IAsyncEnumerator<TSource> _enumerator;

    private int _state = 1;

    private readonly HashSet<TSource> _hashSet;

    internal AsyncDuplicatesEnumerator(IAsyncEnumerable<TSource> source, IEqualityComparer<TSource> comparer)
    {
        _hashSet = new HashSet<TSource>(comparer);
        
        _enumerator = source.GetAsyncEnumerator();
        Current = _enumerator.Current;
    }
    
    public async ValueTask DisposeAsync()
    {
        await _enumerator.DisposeAsync().ConfigureAwait(false);
    }

    public async ValueTask<bool> MoveNextAsync()
    {
        if (_state == 1)
        {
            while (await _enumerator.MoveNextAsync().ConfigureAwait(false))
            {
                bool isDuplicate = _hashSet.Add(_enumerator.Current);

                if (isDuplicate)
                {
                    Current = _enumerator.Current;
                    return true;
                }
            }

            _state = 2;
        }
            
        await DisposeAsync().ConfigureAwait(false);
        return false;
    }

    public TSource Current { get; private set; }
}