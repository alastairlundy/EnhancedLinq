using System.Threading;

namespace EnhancedLinq.Async.Internals;

internal class CustomAsyncEnumerable<TSource> : IAsyncEnumerable<TSource>, IAsyncDisposable
{
    private readonly Func<CancellationToken, IAsyncEnumerator<TSource>> _factory;

    internal CustomAsyncEnumerable(Func<IAsyncEnumerator<TSource>> factory)
    {
        ArgumentNullException.ThrowIfNull(factory);
        _factory = _ => factory();
    }

    internal CustomAsyncEnumerable(Func<CancellationToken, IAsyncEnumerator<TSource>> factory)
    {
        ArgumentNullException.ThrowIfNull(factory);
        _factory = factory;
    }

    public async IAsyncEnumerator<TSource> GetAsyncEnumerator(CancellationToken cancellationToken = default)
    {
        IAsyncEnumerator<TSource> enumerator = _factory(cancellationToken);

        try
        {
            while (true)
            {
                cancellationToken.ThrowIfCancellationRequested();

                if (!await enumerator.MoveNextAsync().ConfigureAwait(false))
                {
                    break;
                }

                cancellationToken.ThrowIfCancellationRequested();
                yield return enumerator.Current;
            }
        }
        finally
        {
            await enumerator.DisposeAsync().ConfigureAwait(false);
        }
    }

    public ValueTask DisposeAsync()
    {
        return ValueTask.CompletedTask;
    }
}
