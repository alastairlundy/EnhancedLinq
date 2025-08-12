using System;
using System.Collections.Generic;
using System.Numerics;
using ExtraLinq.Deferred.Enumerables.IntegerRanges;

namespace ExtraLinq.Deferred;

public static class DeferredIntegerRange
{
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="start"></param>
    /// <param name="count"></param>
    /// <typeparam name="TNumber"></typeparam>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="NotFiniteNumberException"></exception>
    public static IEnumerable<TNumber> IntegerRange<TNumber>(this TNumber start, TNumber count)
        where TNumber : INumber<TNumber>
    {
        if (TNumber.IsNaN(start) || TNumber.IsNaN(count))
            throw new ArgumentException();

        if (TNumber.IsInfinity(start) || TNumber.IsInfinity(count))
            throw new NotFiniteNumberException();
        
        return new IntegerRangeEnumerable<TNumber>(start, count);
    }
}