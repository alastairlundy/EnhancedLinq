/*
    EnhancedLinq.Async
    Copyright (c) 2025-2026 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/.
*/

namespace EnhancedLinq.Async.Deferred.Enumerators;

internal class GenericIndicesAsyncEnumerator<TSource> : IAsyncEnumerator<int>
{
    private readonly Func<TSource, bool> _predicate;

    private readonly IAsyncEnumerator<TSource> _enumerator;

    private int _state;
    private int _index;
    
    internal GenericIndicesAsyncEnumerator(IAsyncEnumerable<TSource> source, Func<TSource, bool> predicate)
    {
        _predicate = predicate;
        _enumerator = source.GetAsyncEnumerator();
        _state = 1;
        _index = 0;
    }
    
    public void Reset()
    {
        throw new NotSupportedException();
    }

    public async ValueTask<bool> MoveNextAsync()
    {
        if (_state == 1)
        {
            try
            {
                while (await _enumerator.MoveNextAsync())
                {
                    if (_predicate(_enumerator.Current))
                    {
                        Current = _index;
                        return true;
                    }

                    _index++;
                }
            }
            finally
            {
                await DisposeAsync();
                _state = -1;
            }
        }
        
        await DisposeAsync();
        return false;
    }

    public int Current { get; private set; }

    public async ValueTask DisposeAsync()
    {
        await _enumerator.DisposeAsync().ConfigureAwait(false);
    }
}