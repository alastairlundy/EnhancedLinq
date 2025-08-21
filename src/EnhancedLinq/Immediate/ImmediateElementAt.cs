/*
    EnhancedLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/.
 */

using System;
using System.Collections.Generic;
using EnhancedLinq.Internals.Localizations;

namespace EnhancedLinq.Immediate;

public static partial class EnhancedLinqImmediate
{
    /// <summary>
    /// Retrieves the element at a specified index from the sequence.
    /// </summary>
    /// <param name="source">The sequence to retrieve the element from.</param>
    /// <param name="index">The zero-based index of the element to retrieve.</param>
    /// <typeparam name="T">The type of elements in the sequence.</typeparam>
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
}