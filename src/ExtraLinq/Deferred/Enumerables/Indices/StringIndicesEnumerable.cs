using System.Collections;
using System.Collections.Generic;
using ExtraLinq.Deferred.Enumerators.Indices;

namespace ExtraLinq.Deferred.Enumerables;

internal class StringIndicesEnumerable : IEnumerable<int>
{
    private readonly string _str;
    private readonly char[] _charArray;

    public StringIndicesEnumerable(string str, char[] charArray)
    {
        _str = str;
        _charArray = charArray;
    }
    
    public IEnumerator<int> GetEnumerator()
    {
        return new StringIndicesEnumerator(_str, _charArray);
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}