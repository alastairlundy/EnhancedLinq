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
   

using System;

using System.Linq;

using Microsoft.Extensions.Primitives;

namespace AlastairLundy.DotExtensions.MsExtensions.System.StringSegments;

public static class SegmentSplitExtensions
{
    
    /// <summary>
    /// Splits a StringSegment into StringSegment substrings using a specified separator.
    /// </summary>
    /// <param name="segment">The input StringSegment.</param>
    /// <param name="separator">The separator to delimit the StringSegment substrings in the StringSegment.</param>
    /// <returns>An array of StringSegment substrings, from this StringSegment instance that is delimited by the separator.</returns>
    public static StringSegment[] Split(this StringSegment segment, StringSegment separator)
    {
        if (segment.Contains(separator) == false)
            return [];

        int[] indices = segment.IndicesOfAsArray(separator);
        
        StringSegment[] output = new StringSegment[indices.Length];
        
        if (indices.First().Equals(-1))
            return [segment];

        int outputIndex = 0;
        int start = 0;

        for (int i = 0; i < indices.Length; i++)
        {
            if (indices.Any(x => x == i))
            {
                int end = i > 0 ? i - 1 : 0;

                StringSegment newSegment = segment.Substring(start, Math.Abs(end - start));

                output[outputIndex] = newSegment;
                outputIndex++;
                start = i;
            }
        }
        
        if(outputIndex < output.Length)
            Array.Resize(ref output, outputIndex);

        return output;
    }
}*/