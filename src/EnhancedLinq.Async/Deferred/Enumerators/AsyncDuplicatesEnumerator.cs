namespace EnhancedLinq.Async.Deferred.Enumerators;

internal class AsyncDuplicatesEnumerator<TSource> : IAsyncEnumerator<TSource>
{
    private readonly IAsyncEnumerator<TSource> _enumerator;

    private int _state = 1;

    private readonly HashSet<TSource> _hashSet;

    public AsyncDuplicatesEnumerator(IAsyncEnumerable<TSource> source, IEqualityComparer<TSource> comparer)
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