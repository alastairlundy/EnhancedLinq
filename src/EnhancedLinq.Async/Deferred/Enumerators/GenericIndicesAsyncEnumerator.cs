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

    public async ValueTask<bool> MoveNextAsync()
    {
        if (_state != 1)
        {
            return false;
        }

        try
        {
            while (await _enumerator.MoveNextAsync().ConfigureAwait(false))
            {
                int currentIndex = _index;
                _index++;

                if (_predicate(_enumerator.Current))
                {
                    Current = currentIndex;
                    return true;
                }
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

    public int Current { get; private set; }

    public async ValueTask DisposeAsync()
    {
        await _enumerator.DisposeAsync().ConfigureAwait(false);
    }
}
