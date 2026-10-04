using System.Threading;

namespace EnhancedLinq.Async.Deferred.Enumerators;

internal class AsyncDuplicatesEnumerator<TSource> : IAsyncEnumerator<TSource>
{
    private readonly IAsyncEnumerator<TSource> _enumerator;

    private int _state = 1;

    private readonly HashSet<TSource> _hashSet;

    internal AsyncDuplicatesEnumerator(IAsyncEnumerable<TSource> source, IEqualityComparer<TSource> comparer, CancellationToken cancellationToken = default)
    {
        _hashSet = new HashSet<TSource>(comparer);
        
        _enumerator = source.GetAsyncEnumerator(cancellationToken);
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
                bool added = _hashSet.Add(_enumerator.Current);

                if (!added)
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

    public TSource Current { get; private set; } = default!;
}
