using System;
using System.Collections.Generic;

namespace ExtraLinq.Immediate;

public static class ImmediateIndicesOf
{
    /// <summary>
    /// Returns a collection of indices within the given span where the specified value occurs.
    /// </summary>
    /// <param name="span">The initial span to search.</param>
    /// <param name="item">The value to find in the span.</param>
    /// <typeparam name="T">The type of elements within the span.</typeparam>
    /// <returns>A collection of indices that represent the occurrences of item in span.</returns>
    public static ICollection<int> IndicesOf<T>(this Span<T> span, T item) where T : notnull
    {
        List<int> indices = new List<int>();

        for (int index = 0; index < span.Length; index++)
        {
            if (item is not null && item.Equals(span[index]))
            {
                indices.Add(index);
            }
        }

        if (indices.Count == 0)
            return [];
        
        return indices;
    }

}