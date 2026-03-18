using System.Threading;

namespace EnhancedLinq.Async.Internals;

internal class CustomAsyncEnumerable<TSource> : IAsyncEnumerable<TSource>, IAsyncDisposable
{
    private readonly IAsyncEnumerator<TSource> _enumerator;

    internal CustomAsyncEnumerable(IAsyncEnumerator<TSource> enumerator)
    {
        _enumerator = enumerator;
    }
    
    public async IAsyncEnumerator<TSource> GetAsyncEnumerator(CancellationToken cancellationToken = default)
    {
        while (await _enumerator.MoveNextAsync().ConfigureAwait(false))
        {
            if (cancellationToken.IsCancellationRequested)
                yield break;
            
            yield return _enumerator.Current;
        }
    }

    public async ValueTask DisposeAsync()
    {
        await _enumerator.DisposeAsync().ConfigureAwait(false);
    }
}