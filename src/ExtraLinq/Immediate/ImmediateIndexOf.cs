using System.Collections.Generic;
using AlastairLundy.DotExtensions.Exceptions;

namespace ExtraLinq.Immediate;

public static class ImmediateIndexOf
{
    /// <summary>
    /// Returns the index of an object in an IEnumerable.
    /// </summary>
    /// <param name="source">The IEnumerable to be searched.</param>
    /// <param name="obj">The object to get the index of.</param>
    /// <typeparam name="T">The type of object in the IEnumerable.</typeparam>
    /// <returns>The index of an object in an IEnumerable, if the IEnumerable contains the object, returns -1 otherwise.</returns>
    public static int IndexOf<T>(this IEnumerable<T> source, T obj)
    {
        if (source is IList<T> list)
        {
            return list.IndexOf(obj);
        }
            
        int index = 0;
                
        foreach (T item in source)
        {
            if (item is not null && item.Equals(obj))
            {
                return index;
            }
                    
            index++;
        }
        
        return -1;
    }
}