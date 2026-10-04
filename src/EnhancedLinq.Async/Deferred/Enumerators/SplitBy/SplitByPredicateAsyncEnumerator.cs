using System.Linq;

namespace EnhancedLinq.Async.Deferred.Enumerators.SplitBy;

internal class SplitByPredicateAsyncEnumerator<T> : IAsyncEnumerator<IAsyncEnumerable<T>>
{
    private readonly Func<T, bool> _predicate;

    private readonly IAsyncEnumerator<T> _enumerator;

    private int _state;
    private IAsyncEnumerable<T> _current;

    internal SplitByPredicateAsyncEnumerator(IAsyncEnumerable<T> source, Func<T, bool> predicate)
    {
        _predicate = predicate;
        _state = 1;
        _enumerator =  source.GetAsyncEnumerator();

        _current = new List<T>().ToAsyncEnumerable();
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
            List<T> tempList = [];

            while (await _enumerator.MoveNextAsync().ConfigureAwait(false))
            {
                bool split = _predicate(_enumerator.Current);

                if (!split)
                {
                    tempList.Add(_enumerator.Current);
                }
                else
                {
                    List<T> list = new(tempList);

                    _current = list.ToAsyncEnumerable();
                    return true;
                }
            }

            if (tempList.Count > 0)
            {
                _current = new List<T>(tempList).ToAsyncEnumerable();
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

    public IAsyncEnumerable<T> Current => _current;


    public async ValueTask DisposeAsync()
    {
        await _enumerator.DisposeAsync().ConfigureAwait(false);
    }
}
