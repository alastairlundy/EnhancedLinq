using System.Threading;

namespace EnhancedLinq.Async.Deferred.Enumerators;

internal class AsyncNetStandardNumberRangeEnumerator : IAsyncEnumerator<int>
{
    private readonly int _incrementor;
    private readonly CancellationToken _cancellationToken;

    private int _next;
    private int _remaining;

    internal AsyncNetStandardNumberRangeEnumerator(int start, int count, int incrementor, CancellationToken cancellationToken = default)
    {
        _incrementor = incrementor;
        _cancellationToken = cancellationToken;
        _next = start;
        _remaining = count;
        Current = 0;
    }

    public ValueTask<bool> MoveNextAsync()
    {
        _cancellationToken.ThrowIfCancellationRequested();

        if (_remaining <= 0)
        {
            return new ValueTask<bool>(false);
        }

        Current = _next;
        _next += _incrementor;
        _remaining--;
        return new ValueTask<bool>(true);
    }

    public int Current { get; private set; }

    public ValueTask DisposeAsync()
    {
        _remaining = 0;
        return ValueTask.CompletedTask;
    }
}
