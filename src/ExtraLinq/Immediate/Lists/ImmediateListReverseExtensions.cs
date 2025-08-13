/*
        MIT License
       
       Copyright (c) 2025 Alastair Lundy
       
       Permission is hereby granted, free of charge, to any person obtaining a copy
       of this software and associated documentation files (the "Software"), to deal
       in the Software without restriction, including without limitation the rights
       to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
       copies of the Software, and to permit persons to whom the Software is
       furnished to do so, subject to the following conditions:
       
       The above copyright notice and this permission notice shall be included in all
       copies or substantial portions of the Software.
       
       THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
       IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
       FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
       AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
       LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
       OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
       SOFTWARE.
   */

using System.Collections.Generic;

namespace ExtraLinq.Immediate.Lists;

public static class ImmediateListReverseExtensions
{
    /// <summary>
    /// Reverses an <see cref="IList{T}"/> and returns it.
    /// </summary>
    /// <param name="list">The list to reverse.</param>
    /// <typeparam name="T">The type of elements in the array.</typeparam>
    /// <returns>The reversed IList.</returns>
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