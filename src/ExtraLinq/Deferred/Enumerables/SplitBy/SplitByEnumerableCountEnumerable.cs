using System.Collections;
using System.Collections.Generic;
using ExtraLinq.Deferred.Enumerators;

namespace ExtraLinq.Deferred.Enumerables;

internal class SplitByEnumerableCountEnumerable<T> : IEnumerable<IEnumerable<T>>
{
    private readonly IEnumerable<T> _source;
    private readonly int _maxEnumerableCount;

    public SplitByEnumerableCountEnumerable(IEnumerable<T> source, int maxEnumerableCount)
    {
        _source = source;
        _maxEnumerableCount = maxEnumerableCount;
    }

    public IEnumerator<IEnumerable<T>> GetEnumerator()
    {
        return new SplitByEnumerableCountEnumerator<T>(_source, _maxEnumerableCount);
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}