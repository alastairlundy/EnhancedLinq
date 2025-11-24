/*
    EnhancedLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
    */

using System.Collections.Generic;
using System.Linq;

namespace AlastairLundy.EnhancedLinq.Deferred.Dictionaries;

/// <summary>
/// Provides a set of static methods for working with dictionaries in a Linq style deferred manner.
/// </summary>
public static partial class EnhancedLinqDeferredDictionary
{
    /// <param name="dictionary">The Dictionary to be searched.</param>
    /// <typeparam name="TKey">The type of Key in the Dictionary.</typeparam>
    /// <typeparam name="TValue">The type of Value in the Dictionary.</typeparam>
    extension<TKey, TValue>(IDictionary<TKey, TValue> dictionary)
    {
        /// <summary>
        /// Returns all keys associated with a specified value in a Dictionary.
        /// </summary>
        /// <param name="value">The value to search for.</param>
        /// <returns>The keys associated with the specified value in a Dictionary.</returns>
        public IEnumerable<TKey> GetKeysByValue(TValue value)
        {
            return (from pair in dictionary
                where pair.Value.Equals(value)
                select pair.Key);
        }
    }
}