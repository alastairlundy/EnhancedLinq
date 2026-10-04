#if NET8_0_OR_GREATER
using System.Numerics;
using System.Threading;

namespace EnhancedLinq.Async.Deferred.Enumerators;

internal class AsyncNumberRangeEnumerator<TNumber> : IAsyncEnumerator<TNumber> where TNumber : INumber<TNumber>
{
    private readonly TNumber _incrementor;
    private readonly CancellationToken _cancellationToken;

    private TNumber _next;
    private TNumber _remaining;

    internal AsyncNumberRangeEnumerator(TNumber start, TNumber count, TNumber incrementor, CancellationToken cancellationToken = default)
    {
        _incrementor = incrementor;
        _cancellationToken = cancellationToken;
        _next = start;
        _remaining = count;
        Current = TNumber.Zero;
    }

    public ValueTask<bool> MoveNextAsync()
    {
        _cancellationToken.ThrowIfCancellationRequested();

        if (_remaining <= TNumber.Zero)
        {
            return new ValueTask<bool>(false);
        }

        Current = _next;
        _next += _incrementor;
        _remaining -= TNumber.One;
        return new ValueTask<bool>(true);
    }

    public TNumber Current { get; private set; }

    public ValueTask DisposeAsync()
    {
        _remaining = TNumber.Zero;
        return ValueTask.CompletedTask;
    }
}
#endif
