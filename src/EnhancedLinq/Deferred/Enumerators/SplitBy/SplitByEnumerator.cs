using System;
using System.Collections;
using System.Collections.Generic;

namespace AlastairLundy.EnhancedLinq.Deferred.Enumerators;

internal class SplitByEnumerator<T> : IEnumerator<IEnumerable<T>>
{
    private readonly IEnumerable<T> _source;
    private readonly Func<T, bool> _predicate;

    private IEnumerator<T> _enumerator;

    private List<T> _items;
    
    private IEnumerable<T> _current;
    
    private int _state;
    
    internal SplitByEnumerator(IEnumerable<T> source, Func<T, bool> predicate)
    {
        _source = source;
        _predicate = predicate;
        _state = 1;
    }
    
    public bool MoveNext()
    {
        if (_state == 1)
        {
            _enumerator =  _source.GetEnumerator();    
            _state = 2;
        }
        if (_state == 2)
        {
            
        }
        
        throw new System.NotImplementedException();
    }

    public void Reset()
    {
        throw new NotSupportedException();
    }

    public IEnumerable<T> Current => _current;

    object? IEnumerator.Current => _current;

    public void Dispose()
    {
        _enumerator?.Dispose();
    }
}