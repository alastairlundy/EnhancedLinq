using System.Linq;
using System.Threading;

namespace EnhancedLinq.Async.Deferred.Enumerators.SplitBy;

internal class SplitByEnumerableCountAsyncEnumerator<T> : IAsyncEnumerator<IAsyncEnumerable<T>>
{
    private readonly IAsyncEnumerable<T> _source;
    private readonly int _maxEnumerableCount;
    private readonly CancellationToken _cancellationToken;

    private SplitByItemCountAsyncEnumerator<T>? _enumerator;
    private bool _initialized;
    private bool _empty;
    private int _state = 1;

    public SplitByEnumerableCountAsyncEnumerator(IAsyncEnumerable<T> source, int maxEnumerableCount, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(maxEnumerableCount);

        _source = source;
        _maxEnumerableCount = maxEnumerableCount;
        _cancellationToken = cancellationToken;
    }

    public void Reset()
    {
        throw new NotSupportedException();
    }

    public async ValueTask DisposeAsync()
    {
        if (_enumerator is not null)
        {
            await _enumerator.DisposeAsync().ConfigureAwait(false);
        }
    }

    public async ValueTask<bool> MoveNextAsync()
    {
        if (_state != 1)
        {
            return false;
        }

        _cancellationToken.ThrowIfCancellationRequested();

        if (!_initialized)
        {
            _initialized = true;

            T[] array = await _source.ToArrayAsync().ConfigureAwait(false);

            if (array.Length == 0)
            {
                _empty = true;
                _state = -1;
                return false;
            }

            int maxItemCount = (int)Math.Ceiling((double)array.Length / _maxEnumerableCount);
            if (maxItemCount <= 0)
            {
                maxItemCount = 1;
            }

            _enumerator = new SplitByItemCountAsyncEnumerator<T>(array.ToAsyncEnumerable(), maxItemCount, _maxEnumerableCount, _cancellationToken);
        }

        if (_enumerator is null || _empty)
        {
            return false;
        }

        try
        {
            if (await _enumerator.MoveNextAsync().ConfigureAwait(false))
            {
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

    public IAsyncEnumerable<T> Current => _enumerator?.Current ?? Array.Empty<T>().ToAsyncEnumerable();
}
