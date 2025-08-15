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

        if (indices.Any() == false)
            return [-1];
        
        return indices;
    }
}