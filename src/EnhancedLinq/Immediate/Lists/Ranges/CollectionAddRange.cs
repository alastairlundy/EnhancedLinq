

using System;
using System.Collections.Generic;

namespace AlastairLundy.EnhancedLinq.Immediate.Ranges;

public static partial class EnhancedLinqImmediateRange
{
    /// <summary>
    /// Appends elements from another collection to the end of the specified collection.
    /// </summary>
    /// <param name="source">The collection into which elements will be appended.</param>
    /// <param name="enumerableToAdd">The IEnumerable containing elements to append to the original collection.</param>
    /// <typeparam name="T">The type of elements in both collections.</typeparam>
    /// <exception cref="NotSupportedException">Thrown if adding to the collection is not supported.</exception>
    public static void AddRange<T>(this ICollection<T> source, IEnumerable<T> enumerableToAdd)
    {
#if NET8_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(enumerableToAdd);
#endif

        if (source.IsReadOnly)
            throw new NotSupportedException();
        
        if (source.Count == int.MaxValue)
        {
            throw new OverflowException($"{nameof(source)} contains the maximum size of {int.MaxValue} and cannot be added to.");
        }
        
        foreach (T item in enumerableToAdd)
        {
            source.Add(item);
        }
    }
        
    /// <summary>
    /// Appends elements from another collection to the end of the specified collection.
    /// </summary>
    /// <param name="source">The collection into which elements will be appended.</param>
    /// <param name="collectionToAdd">The collection containing elements to append to the original collection.</param>
    /// <typeparam name="T">The type of elements in both collections.</typeparam>
    public static void AddRange<T>(this ICollection<T> source, ICollection<T> collectionToAdd)
    {
#if NET8_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(collectionToAdd);
#endif        

        if (source.IsReadOnly)
            throw new NotSupportedException();
        
        if (source.Count == int.MaxValue)
        {
            throw new OverflowException($"{nameof(source)} contains the maximum size of {int.MaxValue} and cannot be added to.");
        }
        else if (collectionToAdd.Count == int.MaxValue)
        {
            throw new OverflowException($"{nameof(collectionToAdd)} contains the maximum size of {int.MaxValue} and cannot be added to {nameof(source)}.");
        }
        
        if (source is List<T> { Count: 0 })
        {
            source = new List<T>(capacity: collectionToAdd.Count);
        }
        
        foreach (T item in collectionToAdd)
        {
            source.Add(item);
        }
    }
}