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

namespace AlastairLundy.EnhancedLinq.Immediate.Dictionaries;

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