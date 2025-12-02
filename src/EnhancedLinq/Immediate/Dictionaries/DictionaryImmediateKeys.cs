/*
    EnhancedLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
    */

using System.Collections.Generic;
using System.Linq;

namespace EnhancedLinq.Immediate.Dictionaries;

/// <summary>
/// Provides a collection of extension methods for performing enhanced LINQ operations on dictionaries.
/// </summary>
public static partial class EnhancedLinqImmediateDictionary
{
    /// <param name="dictionary">The Dictionary to be searched.</param>
    /// <typeparam name="TKey">The type of Key in the Dictionary.</typeparam>
    /// <typeparam name="TValue">The type of Value in the Dictionary.</typeparam>
    extension<TKey, TValue>(IDictionary<TKey, TValue> dictionary) where TKey : notnull
    {
        /// <summary>
        /// Gets the Key associated with the specified value in the Dictionary.
        /// </summary>
        /// <remarks>
        /// This method assumes there is only ONE Key associated with a specific Value in a Dictionary.
        /// If multiple Keys have the same Value, use the GetKeys method instead.</remarks>
        /// <param name="value">The value to search for.</param>
        /// <returns>The key associated with the specified value in a Dictionary.</returns>
        public TKey GetKeyByValue(TValue value) =>
            dictionary.First(x => x.Value is not null &&
                                  x.Value.Equals(value)).Key;
    }
}