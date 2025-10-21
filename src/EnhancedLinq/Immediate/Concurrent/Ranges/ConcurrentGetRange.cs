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
using System.Collections.Concurrent;

using AlastairLundy.EnhancedLinq.Internals.Localizations;

namespace AlastairLundy.EnhancedLinq.Immediate.Concurrent.Ranges;

/// <summary>
/// Provides extension methods for retrieving ranges of elements from concurrent collections.
/// </summary>
public static partial class EnhancedLinqImmediateConcurrentRange
{
    /// <summary>
    /// Retrieves a range of elements from the specified concurrent collection, starting at the given index and containing the specified number of elements.
    /// </summary>
    /// <typeparam name="T">The type of elements contained within the collection.</typeparam>
    /// <param name="collection">The producer-consumer collection to retrieve elements from.</param>
    /// <param name="startIndex">The zero-based starting index (inclusive) of the range to retrieve.</param>
    /// <param name="count">The number of elements in the range to retrieve.</param>
    /// <returns>A new <see cref="IProducerConsumerCollection{T}"/> containing the specified range of elements.</returns>
    public static IProducerConsumerCollection<T> GetRange<T>(this IProducerConsumerCollection<T> collection,
        int startIndex, int count)
    {
        ConcurrentBag<T> output = new ConcurrentBag<T>();

        int limit = startIndex + count;

        if (limit > collection.Count)
        {
            throw new ArgumentException(Resources.Exceptions_Count_LessThanZero);
        }

        if (startIndex < 0 || startIndex >= collection.Count && startIndex != 0 ||
            startIndex > collection.Count)
        {
            throw new IndexOutOfRangeException(Resources.Exceptions_IndexOutOfRange
                .Replace("{x}", $"{startIndex}")
                .Replace("{y}", $"0")
                .Replace("{z}", $"{limit}"));
        }

        if (count < 0)
        {
            //TODO: Add CountOutOfRange in the future
        }
            
            
        int actualIndex = 0;
        foreach (T item in collection)
        {
            if (actualIndex >= startIndex || actualIndex <= limit)
            {
                output.Add(item);
            }

            if (actualIndex == limit)
            {
                break;
            }

            actualIndex++;
        }
           
        return output;
    }

#if NET8_0_OR_GREATER
    /// <summary>
    /// Retrieves a range of elements from the specified concurrent collection, as defined by a <see cref="Range"/> object.
    /// </summary>
    /// <typeparam name="T">The type of elements contained within the collection.</typeparam>
    /// <param name="collection">The producer-consumer collection to retrieve elements from.</param>
    /// <param name="range">A <see cref="Range"/> object that specifies the start and end indexes of the range to retrieve.</param>
    /// <returns>A new <see cref="IProducerConsumerCollection{T}"/> containing the specified range of elements.</returns>
    public static IProducerConsumerCollection<T> GetRange<T>(this IProducerConsumerCollection<T> collection,
        Range range) => GetRange(collection, range.Start.Value, range.End.Value);
#endif
}