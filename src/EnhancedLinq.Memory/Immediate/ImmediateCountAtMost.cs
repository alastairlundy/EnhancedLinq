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

namespace AlastairLundy.EnhancedLinq.Memory.Immediate;

public static partial class EnhancedLinqMemoryImmediate
{
    /// <param name="source">The source <see cref="Span{T}"/> to search through.</param>
    /// <typeparam name="T">The element type of the source <see cref="Span{T}"/>.</typeparam>
    extension<T>(Span<T> source)
    {
        /// <summary>
        /// Determines whether there are at most a maximum number of elements in the source <see cref="Span{T}"/>.
        /// </summary>
        /// <param name="countToLookFor">The maximum number of elements that can meet the condition.</param>
        /// <returns>True if there are at most <paramref name="countToLookFor"/> number of elements, false otherwise.</returns>
        public bool CountAtMost(int countToLookFor)
        {
            InvalidOperationException.ThrowIfSpanIsEmpty(source);
            ArgumentOutOfRangeException.ThrowIfNegative(countToLookFor);
            
            return source.Length <= countToLookFor;
        }

        /// <summary>
        /// Determines whether there are at most a maximum number of elements in the source <see cref="Span{T}"/> that satisfy the given condition.
        /// </summary>
        /// <param name="predicate">The predicate condition to check elements against.</param>
        /// <param name="countToLookFor">The maximum number of elements that can meet the condition.</param>
        /// <returns>True if there are at most <paramref name="countToLookFor"/> number of elements that satisfy the condition, false otherwise.</returns>
        public bool CountAtMost(Func<T, bool> predicate,
            int countToLookFor)
        {
            InvalidOperationException.ThrowIfSpanIsEmpty(source);
            ArgumentNullException.ThrowIfNull(predicate);
            ArgumentOutOfRangeException.ThrowIfNegative(countToLookFor);

            int currentCount = 0;

            foreach (T obj in source)
            {
                if (predicate(obj))
                    currentCount += 1;
            
                if(currentCount >= countToLookFor)
                    return false;
            }

            return true;
        }
    }


    /// <summary>
    /// Determines whether there are at most a maximum number elements in the source <see cref="Span{T}"/> that satisfy the given condition.
    /// </summary>
    /// <param name="source">The source <see cref="Span{T}"/> to search through.</param>
    /// <param name="predicate">The predicate condition to check elements against.</param>
    /// <param name="countToLookFor">The maximum number of elements that can meet the condition.</param>
    /// <typeparam name="T">The element type of the source <see cref="Span{T}"/>.</typeparam>
    /// <returns>True if there are at most <paramref name="countToLookFor"/> number of elements that satisfy the condition, false otherwise.</returns>
    public static bool CountAtMost<T>(this ReadOnlySpan<T> source, Func<T, bool> predicate,
        int countToLookFor)
    {
        InvalidOperationException.ThrowIfSpanIsEmpty(source);
        ArgumentOutOfRangeException.ThrowIfNegative(countToLookFor);
        
        int currentCount = 0;

        foreach (T obj in source)
        {
            if (predicate(obj))
                currentCount += 1;
            
            if(currentCount >= countToLookFor)
                return false;
        }

        return true;
    }
}