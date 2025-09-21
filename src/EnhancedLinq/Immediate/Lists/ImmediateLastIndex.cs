/*
    EnhancedLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/.
 */

using System;
using System.Collections.Generic;

namespace AlastairLundy.EnhancedLinq.Immediate;

public static partial class EnhancedLinqImmediate
{
    /// <summary>
    /// Provides the last index of an item in a collection.
    /// </summary>
    /// <param name="source"></param>
    /// <typeparam name="T">The type of element in the collection.</typeparam>
    /// <returns>The last index of an item in a collection.</returns>
    public static int LastIndex<T>(this ICollection<T> source)
    {
        ArgumentNullException.ThrowIfNull(source);
        
        if(source.Count > 0)
            return source.Count - 1;

        return -1;
    }
}