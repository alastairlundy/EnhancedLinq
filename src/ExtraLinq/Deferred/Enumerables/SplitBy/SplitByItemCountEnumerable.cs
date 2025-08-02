using System.Collections;
using System.Collections.Generic;
using ExtraLinq.Deferred.Enumerators;

namespace ExtraLinq.Deferred.Enumerables;

internal class SplitByItemCountEnumerable<T> : IEnumerable<IEnumerable<T>>
{
    private readonly IEnumerable<T> _source;
    private readonly int _maximumItemCount;

    public SplitByItemCountEnumerable(IEnumerable<T> source, int maximumItemCount)
    {
        _source = source;
        _maximumItemCount = maximumItemCount;
    }

    
    public IEnumerator<IEnumerable<T>> GetEnumerator()
    {
        return new SplitByItemCountEnumerator<T>(_source, _maximumItemCount);
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}