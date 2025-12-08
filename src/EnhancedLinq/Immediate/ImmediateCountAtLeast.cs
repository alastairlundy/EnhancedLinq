/*
    EnhancedLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
    */

namespace EnhancedLinq.Immediate;

public static partial class EnhancedLinqImmediate
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="source">The source sequence.</param>
    /// <typeparam name="T">The element type in the source sequence.</typeparam>
    extension<T>(IEnumerable<T> source)
    {
        /// <summary>
        /// Determines whether there are at least a specified number of elements in the sequence.
        /// </summary>
        /// <param name="countToLookFor">The minimum count to look for.</param>
        /// <returns><c>true</c> if there is at least the specified number of elements in the sequence; otherwise, <c>false</c>.</returns>
        public bool CountAtLeast(int countToLookFor)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentOutOfRangeException.ThrowIfNegative(countToLookFor);
            
            if (source is ICollection<T> collection)
                return collection.Count >= countToLookFor;
            
            int currentCount = 0;

            foreach (T unused in source)
            {
                if(currentCount >= countToLookFor)
                    return true;

                currentCount += 1;
            }

            return false;
        }
        
        /// <summary>
        /// Determines whether there are at least a specified number of elements in the sequence that meet a given condition.
        /// </summary>
        /// <param name="predicate">The predicate condition to check elements against.</param>
        /// <param name="countToLookFor">The minimum count to look for.</param>
        /// <returns><c>true</c> if there is at least the specified number of elements that meet the condition; otherwise, <c>false</c>.</returns>
        public bool CountAtLeast(Func<T, bool> predicate,
            int countToLookFor)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(predicate);
            ArgumentOutOfRangeException.ThrowIfNegative(countToLookFor);
            
            int currentCount = 0;

            foreach (T obj in source)
            {
                if (predicate(obj))
                    currentCount += 1;
            
                if(currentCount >= countToLookFor)
                    return true;
            }

            return false;
        }
    }
}