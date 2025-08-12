using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;

namespace ExtraLinq.Deferred.Enumerators.IntegerRanges;

internal class IntegerRangeEnumerator<TNumber> : IEnumerator<TNumber> where TNumber : INumber<TNumber>
{
    private readonly IEnumerable<TNumber> _source;   
    private IEnumerator<TNumber> _enumerator;
    
    private TNumber _current;

    private int _state;

    internal IntegerRangeEnumerator(IEnumerable<TNumber> source)
    {
        _source = source;
        _current = TNumber.Zero;
        _state = 0;
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
                while(_enumerator.MoveNext())
                {
                    _current = _enumerator.Current;
                    return true;
                }
            }
            catch
            {
                Dispose();
                throw;
            }
        }

        _state = -1;
        Dispose();
        return false;
    }

    public void Reset()
    {
        throw new NotSupportedException();
    }

    TNumber IEnumerator<TNumber>.Current => _current;

    object? IEnumerator.Current => _current;

    public void Dispose()
    {
        _enumerator.Dispose();
    }
}