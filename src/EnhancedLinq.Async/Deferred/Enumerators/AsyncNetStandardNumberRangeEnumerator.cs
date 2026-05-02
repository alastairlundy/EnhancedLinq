/*
    EnhancedLinq.Async
    Copyright (c) 2025-2026 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/.
*/

namespace EnhancedLinq.Async.Deferred.Enumerators;

internal class AsyncNetStandardNumberRangeEnumerator : IAsyncEnumerator<int>
{
    private readonly IAsyncEnumerator<int> _enumerator;
    
    private int _state;

    internal AsyncNetStandardNumberRangeEnumerator(IAsyncEnumerable<int> source)
    {
        Current = 0;
        _state = 0;
        _enumerator = source.GetAsyncEnumerator();
    }
    
    public async ValueTask<bool> MoveNextAsync()
    {
        if (_state == 1)
        {
            try
            {
                while (await _enumerator.MoveNextAsync()
                           .ConfigureAwait(false))
                {
                    Current = _enumerator.Current;
                    return true;
                }
            }
            catch
            {
                await DisposeAsync().ConfigureAwait(false);
                throw;
            }
            finally
            {
                _state = -1;
            }
        }

        await DisposeAsync().ConfigureAwait(false);
        return false;
    }

    public int Current { get; private set; }

    public async ValueTask DisposeAsync()
    {
        if (_enumerator is IAsyncDisposable enumeratorAsyncDisposable)
            await enumeratorAsyncDisposable.DisposeAsync().ConfigureAwait(false);
        else
            await _enumerator.DisposeAsync().ConfigureAwait(false);
    }
}