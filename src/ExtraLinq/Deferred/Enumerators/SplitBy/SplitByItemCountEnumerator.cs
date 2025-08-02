using System;
using System.Collections;
using System.Collections.Generic;

namespace ExtraLinq.Deferred.Enumerators;

internal class SplitByItemCountEnumerator<T> : IEnumerator<IEnumerable<T>>
{
    private IEnumerator<T> _enumerator;
    
    private readonly IEnumerable<T> _source;
    private readonly int _maximumItemCount;
    
    private List<T> _current;

    private int _currentItemCount;
    private int _currentEnumerableCount;
    
    private int _state;
    
    public SplitByItemCountEnumerator(IEnumerable<T> source, int maximumItemCount)
    {
        _source = source;
        _maximumItemCount = maximumItemCount;
        
        _state = 0;
        _currentItemCount = 0;
        _currentEnumerableCount = 0;

        if (maximumItemCount <= 0)
            throw new ArgumentOutOfRangeException(nameof(maximumItemCount));
    }

    public bool MoveNext()
    {
        if (_state == 1)
        {
            _enumerator = _source.GetEnumerator();
            _state = 2;
        }
        
        if (_state == 2)
        {
            try
            {
                List<T> tempList = new List<T>();
                
                while(_enumerator.MoveNext())
                {
                    if (_currentItemCount < _maximumItemCount)
                    {
                        tempList.Add(_enumerator.Current);
                        _currentItemCount++;
                    }
                    else
                    {
                        _current = new List<T>(tempList);
                        tempList.Clear();
                        _currentEnumerableCount++;
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

    IEnumerable<T> IEnumerator<IEnumerable<T>>.Current => _current;

    object? IEnumerator.Current => _current;

    public void Dispose()
    {
        _enumerator.Dispose();
    }
}