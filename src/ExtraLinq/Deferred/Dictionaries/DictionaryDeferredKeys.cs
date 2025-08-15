/*
    ExtraLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/.
 */

using System.Collections.Generic;
using System.Linq;

namespace ExtraLinq.Deferred.Dictionaries;

public static class DictionaryDeferredKeys
{
    /// <summary>
    /// Returns all keys associated with a specified value in a Dictionary.
    /// </summary>
    /// <param name="dictionary">The Dictionary to be searched.</param>
    /// <param name="value">The value to search for.</param>
    /// <typeparam name="TKey">The type of Key in the Dictionary.</typeparam>
    /// <typeparam name="TValue">The type of Value in the Dictionary.</typeparam>
    /// <returns>The keys associated with the specified value in a Dictionary.</returns>
    public static IEnumerable<TKey> GetKeysByValue<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TValue value)
    {
        return (from pair in dictionary
            where pair.Value.Equals(value)
            select pair.Key);
    }
}