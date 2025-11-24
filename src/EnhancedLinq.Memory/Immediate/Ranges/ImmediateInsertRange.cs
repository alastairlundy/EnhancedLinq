/*
    EnhancedLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
    */

namespace AlastairLundy.EnhancedLinq.Memory.Immediate.Ranges;

public static partial class EnhancedLinqMemoryImmediateRange
{
    /// <param name="span">The original span to insert the range of items into.</param>
    /// <typeparam name="T">The type of elements in the span.</typeparam>
    extension<T>(ref Span<T> span)
    {
        /// <summary>
        /// Inserts a collection of elements at the specified start index into the span.
        /// </summary>
        /// <param name="elements">The collection of elements to be inserted.</param>
        /// <param name="startIndex">The zero-based starting index of the insertion.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the start or end indices are out of range for the span.</exception>
        public void InsertRange(ICollection<T> elements, int startIndex)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(startIndex);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(startIndex, span.Length);
        
            int newLength = span.Length + elements.Count;
        
            span.Resize(newLength);
        
            int i = startIndex;
        
            foreach (T element in elements)
            {
                span[i] = element;
            
                i++;
            }
        }
    }
}