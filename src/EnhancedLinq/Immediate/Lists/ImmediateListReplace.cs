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

using System;
using System.Collections.Generic;

namespace AlastairLundy.EnhancedLinq.Immediate;

public static partial class EnhancedLinqImmediate
{
    /// <param name="source">The IList to be modified.</param>
    /// <typeparam name="T">The type of value.</typeparam>
    extension<T>(IList<T> source)
    {
        /// <summary>
        /// Replaces all occurrences of an item in an IList with a replacement item.
        /// </summary>
        /// <param name="oldValue">The value to be replaced.</param>
        /// <param name="newValue">The replacement value.</param>
        public void Replace(T oldValue, T newValue)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(oldValue);
            ArgumentNullException.ThrowIfNull(newValue);
            
            ICollection<int> indices = source.IndicesOf(oldValue);

            foreach (int index in indices)
            {
                source[index] = newValue;
            }
        }
    }
}