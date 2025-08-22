/*
    EnhancedLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/.
 */

using System.Collections.Generic;

namespace AlastairLundy.EnhancedLinq.Immediate;

public static partial class EnhancedLinqImmediate
{
    /// <summary>
    /// Reverses a <see cref="List{T}"/>.
    /// </summary>
    /// <param name="list">The list to reverse.</param>
    /// <typeparam name="T">The type of elements in the array.</typeparam>
    /// <returns>The reversed IList.</returns>
    public static void Reverse<T>(this List<T> list)
    {
        List<T> output = new List<T>(list.Count);

        for (int i = 0; i < output.Count; i++)
        {
            if(list.Count -1 - i >= 0)
                output.Add(list[list.Count - 1 - i]);
            else
                break;
        }
        
        list.Clear();
        list.AddRange(output);
    }

    /// <summary>
    /// Returns a reversed array.
    /// </summary>
    /// <param name="array">The array to reverse.</param>
    /// <typeparam name="T">The type of elements in the array.</typeparam>
    /// <returns>The reversed array.</returns>
    public static T[] Reverse<T>(this T[] array)
    {
        T[] newArray = new T[array.Length];

        for (int i = 0; i < array.Length; i++)
        {
            newArray[i] = array[array.Length - 1 - i];
        }
        
        return newArray;
    }
}