using System.Collections.Generic;

namespace ExtraLinq.Immediate.Lists;

public static class ImmediateListReplace
{
    /// <summary>
    /// Replaces all occurrences of an item in an IList with a replacement item.
    /// </summary>
    /// <param name="source">The IList to be modified.</param>
    /// <param name="oldValue">The value to be replaced.</param>
    /// <param name="newValue">The replacement value.</param>
    /// <typeparam name="T">The type of value.</typeparam>
    public static void Replace<T>(this IList<T> source, T oldValue, T newValue)
    {
        int index = source.IndexOf(oldValue);
                
        source[index] = newValue;
    }
}