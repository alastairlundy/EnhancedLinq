using System.Collections.Generic;
/*
    ExtraLinq
    Copyright (c) 2025 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/.
 */

namespace ExtraLinq.Immediate;

public static class ImmediateReverse
{
    
    /// <summary>
    /// Reverses a <see cref="List{T}"/> and returns it.
    /// </summary>
    /// <param name="list">The list to reverse.</param>
    /// <typeparam name="T">The type of elements in the array.</typeparam>
    /// <returns>The reversed <see cref="List{T}"/>.</returns>
    public static List<T> Reverse<T>(this List<T> list)
    {
        List<T> output = new List<T>(list.Count);

        for (int i = 0; i < output.Count; i++)
        {
            output.Add(list[list.Count - 1 - i]);
        }
        
        return output;
    }

    /// <summary>
    /// Reverses an array and returns it.
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