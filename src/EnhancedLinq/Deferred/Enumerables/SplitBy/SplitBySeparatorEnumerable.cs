using System;
using System.Collections;
using System.Collections.Generic;

using AlastairLundy.EnhancedLinq.Deferred.Enumerators;

namespace AlastairLundy.EnhancedLinq.Deferred.Enumerables;

internal class SplitBySeparatorEnumerable<T> : IEnumerable<IEnumerable<T>>
{
    private readonly IEnumerable<T> _source;
    private readonly T _separator;

    internal SplitBySeparatorEnumerable(IEnumerable<T> source, T separator)
    {
        _source = source;
        _separator = separator;
    }
    
    public IEnumerator<IEnumerable<T>> GetEnumerator()
    {
        return new SplitBySeparatorEnumerator<T>(_source, _separator);
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}