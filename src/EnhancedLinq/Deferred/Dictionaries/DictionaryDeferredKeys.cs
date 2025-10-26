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

namespace AlastairLundy.EnhancedLinq.Deferred.Dictionaries;

/// <summary>
/// Provides a set of static methods for working with dictionaries in a Linq style deferred manner.
/// </summary>
public static partial class EnhancedLinqDeferredDictionary
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