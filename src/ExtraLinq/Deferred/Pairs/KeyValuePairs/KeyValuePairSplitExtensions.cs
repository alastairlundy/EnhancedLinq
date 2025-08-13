/*
        MIT License
       
       Copyright (c) 2024-2025 Alastair Lundy
       
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
using System.Linq;

namespace ExtraLinq.Deferred.Pairs.KeyValuePairs;

/// <summary>
/// 
/// </summary>
public static class KeyValuePairSplitExtensions
{
    /// <summary>
    /// Converts an IEnumerable of key-value pairs to a sequence of keys.
    /// </summary>
    /// <param name="source">The IEnumerable of key-value pairs.</param>
    /// <typeparam name="TKey">The type of Key in the KeyValuePair.</typeparam>
    /// <typeparam name="TValue">The type of Value in the KeyValuePair.</typeparam>
    /// <returns>A sequence of keys extracted from the input.</returns>
    public static IEnumerable<TKey> ToKeys<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source)
    {
        return from pair in source
            select pair.Key;
    }

    /// <summary>
    /// Converts an IEnumerable of key-value pairs to a sequence of values.
    /// </summary>
    /// <param name="source">The IEnumerable of key-value pairs to extract values from.</param>
    /// <typeparam name="TKey">The type of Key in the KeyValuePair.</typeparam>
    /// <typeparam name="TValue">The type of Value in the KeyValuePair.</typeparam>
    /// <returns>A sequence of values extracted from the input.</returns>
    public static IEnumerable<TValue> ToValues<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source)
    {
        return from pair in source
            select pair.Value;
    }
}