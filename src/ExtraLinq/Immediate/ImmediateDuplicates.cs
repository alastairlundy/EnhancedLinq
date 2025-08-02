using System.Collections.Generic;

namespace ExtraLinq.Immediate;

public static class ImmediateDuplicates
{
    /// <summary>
    /// Returns whether an <see cref="IEnumerable{T}"/> contains duplicate instances of an object.
    /// </summary>
    /// <param name="source">The <see cref="IEnumerable{T}"/> to be searched.</param>
    /// <typeparam name="T">The type of objects in the <see cref="IEnumerable{T}"/>.</typeparam>
    /// <returns>True if the <see cref="IEnumerable{T}"/> contains duplicate objects; false otherwise.</returns>
    public static bool ContainsDuplicates<T>(this IEnumerable<T> source) where T : notnull
    {
        HashSet<T> hash = new();
        
        foreach (T item in source)
        {
            bool result = hash.Add(item);

            if (result == false)
                return true;
        }

        return false;
    }
}