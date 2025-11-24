/*
    EnhancedLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
    */

using System.Linq;

namespace AlastairLundy.EnhancedLinq.Memory.Immediate;

public static partial class EnhancedLinqMemoryImmediate
{
    /// <param name="target">The Span to be searched.</param>
    /// <typeparam name="T">The type of items stored in the span.</typeparam>
    extension<T>(Span<T> target)
    {
        /// <summary>
        /// Returns whether there are any items in the span.
        /// </summary>
        /// <returns></returns>
        public bool Any() => target.Length > 0;

        /// <summary>
        /// Returns whether any item in a Span matches the predicate condition.
        /// </summary>
        /// <param name="predicate">The predicate func to be invoked on each item in the Span.</param>
        /// <returns>True if any item in the span matches the predicate; false otherwise.</returns>
        public bool Any(Func<T, bool> predicate)
        {
            InvalidOperationException.ThrowIfSpanIsEmpty(target);
            ArgumentNullException.ThrowIfNull(predicate);
            
            Span<bool> groups = (from c in target
                group c by predicate.Invoke(c)
                into g
                where g.Key
                select g.Any());

            bool? result = groups.FirstOrDefault();

            return result ?? false;
        }
    }
}