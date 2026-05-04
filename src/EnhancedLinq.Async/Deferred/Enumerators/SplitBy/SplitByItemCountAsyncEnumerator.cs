/*
    EnhancedLinq.Async
    Copyright (c) 2025-2026 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
*/

using System.Linq;

namespace EnhancedLinq.Async.Deferred.Enumerators.SplitBy;

internal class SplitByItemCountAsyncEnumerator<T> : IAsyncEnumerator<IAsyncEnumerable<T>>
{
    private readonly IAsyncEnumerator<T> _enumerator;

    private readonly int _maximumItemCount;
    private readonly int _maxEnumerableCount;
    
    private List<T> _current;

    private int _currentEnumerableCount;
    private int _currentItemCount;
    
    private int _state;
    
    internal SplitByItemCountAsyncEnumerator(IAsyncEnumerable<T> source, int maximumItemCount)
    {
        _maximumItemCount = maximumItemCount;
        
        _state = 1;
        _currentItemCount = 0;

        if (maximumItemCount <= 0)
            throw new ArgumentOutOfRangeException(nameof(maximumItemCount));

        _maxEnumerableCount = -1;
        
        _enumerator = source.GetAsyncEnumerator();
        _current = [];
    }
    
    internal SplitByItemCountAsyncEnumerator(IAsyncEnumerable<T> source, int maximumItemCount, int maxEnumerableCount)
    {
        _maximumItemCount = maximumItemCount;
        
        _state = 1;
        _currentItemCount = 0;

        if (maximumItemCount <= 0)
            throw new ArgumentOutOfRangeException(nameof(maximumItemCount));
        
        if(maxEnumerableCount > 0)
            _maxEnumerableCount = maxEnumerableCount;
        else
            _maxEnumerableCount = -1;
        
        _enumerator = source.GetAsyncEnumerator();
        _current = [];
    }

    public void Reset()
    {
        throw new NotSupportedException();
    }


    public async ValueTask DisposeAsync()
    {
        await _enumerator.DisposeAsync().ConfigureAwait(false);
    }

    public async ValueTask<bool> MoveNextAsync()
    {
        if (_state == 1)
        {
            try
            {
                _currentEnumerableCount = 0;
                List<T> tempList = new List<T>();

                while (await _enumerator.MoveNextAsync()
                           .ConfigureAwait(false))
                {
                    if (_currentItemCount < _maximumItemCount)
                    {
                        tempList.Add(_enumerator.Current);
                        _currentItemCount++;
                    }
                    else if (_currentEnumerableCount < _maxEnumerableCount)
                    {
                        _current = new List<T>(tempList);
                        _currentEnumerableCount++;
                        tempList.Clear();
                        return true;
                    }
                    else
                    {
                        _current = new List<T>(tempList);
                        _currentEnumerableCount++;
                        tempList.Clear();
                        break;
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

    public IAsyncEnumerable<T> Current => _current.ToAsyncEnumerable();
}