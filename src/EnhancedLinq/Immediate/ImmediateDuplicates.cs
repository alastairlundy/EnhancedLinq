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

namespace AlastairLundy.EnhancedLinq.Immediate;

public static partial class EnhancedLinqImmediate
{
    /// <param name="source">The <see cref="IEnumerable{T}"/> to be searched.</param>
    /// <typeparam name="T">The type of objects in the <see cref="IEnumerable{T}"/>.</typeparam>
    extension<T>(IEnumerable<T> source) where T : notnull
    {
        /// <summary>
        /// Determines whether an <see cref="IEnumerable{T}"/> contains duplicate instances of an object.
        /// </summary>
        /// <returns>True if the <see cref="IEnumerable{T}"/> contains duplicate objects; false otherwise.</returns>
        public bool ContainsDuplicates()
        {
            HashSet<T> hash = new();
        
            foreach (T item in source)
            {
                bool result = hash.Add(item);

                if (result == false)
                    return true;
            }

            return false;
        }
    }
}