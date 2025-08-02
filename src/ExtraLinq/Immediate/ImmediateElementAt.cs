using System;
using System.Collections.Generic;
using ExtraLinq.Internals.Localizations;

namespace ExtraLinq.Immediate;

public static class ImmediateElementAt
{
    /// <summary>
    /// Retrieves the element at a specified index from the collection.
    /// </summary>
    /// <param name="source">The collection to retrieve the element from.</param>
    /// <param name="index">The zero-based index of the element to retrieve.</param>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <returns>The element at the specified index in the sequence, or throws an exception if no such element exists.</returns>
    /// <exception cref="ArgumentException">Thrown when no element is found at the specified index.</exception>
    public static T ElementAt<T>(this IEnumerable<T> source, int index)
    {
        if (source is IList<T> list)
        {
            return list[index];
        }
        
        int i = 0;

        foreach (T item in source)
        {
            if (i == index)
            {
                return item;
            }

            ++i;
        }

        throw new ArgumentException(Resources.Exceptions_ValueNotFound_AtIndex.Replace("{y}", nameof(source))
            .Replace("{x}",$"{index}"));
    }
    
    /// <summary>
    /// Returns a IList of elements from the specified source, 
    /// where the index of each element in the returned IList corresponds to an index in the provided indexes.
    /// </summary>
    /// <remarks>The order of the elements in the returned IList is determined by their original position in the source,
    /// but the order within the returned IList is based on the provided indexes.</remarks>
    /// <param name="source">The IList from which to retrieve elements.</param>
    /// <param name="indices">A sequence of indices, where each index corresponds to an element in the source.</param>
    /// <typeparam name="T">The type of the elements in the source and returned IList.</typeparam>
    /// <returns>A new IList containing the elements at the specified indexes from the original source.</returns>
    public static IList<T> ElementsAt<T>(this IList<T> source, IList<int> indices)
    {
        List<T> output = new();
        
        foreach (var index in indices)
        {
            if (index >= 0 && index < source.Count)
            {
                output.Add(source[index]);
            }
        }
            
        return output;
    }
}