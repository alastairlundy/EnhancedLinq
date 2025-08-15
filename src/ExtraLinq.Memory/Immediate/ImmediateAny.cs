/*
    ExtraLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/.
 */

using AlastairLundy.DotExtensions.Memory.Spans;

namespace ExtraLinq.Memory.Immediate;

public static partial class ExtraLinqMemoryImmediate
{
    
    /// <summary>
    /// Returns whether any item in a Span matches the predicate condition.
    /// </summary>
    /// <param name="target">The Span to be searched.</param>
    /// <param name="predicate">The predicate func to be invoked on each item in the Span.</param>
    /// <typeparam name="T">The type of items stored in the span.</typeparam>
    /// <returns>True if any item in the span matches the predicate; false otherwise.</returns>
    public static bool Any<T>(this Span<T> target, Func<T, bool> predicate)
    {
        Span<bool> groups = (from c in target
                group c by predicate.Invoke(c)
                into g
                where g.Key
                select g.Any());

        bool? result = groups.FirstOrDefault();

        return result ?? false;
    }
}