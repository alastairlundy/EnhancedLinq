/*
    EnhancedLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
    */

using System;
using System.Collections.Generic;
using System.Linq;

namespace AlastairLundy.EnhancedLinq.Deferred.Pairs.KeyValuePairs;

/// <summary>
/// This static class contains Deferred Execution extension methods for sequences that store <see cref="KeyValuePair{TKey,TValue}"/>.
/// </summary>
public static partial class EnhancedLinqDeferredPairs
{
    /// <param name="source">The IEnumerable of key-value pairs.</param>
    /// <typeparam name="TKey">The type of Key in the KeyValuePair.</typeparam>
    /// <typeparam name="TValue">The type of Value in the KeyValuePair.</typeparam>
    extension<TKey, TValue>(IEnumerable<KeyValuePair<TKey, TValue>> source)
    where TKey : notnull
    where TValue : notnull
    {
        /// <summary>
        /// Converts an IEnumerable of key-value pairs to a sequence of keys.
        /// </summary>
        /// <returns>A sequence of keys extracted from the input.</returns>
        public IEnumerable<TKey> ToKeys()
        {
            ArgumentNullException.ThrowIfNull(source);
            
            return from pair in source
                select pair.Key;
        }

        /// <summary>
        /// Converts an IEnumerable of key-value pairs to a sequence of values.
        /// </summary>
        /// <returns>A sequence of values extracted from the input.</returns>
        public IEnumerable<TValue> ToValues()
        {
            ArgumentNullException.ThrowIfNull(source);
            
            return from pair in source
                select pair.Value;
        }
    }
}