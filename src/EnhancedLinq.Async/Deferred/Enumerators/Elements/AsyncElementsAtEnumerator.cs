using System.Linq;
using System.Threading;
using EnhancedLinq.Async.Immediate;

namespace EnhancedLinq.Async.Deferred.Enumerators;

internal class AsyncElementsAtEnumerator<TSource> : IAsyncEnumerator<TSource>
{
    private readonly IAsyncEnumerator<TSource> _enumerator;

    private int _state;

    internal AsyncElementsAtEnumerator(IAsyncEnumerable<TSource> source, IAsyncEnumerable<int> indices, CancellationToken cancellationToken = default)
    {
        _state = 1;

        IAsyncEnumerable<TSource> values = indices.
            ForEachAsync(async i => await source.ElementAtAsync(i).ConfigureAwait(false));

        _enumerator = values.GetAsyncEnumerator(cancellationToken);
    }
    

    public void Reset()
    {
        throw new NotSupportedException();
    }

    public async ValueTask<bool> MoveNextAsync()
    {
        if (_state != 1)
        {
            return false;
        }

        try
        {
            if (await _enumerator.MoveNextAsync().ConfigureAwait(false))
            {
                Current = _enumerator.Current;
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

    public TSource Current { get; private set; } = default!;

    public async ValueTask DisposeAsync()
    {
        await _enumerator.DisposeAsync().ConfigureAwait(false);
    }
}
