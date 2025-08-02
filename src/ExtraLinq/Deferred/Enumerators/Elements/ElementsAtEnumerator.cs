using System;
using System.Collections;
using System.Collections.Generic;

using ExtraLinq.Immediate;

namespace ExtraLinq.Deferred.Enumerators;

internal class ElementsAtEnumerator<TSource> : IEnumerator<TSource>
{
    private IEnumerator<int> _indicesEnumerator;
    
    private readonly IEnumerable<TSource> _source;
    private readonly IEnumerable<int> _indices;

    private int _state;

    private int _index;
    
    private TSource _current;
    
    internal ElementsAtEnumerator(IEnumerable<TSource> source, IEnumerable<int> indices)
    {
        _source = source;
        _indices = indices;
        _state = 1;
        _index = 0;
    }

    public bool MoveNext()
    {
        if (_state == 1)
        {
            _indicesEnumerator = _indices.GetEnumerator();
            
            _state = 2;
        }

        if (_state == 2)
        {
            try
            {
                if(_indicesEnumerator.MoveNext())
                {
                    _current = _source.ElementAt(_indicesEnumerator.Current);
                    return true;
                }

                return false;
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

    public TSource Current => _current;

    object? IEnumerator.Current => _current;

    public void Dispose()
    {
        _indicesEnumerator?.Dispose();
    }
}