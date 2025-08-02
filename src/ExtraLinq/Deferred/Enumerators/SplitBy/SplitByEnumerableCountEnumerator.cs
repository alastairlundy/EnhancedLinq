using System.Collections;
using System.Collections.Generic;

namespace ExtraLinq.Deferred.Enumerators;

internal class SplitByEnumerableCountEnumerator<T> : IEnumerator<IEnumerable<T>>
{
    private readonly List<T> _source;
    private readonly int _maxEnumerableCount;
    
    private IEnumerable<T> _current;

    public SplitByEnumerableCountEnumerator(IEnumerable<T> source, int maxEnumerableCount)
    {
        _source = new List<T>(source);
        _maxEnumerableCount = maxEnumerableCount;
    }


    public bool MoveNext()
    {
        throw new System.NotImplementedException();
    }

    public void Reset()
    {
        throw new System.NotImplementedException();
    }

    IEnumerable<T> IEnumerator<IEnumerable<T>>.Current => _current;

    object? IEnumerator.Current => _current;

    public void Dispose()
    {
        throw new System.NotImplementedException();
    }
}