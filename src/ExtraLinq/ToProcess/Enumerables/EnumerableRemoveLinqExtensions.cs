using System.Collections.Generic;
using System.Linq;

namespace ExtraLinq.ToProcess.Enumerables;

public static class EnumerableRemoveLinqExtensions
{
    /// <summary>
    /// Removes an item from an IEnumerable.
    /// </summary>
    /// <param name="source">The IEnumerable to have an item removed from it.</param>
    /// <param name="itemToBeRemoved">The item to be removed.</param>
    /// <typeparam name="T">The type of elements stored in the IEnumerable.</typeparam>
    /// <returns>The new IEnumerable with the specified item removed.</returns>
    public static IEnumerable<T> Remove<T>(this IEnumerable<T> source, T itemToBeRemoved)
    {
        return from item in source
            where item.Equals(itemToBeRemoved) == false
            select item;
    }

}