/*
    EnhancedLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/.
 */

using System;
using System.Linq;

namespace AlastairLundy.EnhancedLinq.Memory.Immediate;

public static partial class EnhancedLinqMemoryImmediate
{
    
    /// <summary>
    /// Returns whether all items in a Span match the predicate condition.
    /// </summary>
    /// <param name="target">The span to be searched.</param>
    /// <param name="predicate">The predicate func to be invoked on each item in the Span.</param>
    /// <typeparam name="T">The type of items stored in the span.</typeparam>
    /// <returns>True if all items in the span match the predicate; false otherwise.</returns>
    public static bool All<T>(this Span<T> target, Func<T, bool> predicate)
    {      
        Span<bool> groups = (from c in target
                group c by predicate.Invoke(c)
                into g
                where g.Key
                select g.Any());

        return groups.Distinct().Length ==  1;
    }
}