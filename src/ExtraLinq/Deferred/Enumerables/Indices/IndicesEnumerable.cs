using System;
using System.Collections;
using System.Collections.Generic;
using ExtraLinq.Deferred.Enumerators.Indices;

namespace ExtraLinq.Deferred.Enumerables;

internal class IndicesEnumerable<T> : IEnumerable<int>
{
    private readonly IEnumerable<T> _source;
    private readonly Func<T, bool> _predicate;

    public IndicesEnumerable(IEnumerable<T> source, Func<T, bool> predicate)
    {
        _source = source;
        _predicate = predicate;
    }

    public IEnumerator<int> GetEnumerator()
    {
        return new IndicesEnumerator<T>(_source, _predicate);
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}