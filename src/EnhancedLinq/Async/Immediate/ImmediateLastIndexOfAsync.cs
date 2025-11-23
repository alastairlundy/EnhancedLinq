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

using System.Linq;

namespace AlastairLundy.EnhancedLinq.Async.Immediate;

public static partial class EnhancedLinqAsyncImmediate
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <typeparam name="T"></typeparam>
    extension<T>(IAsyncEnumerable<T> source)
    {
        /// <summary>
        /// Gets the last index of an element in an asynchronous sequence that satisfies the provided predicate.
        /// </summary>
        /// <remarks>
        /// This method iterates over the sequence in reverse order, which may be computationally expensive depending on the number of items in the sequence.
        /// </remarks>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>The last index of an element in the sequence that satisfies the predicate; if no element is found, returns -1.</returns>
        public async Task<int> LastIndexOf(Func<T, bool> predicate)
        {
            ArgumentNullException.ThrowIfNull(source);
        
            bool foundItem = false;
            int reverseIndex = 0;
        
            int count = 0;
        
            await foreach (T item in source.Reverse())
            {
                if (predicate(item))
                {
                    foundItem = true;
                    reverseIndex = count;
                }

                count++;
            }

        
            return foundItem ? Math.Abs(count - reverseIndex) : -1;
        }

        /// <summary>
        /// Gets the last index of an element in an asynchronous sequence.
        /// </summary>
        /// <remarks>
        /// This method iterates over the sequence in reverse order, which may be computationally expensive depending on the number of items in the sequence.
        /// </remarks>
        /// <returns>The last index of an element in the sequence; if no element is found, returns -1.</returns>
        public async Task<int> LastIndexOf(T obj)
        {
            ArgumentNullException.ThrowIfNull(source);

            bool foundItem = false;
            int reverseIndex = 0;
            int count = 0;
                
            await foreach (T item in source.Reverse())
            {
                if (item is not null && item.Equals(obj))
                {
                    foundItem = true;
                    reverseIndex = count;
                }
                    
                count++;
            }
        
            return foundItem ? Math.Abs(count - reverseIndex) : -1;
        }
    }
}