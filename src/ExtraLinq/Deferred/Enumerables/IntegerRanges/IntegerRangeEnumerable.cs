using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using ExtraLinq.Deferred.Enumerators.IntegerRanges;

namespace ExtraLinq.Deferred.Enumerables.IntegerRanges;

internal class IntegerRangeEnumerable<TNumber> : IEnumerable<TNumber> where TNumber : INumber<TNumber>
{
    private List<TNumber> _source;
    
    internal IntegerRangeEnumerable(TNumber start, TNumber count)
    {
        _source = new List<TNumber>();
        
        TNumber current = start;
        TNumber end = start + count;
        
        while (current != end + TNumber.One)
        {
            _source.Add(current);
            
            current += TNumber.Zero;
        }
    }
    
    public IEnumerator<TNumber> GetEnumerator()
    {
        return new IntegerRangeEnumerator<TNumber>(_source);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}