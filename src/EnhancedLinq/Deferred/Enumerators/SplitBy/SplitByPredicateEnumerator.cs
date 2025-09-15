using System;
using System.Collections;
using System.Collections.Generic;

namespace AlastairLundy.EnhancedLinq.Deferred.Enumerators;

internal class SplitByPredicateEnumerator<T> : IEnumerator<IEnumerable<T>>
{
    private readonly IEnumerable<T> _source;
    private readonly Func<T, bool> _predicate;

    private IEnumerator<T> _enumerator;
    
    private IEnumerable<T> _current;
    
    private int _state;
    
    internal SplitByPredicateEnumerator(IEnumerable<T> source, Func<T, bool> predicate)
    {
        _predicate = predicate;
        _source = source;
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
            try
            {
                List<T> tempList = new List<T>();
                
                while(_enumerator.MoveNext())
                {
                    bool split = _predicate(_enumerator.Current);
                    
                    if (split == false)
                    {
                        tempList.Add(_enumerator.Current);
                    }
                    else
                    {
                        _current = new List<T>(tempList);
                        tempList.Clear();
                        return true;
                    }
                }
            }
            catch
            {
                Dispose();
                throw;
            }
        }

        Dispose();
        _state = -1;
        return false;
    }

    public void Reset()
    {
       throw new NotSupportedException();
    }

    public IEnumerable<T> Current => _current;

    object? IEnumerator.Current => Current;

    public void Dispose()
    {
        _enumerator?.Dispose();
    }
}