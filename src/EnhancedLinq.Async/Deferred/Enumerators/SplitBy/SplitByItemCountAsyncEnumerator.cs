using System.Linq;

namespace EnhancedLinq.Async.Deferred.Enumerators.SplitBy;

internal class SplitByItemCountAsyncEnumerator<T> : IAsyncEnumerator<IAsyncEnumerable<T>>
{
    private readonly IAsyncEnumerator<T> _enumerator;

    private readonly int _maximumItemCount;
    private readonly int _maxEnumerableCount;
    
    private List<T> _current;

    private int _currentEnumerableCount;
    
    private int _state;
    
    internal SplitByItemCountAsyncEnumerator(IAsyncEnumerable<T> source, int maximumItemCount)
    {
        _maximumItemCount = maximumItemCount;
        
        _state = 1;

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
        if (_state != 1)
        {
            return false;
        }

        if (_maxEnumerableCount != -1 && _currentEnumerableCount >= _maxEnumerableCount)
        {
            await DisposeAsync().ConfigureAwait(false);
            _state = -1;
            return false;
        }

        try
        {
            List<T> tempList = new List<T>();

            while (await _enumerator.MoveNextAsync()
                       .ConfigureAwait(false))
            {
                tempList.Add(_enumerator.Current);

                if (tempList.Count >= _maximumItemCount)
                {
                    _current = new List<T>(tempList);
                    _currentEnumerableCount++;
                    return true;
                }
            }

            if (tempList.Count > 0)
            {
                _current = new List<T>(tempList);
                _currentEnumerableCount++;
                return true;
            }
        }
        catch
        {
            await DisposeAsync().ConfigureAwait(false);
            _state = -1;
            throw;
        }
        
        await DisposeAsync().ConfigureAwait(false);
        _state = -1;
        return false;
    }

    public IAsyncEnumerable<T> Current => _current.ToAsyncEnumerable();
}
