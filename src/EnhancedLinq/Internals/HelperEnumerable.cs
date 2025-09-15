using System.Collections;
using System.Collections.Generic;

namespace AlastairLundy.EnhancedLinq.Internals;

internal class HelperEnumerable<T> : IEnumerable<T>
{
    private readonly IEnumerator<T> _enumerator;

    internal HelperEnumerable(IEnumerator<T> enumerator)
    {
        _enumerator = enumerator;
    }
    
    public IEnumerator<T> GetEnumerator()
    {
        return _enumerator;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}