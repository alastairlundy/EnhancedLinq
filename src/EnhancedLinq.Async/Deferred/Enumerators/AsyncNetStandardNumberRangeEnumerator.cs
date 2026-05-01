namespace EnhancedLinq.Async.Deferred.Enumerators;

internal class AsyncNetStandardNumberRangeEnumerator : IAsyncEnumerator<int>
{
    private readonly IAsyncEnumerator<int> _enumerator;
    
    private int _state;

    internal AsyncNetStandardNumberRangeEnumerator(IAsyncEnumerable<int> source)
    {
        Current = 0;
        _state = 0;
        _enumerator = source.GetAsyncEnumerator();
    }
    
    public async ValueTask<bool> MoveNextAsync()
    {
        if (_state == 1)
        {
            try
            {
                while (await _enumerator.MoveNextAsync()
                           .ConfigureAwait(false))
                {
                    Current = _enumerator.Current;
                    return true;
                }
            }
            catch
            {
                await DisposeAsync().ConfigureAwait(false);
                throw;
            }
            finally
            {
                _state = -1;
            }
        }

        await DisposeAsync().ConfigureAwait(false);
        return false;
    }

    public int Current { get; private set; }

    public async ValueTask DisposeAsync()
    {
        if (_enumerator is IAsyncDisposable enumeratorAsyncDisposable)
            await enumeratorAsyncDisposable.DisposeAsync().ConfigureAwait(false);
        else
            await _enumerator.DisposeAsync().ConfigureAwait(false);
    }
}