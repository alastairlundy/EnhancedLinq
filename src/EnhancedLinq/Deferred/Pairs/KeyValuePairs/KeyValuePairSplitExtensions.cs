/*
      EnhancedLinq 
      Copyright (c) 2025 Alastair Lundy
      
     Licensed under the Apache License, Version 2.0 (the "License");
     you may not use this file except in compliance with the License.
     You may obtain a copy of the License at

         http://www.apache.org/licenses/LICENSE-2.0

     Unless required by applicable law or agreed to in writing, software
     distributed under the License is distributed on an "AS IS" BASIS,
     WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
     See the License for the specific language governing permissions and
     limitations under the License.
 */

using System.Collections.Generic;
using System.Linq;

namespace AlastairLundy.EnhancedLinq.Deferred.Pairs.KeyValuePairs;

/// <summary>
/// This static class contains Deferred Execution extension methods for sequences that store <see cref="KeyValuePair{TKey,TValue}"/>.
/// </summary>
public static partial class EnhancedLinqDeferredPairs
{
    /// <summary>
    /// Converts an IEnumerable of key-value pairs to a sequence of keys.
    /// </summary>
    /// <param name="source">The IEnumerable of key-value pairs.</param>
    /// <typeparam name="TKey">The type of Key in the KeyValuePair.</typeparam>
    /// <typeparam name="TValue">The type of Value in the KeyValuePair.</typeparam>
    /// <returns>A sequence of keys extracted from the input.</returns>
    public static IEnumerable<TKey> ToKeys<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source)
    {
        return from pair in source
            select pair.Key;
    }

    /// <summary>
    /// Converts an IEnumerable of key-value pairs to a sequence of values.
    /// </summary>
    /// <param name="source">The IEnumerable of key-value pairs to extract values from.</param>
    /// <typeparam name="TKey">The type of Key in the KeyValuePair.</typeparam>
    /// <typeparam name="TValue">The type of Value in the KeyValuePair.</typeparam>
    /// <returns>A sequence of values extracted from the input.</returns>
    public static IEnumerable<TValue> ToValues<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source)
    {
        return from pair in source
            select pair.Value;
    }
}