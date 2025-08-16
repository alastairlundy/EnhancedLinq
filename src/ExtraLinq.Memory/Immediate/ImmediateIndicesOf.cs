/*
    ExtraLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/.
 */

namespace ExtraLinq.Memory.Immediate;

/// <summary>
/// 
/// </summary>
public static partial class ExtraLinqMemoryImmediate
{
    /// <summary>
    /// Gets a collection of indices within the given span where the specified value occurs.
    /// </summary>
    /// <param name="source">The initial span to search.</param>
    /// <param name="item">The value to find in the span.</param>
    /// <typeparam name="T">The type of elements within the span.</typeparam>
    /// <returns>A collection of indices that represent the occurrences of item in span.</returns>
    public static ICollection<int> IndicesOf<T>(this Span<T> source, T item) where T : notnull
    {
        List<int> indices = new List<int>();

        for (int index = 0; index < source.Length; index++)
        {
            if (item is not null && item.Equals(source[index]))
            {
                indices.Add(index);
            }
        }
        
        return indices;
    }

    /// <summary>
    /// Gets a collection of indices within the given span where items match the selector condition.
    /// </summary>
    /// <param name="source">The initial span to search.</param>
    /// <param name="selector"></param>
    /// <typeparam name="T">The type of elements within the span.</typeparam>
    /// <returns>A collection of indices that represent all items in the span that match the selector.</returns>
    public static ICollection<int> IndicesOf<T>(this Span<T> source, Func<T, bool> selector) where T : notnull
    {
        List<int> indices = new List<int>();
        
        for (int index = 0; index < source.Length; index++)
        {
            if(selector(source[index]))
                indices.Add(index);
        }
        
        return indices;
    }
}