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

// ReSharper disable InconsistentNaming

namespace ExtraLinq.ToProcess.ILists;

public static class IListReplaceExtensions
{
    /// <summary>
    /// Replaces all occurrences of an item in an IList with a replacement item.
    /// </summary>
    /// <param name="source">The IList to be modified.</param>
    /// <param name="oldValue">The value to be replaced.</param>
    /// <param name="newValue">The replacement value.</param>
    /// <typeparam name="T">The type of value.</typeparam>
    /// <returns>The modified IList if the IList contains the value to be replaced; Otherwise the original IList is returned.</returns>
    public static IList<T> Replace<T>(this IList<T> source, T oldValue, T newValue)
    {
        int index = source.IndexOf(oldValue);
                
        source[index] = newValue;
        return source;
    }
}