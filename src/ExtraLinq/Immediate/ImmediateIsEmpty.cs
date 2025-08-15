/*
    ExtraLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/.
 */

using System.Collections.Generic;
using System.Linq;

namespace ExtraLinq.Immediate;

public static partial class ExtraLinqImmediate
{
    /// <summary>
    /// Checks if a sequence is empty or not.
    /// </summary>
    /// <param name="source">The sequence to check.</param>
    /// <typeparam name="T">The type of element stored in the sequence.</typeparam>
    /// <returns>True if the sequence is empty, false otherwise.</returns>
    public static bool IsEmpty<T>(this IEnumerable<T> source)
    {
        if (source is ICollection<T> collection)
            return collection.Count == 0;

        return source.Any() == false;
    }
}