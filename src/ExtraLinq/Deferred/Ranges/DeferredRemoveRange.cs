using System.Collections.Generic;
using System.Linq;

namespace ExtraLinq.Deferred.Ranges;

public static class DeferredRemoveRange
{
    /// <summary>
    /// Removes items from an IEnumerable.
    /// </summary>
    /// <param name="source">The IEnumerable to have items removed from.</param>
    /// <param name="itemsToBeRemoved">The items to be removed.</param>
    /// <typeparam name="T">The type of elements stored in the IEnumerable.</typeparam>
    /// <returns>The new IEnumerable with the specified items removed.</returns>
    public static IEnumerable<T> RemoveRange<T>(this IEnumerable<T> source, IEnumerable<T> itemsToBeRemoved)
    {
        return from item in source
            where itemsToBeRemoved.Contains(item) == false
            select item;
    }
}