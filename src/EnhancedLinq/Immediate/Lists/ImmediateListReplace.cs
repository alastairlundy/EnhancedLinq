/*
    EnhancedLinq 
    Copyright (c) 2025-2026 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/.
*/

using EnhancedLinq.Deferred;

namespace EnhancedLinq.Immediate;

public static partial class EnhancedLinqImmediate
{
    /// <param name="source">The IList to be modified.</param>
    /// <typeparam name="T">The type of value.</typeparam>
    extension<T>(IList<T> source) where T : notnull
    {
        /// <summary>
        /// Replaces all occurrences of an item in an IList with a replacement item.
        /// </summary>
        /// <param name="oldValue">The value to be replaced.</param>
        /// <param name="newValue">The replacement value.</param>
        public void Replace(T oldValue, T newValue)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(oldValue);
            ArgumentNullException.ThrowIfNull(newValue);
            
            IEnumerable<int> indices = source.IndicesOf(oldValue);

            foreach (int index in indices)
            {
                source[index] = newValue;
            }
        }
    }
}