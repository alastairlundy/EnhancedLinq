using System.Threading;
using EnhancedLinq.Async.Deferred.Enumerators;

namespace EnhancedLinq.Async.Deferred;

internal class AsyncNumberRangeNetStandardEnumerable : IAsyncEnumerable<int>
{
    private readonly int _start;
    private readonly int _count;
    private readonly int _incrementor;

    internal AsyncNumberRangeNetStandardEnumerable(int start, int count, int incrementor)
    {
        _start = start;
        _count = count;
        _incrementor = incrementor;
    }

    public IAsyncEnumerator<int> GetAsyncEnumerator(CancellationToken cancellationToken = new())
    {
        return new AsyncNetStandardNumberRangeEnumerator(_start, _count, _incrementor, cancellationToken);
    }
}
