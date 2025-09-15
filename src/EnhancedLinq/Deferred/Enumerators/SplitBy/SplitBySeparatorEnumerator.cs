using System;
using System.Collections;
using System.Collections.Generic;

namespace AlastairLundy.EnhancedLinq.Deferred.Enumerators;

internal class SplitBySeparatorEnumerator<T> : IEnumerator<IEnumerable<T>>
{
    private readonly IEnumerable<T> _source;
    private readonly T _separator;

    private IEnumerator<T> _enumerator;
    
    private IEnumerable<T> _current;
    
    private int _state;
    
    internal SplitBySeparatorEnumerator(IEnumerable<T> source, T separator)
    {
        _source = source;
        _separator = separator;
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
                    bool split = false;
                    
                    if(_enumerator.Current is not null)
                        split = _enumerator.Current.Equals(_separator);
                    
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

    object? IEnumerator.Current => _current;

    public void Dispose()
    {
        _enumerator?.Dispose();
    }
}