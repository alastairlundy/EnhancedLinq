using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using ExtraLinq.Deferred.Enumerators.NumberRanges;

namespace ExtraLinq.Deferred.Enumerables.NumberRanges;

internal class NumberRangeEnumerable<TNumber> : IEnumerable<TNumber> where TNumber : INumber<TNumber>
{
    private List<TNumber> _source;
    
    internal NumberRangeEnumerable(TNumber start, TNumber count)
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
        return new NumberRangeEnumerator<TNumber>(_source);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}