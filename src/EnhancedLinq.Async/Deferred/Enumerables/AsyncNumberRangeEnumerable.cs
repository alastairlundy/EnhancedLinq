#if NET8_0_OR_GREATER
using System.Threading;

using System.Numerics;
using EnhancedLinq.Async.Deferred.Enumerators;

namespace EnhancedLinq.Async.Deferred;

internal class AsyncNumberRangeEnumerable<TNumber> : IAsyncEnumerable<TNumber> where TNumber : INumber<TNumber>
{
    private readonly TNumber _start;
    private readonly TNumber _count;
    private readonly TNumber _incrementor;

    internal AsyncNumberRangeEnumerable(TNumber start, TNumber count, TNumber incrementor)
    {
        _start = start;
        _count = count;
        _incrementor = incrementor;
    }

    public IAsyncEnumerator<TNumber> GetAsyncEnumerator(CancellationToken cancellationToken = new())
    {
        return new AsyncNumberRangeEnumerator<TNumber>(_start, _count, _incrementor, cancellationToken);
    }
}
#endif
