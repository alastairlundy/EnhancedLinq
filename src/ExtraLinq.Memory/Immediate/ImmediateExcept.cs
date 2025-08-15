/*
    ExtraLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/.
 */

namespace ExtraLinq.Memory.Immediate;

public static class ImmediateExcept
{
    /// <summary>
    /// Returns a new Span with all the elements of two Spans that are only in one Span and not the other.
    /// </summary>
    /// <param name="first">The first Span to search.</param>
    /// <param name="second">The second Span to search.</param>
    /// <typeparam name="T">The type of items stored in the span.</typeparam>
    /// <returns>A new Span with all the elements of Span One and Span Two that were not in the other Span.</returns>
    public static Span<T> Except<T>(this Span<T> first, Span<T> second) where T : IEquatable<T>
    {
        T[] output = new  T[first.Length + second.Length];
        int index = 0;

        foreach (T item in first)
        {
            if (second.Contains(item) == false)
            {
                output[index] = item;
                index++;
            }
        }

        foreach (T item in second)
        {
            if(first.Contains(item) == false)
            {
                output[index] = item;
                index++;
            }
        }
        
        Array.Resize(ref output, index);

        return new  Span<T>(output);
    }
}