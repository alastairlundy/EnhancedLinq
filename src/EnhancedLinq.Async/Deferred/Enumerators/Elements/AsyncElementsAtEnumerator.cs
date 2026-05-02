/*
    EnhancedLinq.Async
    Copyright (c) 2025-2026 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/.
*/

using System.Linq;
using EnhancedLinq.Async.Immediate;

namespace EnhancedLinq.Async.Deferred.Enumerators;

internal class AsyncElementsAtEnumerator<TSource> : IAsyncEnumerator<TSource>
{
    private readonly IAsyncEnumerator<TSource> _enumerator;

    private int _state;

    internal AsyncElementsAtEnumerator(IAsyncEnumerable<TSource> source, IAsyncEnumerable<int> indices)
    {
        _state = 1;

        IAsyncEnumerable<TSource> values = indices.
            ForEachAsync(async i => await source.ElementAtAsync(i).ConfigureAwait(false));

        _enumerator = values.GetAsyncEnumerator();
        Current = _enumerator.Current;
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
                if (await _enumerator.MoveNextAsync().ConfigureAwait(false))
                {
                    Current = _enumerator.Current;
                    return true;
                }

                return false;
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

    public TSource Current { get; private set; }

    public async ValueTask DisposeAsync()
    {
        await _enumerator.DisposeAsync().ConfigureAwait(false);
    }
}