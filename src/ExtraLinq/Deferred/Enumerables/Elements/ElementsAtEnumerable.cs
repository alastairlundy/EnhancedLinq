using System.Collections;
using System.Collections.Generic;

using ExtraLinq.Deferred.Enumerators;

namespace ExtraLinq.Deferred.Enumerables;

internal class ElementsAtEnumerable<TSource> : IEnumerable<TSource>
{
    private readonly IEnumerable<TSource> _source;
    private readonly IEnumerable<int> _indices;
    
    internal ElementsAtEnumerable(IEnumerable<TSource> source, IEnumerable<int> indices)
    {
        _source = source;
        _indices = indices;
    }

    public IEnumerator<TSource> GetEnumerator()
    {
        return new ElementsAtEnumerator<TSource>(_source, _indices);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}