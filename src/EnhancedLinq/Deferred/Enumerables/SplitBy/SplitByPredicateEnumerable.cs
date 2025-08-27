using System;
using System.Collections;
using System.Collections.Generic;
using AlastairLundy.EnhancedLinq.Deferred.Enumerators;

namespace AlastairLundy.EnhancedLinq.Deferred.Enumerables;

internal class SplitByPredicateEnumerable<T> : IEnumerable<T>
{
    private readonly IEnumerable<T> _source;
    private readonly Func<T, bool> _predicate;

    internal SplitByPredicateEnumerable(IEnumerable<T> source, Func<T, bool> predicate)
    {
        _source = source;
        _predicate = predicate;
    }
    
    public IEnumerator<T> GetEnumerator()
    {
        return new SplitByEnumerator<T>(_source, _predicate);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}