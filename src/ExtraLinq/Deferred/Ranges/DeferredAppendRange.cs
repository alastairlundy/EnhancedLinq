using System;
using System.Collections.Generic;
using ExtraLinq.Deferred.Enumerables;

namespace ExtraLinq.Deferred.Ranges;

public static partial class EnumerableLinqExtra
{
    
    /// <summary>
    /// Appends one sequence of elements to the end of another specified sequence.
    /// </summary>
    /// <param name="source">The sequence to add items to.</param>
    /// <param name="toBeAppended">The elements to add to the sequence.</param>
    /// <typeparam name="TSource">The type of element in the sequence and elements being added.</typeparam>
    public static IEnumerable<TSource> AppendRange<TSource>(this IEnumerable<TSource> source,
        IEnumerable<TSource> toBeAppended)
    { 
        ArgumentNullException.ThrowIfNull(source, nameof(source));
        ArgumentNullException.ThrowIfNull(toBeAppended, nameof(toBeAppended));
        
        return new AppendRangeEnumerable<TSource>(source, toBeAppended);
    }
    
    
}