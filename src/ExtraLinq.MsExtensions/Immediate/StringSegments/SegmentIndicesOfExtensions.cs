/*/*
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

using System.Collections.Generic;
using System.Linq;

using AlastairLundy.DotExtensions.Collections.Strings.Enumerables;

using Microsoft.Extensions.Primitives;

namespace AlastairLundy.DotExtensions.MsExtensions.System.StringSegments;

public static class SegmentIndicesOfExtensions
{
    /// <summary>
    /// Finds all occurrences of the specified character within the provided StringSegment.
    /// </summary>
    /// <param name="this">The string segment to be searched.</param>
    /// <param name="c">The character to search for.</param>
    /// <returns>An IEnumerable of Indices for all occurrences specified character within the String Segment;
    /// empty if not found within the String Segment.
    /// </returns>
    public static IEnumerable<int> IndicesOf(this StringSegment @this, char c)
    {
        if (@this.Contains(c) == false)
        {
            yield break;
        }
        
        for(int i = 0; i < @this.Length; i++)
        {
            if (@this[i] == c)
            {
                yield return i;
            }
        }
    }

    /// <summary>
    /// Finds all occurrences of the specified character within the provided StringSegment.
    /// </summary>
    /// <param name="this">The string segment to be searched.</param>
    /// <param name="c">The character to search for.</param>
    /// <returns>An array of Indices for all occurrences specified character within the String Segment;
    /// empty if not found within the String Segment.
    /// </returns>
    public static int[] IndicesOfAsArray(this StringSegment @this, char c)
    {
        List<int> indices = new List<int>();
        
        for(int i = 0; i < @this.Length; i++)
        {
            if (@this[i] == c)
            {
                indices.Add(i);
            }
        }

        if (indices.Count == 0)
            indices = [-1];
        
        return indices.ToArray();
    }

    


    /// <summary>
    /// Gets an <see cref="IEnumerable{T}"/> of Indices for all occurrences of the specified character within the provided StringSegment.
    /// </summary>
    /// <param name="this">The string segment to be searched.</param>
    /// <param name="segment">The StringSegment to search for.</param>
    /// <returns>An <see cref="IEnumerable{T}"/> of Indices for all occurrences specified StringSegment within the String Segment;
    /// an empty <see cref="IEnumerable{T}"/> otherwise.
    /// </returns>
    public static IEnumerable<int> IndicesOf(this StringSegment @this, StringSegment segment)
    {
        if (@this.Length < segment.Length || segment.Length == 0)
        {
            yield return -1;
            yield break;
        }

        IEnumerable<int> indexes = IndicesOf(@this, segment.First())
            .Where(x => x != -1);

        foreach (int index in indexes)
        {
            StringSegment indexSegment = @this.Subsegment(index, segment.Length);

            if (indexSegment.Equals(segment))
            {
                yield return index;
            }
        }
    }
    
    /// <summary>
    /// Finds all occurrences of the specified character within the provided StringSegment.
    /// </summary>
    /// <param name="this">The string segment to be searched.</param>
    /// <param name="segment">The StringSegment to search for.</param>
    /// <returns>An array of Indices for all occurrences of the specified StringSegment within the String Segment;
    /// an empty array otherwise.
    /// </returns>
    public static int[] IndicesOfAsArray(this StringSegment @this, StringSegment segment)
    {
        List<int> indices = new();

        if (@this.Length < segment.Length || segment.Length == 0)
            return [];

        IEnumerable<int> indexes = IndicesOf(@this, segment.First()).Where(x => x != -1);

        foreach (int index in indexes)
        {
            StringSegment indexSegment = @this.Subsegment(index, segment.Length);

            if (indexSegment.Equals(segment))
            {
                indices.Add(index);
            }
        }
        
        return indices.ToArray();
    }
    



    /// <summary>
    /// Finds all occurrences of a specified StringSegment within a string.
    /// </summary>
    /// <param name="str">The string to be searched.</param>
    /// <param name="segment">The StringSegment to search for.</param>
    /// <returns>An IEnumerable of Indices for all occurrences of the specified StringSegment within the string; empty if not found within the String Segment.</returns>
    public static IEnumerable<int> IndicesOf(this string str, StringSegment segment)
    {
        if (str.Length < segment.Length || segment.IsEmpty())
        {
            yield return -1;
            yield break;
        }

        IEnumerable<int> indices = str.IndicesOf(segment.First())
            .Where(x => x != -1);

        foreach (int index in indices)
        {
            StringSegment indexSegment = str.Substring(index, segment.Length);

            if (indexSegment.Equals(segment))
            {
                yield return index;
            }
        }
    }
    
    /// <summary>
    /// Finds all occurrences of a specified StringSegment within a string.
    /// </summary>
    /// <param name="str">The string to be searched.</param>
    /// <param name="segment">The StringSegment to search for.</param>
    /// <returns>An array of Indices for all occurrences of the specified StringSegment within the string; empty if not found within the String Segment.</returns>
    public static int[] IndicesOfAsArray(this string str, StringSegment segment)
    {
        List<int> indices = new();

        if (str.Length < segment.Length || segment.IsEmpty())
            return [];

        IEnumerable<int> indexes = str.IndicesOf(segment.First()).Where(x => x != -1);

        foreach (int index in indexes)
        {
            StringSegment indexSegment = str.Substring(index, segment.Length);

            if (indexSegment.Equals(segment))
            {
                indices.Add(index);
            }
        }
        
        return indices.ToArray();
    }
}*/