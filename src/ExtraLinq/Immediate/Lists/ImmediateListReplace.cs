/*
    ExtraLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/.
 */

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