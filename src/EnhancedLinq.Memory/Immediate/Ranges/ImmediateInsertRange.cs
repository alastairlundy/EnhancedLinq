/*
    EnhancedLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/.
 */

using AlastairLundy.DotExtensions.Memory.Spans;

namespace AlastairLundy.EnhancedLinq.Memory.Immediate.Ranges;

public static partial class EnhancedLinqMemoryImmediateRange
{
    /// <summary>
    /// Inserts a collection of elements at the specified start index into the span.
    /// </summary>
    /// <param name="span">The original span to insert the range of items into.</param>
    /// <param name="elements">The collection of elements to be inserted.</param>
    /// <param name="startIndex">The zero-based starting index of the insertion.</param>
    /// <typeparam name="T">The type of elements in the span.</typeparam>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the start or end indices are out of range for the span.</exception>
    public static void InsertRange<T>(this ref Span<T> span, ICollection<T> elements, int startIndex)
    {
        if (startIndex >= 0 == false && startIndex < span.Length == false)
            throw new ArgumentOutOfRangeException(nameof(startIndex));

        int newLength = span.Length + elements.Count;
        
        span.Resize(newLength);
        
        int i = startIndex;
        
        foreach (T element in elements)
        {
            if (i > span.Length)
                throw new ArgumentOutOfRangeException(nameof(startIndex));
            
            span[i] = element;
            
            i++;
        }
    }
}