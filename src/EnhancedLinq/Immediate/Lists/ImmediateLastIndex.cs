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

#if NET8_0_OR_GREATER
using System;
#endif

using System.Collections.Generic;

namespace AlastairLundy.EnhancedLinq.Immediate;

public static partial class EnhancedLinqImmediate
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <typeparam name="T">The type of element in the collection.</typeparam>
    extension<T>(ICollection<T> source)
    {
        /// <summary>
        /// Provides the last index of an item in a collection.
        /// </summary>
        /// <returns>The last index of an item in a collection.</returns>
        public int LastIndex()
        {
#if NET8_0_OR_GREATER
            ArgumentNullException.ThrowIfNull(source);
#endif
        
            if(source.Count > 0)
                return source.Count - 1;

            return -1;
        }
    }
}