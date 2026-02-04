using System.Threading;

namespace EnhancedLinq.Async.Internals;

internal class CustomAsyncEnumerable<TSource> : IAsyncEnumerable<TSource>, IAsyncDisposable
{
    private readonly IAsyncEnumerator<TSource> _enumerator;

    internal CustomAsyncEnumerable(IAsyncEnumerator<TSource> enumerator)
    {
        _enumerator = enumerator;
    }
    
    public IAsyncEnumerator<TSource> GetAsyncEnumerator(CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public async ValueTask DisposeAsync()
    {
        await _enumerator.DisposeAsync();
    }
}