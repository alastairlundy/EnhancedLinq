using System;
using Microsoft.Extensions.Primitives;

namespace ExtraLinq.MsExtensions.Immediate.StringSegments;

public static class ImmediateSegmentCount
{
    /// <summary>
    /// Counts the number of chars in the StringSegment that match the predicate.
    /// </summary>
    /// <param name="target">The StringSegment to search.</param>
    /// <param name="selector">The predicate to check each char against.</param>
    /// <returns>The number of chars matching the predicate condition as an integer.</returns>
    public static int Count(this StringSegment target,  Func<char, bool> selector)
    {
        int output = 0;

        for (int i =  0; i < target.Length; i++)
        {
            if (selector(target[i])) 
                output++;
        }
            
        return output;
    }

}