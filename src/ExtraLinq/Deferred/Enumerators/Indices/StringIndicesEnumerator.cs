using System.Collections;
using System.Collections.Generic;

namespace ExtraLinq.Deferred.Enumerators.Indices;

internal class StringIndicesEnumerator : IEnumerator<int>
{
    private int _current;

    public StringIndicesEnumerator(string str, char[] values)
    {
      
    }

    public bool MoveNext()
    {
        throw new System.NotImplementedException();
    }

    public void Reset()
    {
        throw new System.NotImplementedException();
    }

    int IEnumerator<int>.Current => _current;

    object? IEnumerator.Current => _current;

    public void Dispose()
    {
        throw new System.NotImplementedException();
    }
}