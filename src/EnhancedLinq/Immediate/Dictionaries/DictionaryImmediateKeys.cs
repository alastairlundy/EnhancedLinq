/*
    EnhancedLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/.
 */

using System.Collections.Generic;
using System.Linq;

namespace AlastairLundy.EnhancedLinq.Immediate.Dictionaries;

public static partial class EnhancedLinqImmediateDictionary
{
    /// <summary>
    /// Gets the Key associated with the specified value in the Dictionary.
    /// </summary>
    /// <remarks>
    /// This method assumes there is only ONE Key associated with a specific Value in a Dictionary.
    /// If multiple Keys have the same Value, use the GetKeys method instead.</remarks>
    /// <param name="dictionary">The Dictionary to be searched.</param>
    /// <param name="value">The value to search for.</param>
    /// <typeparam name="TKey">The type of Key in the Dictionary.</typeparam>
    /// <typeparam name="TValue">The type of Value in the Dictionary.</typeparam>
    /// <returns>The key associated with the specified value in a Dictionary.</returns>
    public static TKey GetKeyByValue<TKey, TValue>(this IDictionary<TKey, TValue> dictionary,
        TValue value) where TKey : notnull =>
        dictionary.First(x => x.Value is not null &&
                              x.Value.Equals(value)).Key;
}