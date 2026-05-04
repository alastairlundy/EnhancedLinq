/*
    EnhancedLinq.Async
    Copyright (c) 2025-2026 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
*/

using System.Linq;

namespace EnhancedLinq.Async.Deferred.Enumerators.SplitBy;

internal class SplitByPredicateAsyncEnumerator<T> : IAsyncEnumerator<IAsyncEnumerable<T>>
{
    private readonly Func<T, bool> _predicate;

    private readonly IAsyncEnumerator<T> _enumerator;

    private int _state;
    private IAsyncEnumerable<T> _current;

    internal SplitByPredicateAsyncEnumerator(IAsyncEnumerable<T> source, Func<T, bool> predicate)
    {
        _predicate = predicate;
        _state = 1;
        _enumerator =  source.GetAsyncEnumerator();
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
                List<T> tempList = new List<T>();

                while (await _enumerator.MoveNextAsync().ConfigureAwait(false))
                {
                    bool split = _predicate(_enumerator.Current);

                    if (!split)
                    {
                        tempList.Add(_enumerator.Current);
                    }
                    else
                    {
                        List<T> list = new(tempList);

                        _current = list.ToAsyncEnumerable();
                        tempList.Clear();
                        return true;
                    }
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

    public IAsyncEnumerable<T> Current => _current;


    public async ValueTask DisposeAsync()
    {
        await _enumerator.DisposeAsync().ConfigureAwait(false);
    }
}