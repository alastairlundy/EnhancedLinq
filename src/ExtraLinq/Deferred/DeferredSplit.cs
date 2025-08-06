using System;
using System.Collections.Generic;
using ExtraLinq.Deferred.Enumerables;

namespace ExtraLinq.Deferred;

public static partial class EnumerableLinqExtra
{
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="maximumCount"></param>
    /// <typeparam name="TSource"></typeparam>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public static IEnumerable<IEnumerable<TSource>> SplitByCount<TSource>(this IEnumerable<TSource> source, int maximumCount)
    {
#if NET8_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(source, nameof(source));
#else
        if(source == null)
            throw new ArgumentNullException(nameof(source));
#endif
        
        if(maximumCount <= 0)
            throw new ArgumentOutOfRangeException(nameof(maximumCount));

        return new SplitByItemCountEnumerable<TSource>(source, maximumItemCount: maximumCount);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <typeparam name="TSource"></typeparam>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IEnumerable<IEnumerable<TSource>> SplitByProcessorCount<TSource>(this IEnumerable<TSource> source)
    {
#if NET8_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(source, nameof(source));
#else
        if(source == null)
            throw new ArgumentNullException(nameof(source));
#endif  
        
        return new SplitByEnumerableCountEnumerable<TSource>(source, Environment.ProcessorCount);
    }
}