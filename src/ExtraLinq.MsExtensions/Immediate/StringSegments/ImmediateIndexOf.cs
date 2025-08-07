using System.Collections.Generic;
using AlastairLundy.DotExtensions.MsExtensions.System.StringSegments;
using Microsoft.Extensions.Primitives;

namespace ExtraLinq.MsExtensions.Immediate.StringSegments;

public static class ImmediateIndexOf
{
    /// <summary>
    /// Finds the index of a specified StringSegment within another StringSegment.
    /// </summary>
    /// <param name="this">The StringSegment to be searched.</param>
    /// <param name="segment">The StringSegment to search for.</param>
    /// <returns>The index at which the specified StringSegment can be found, or -1 if not found.</returns>
    public static int IndexOf(this StringSegment @this, StringSegment segment)
    {
        if (@this.Length < segment.Length || segment.Length == 0)
            return -1;
        
        IEnumerable<int> indexes = @this.IndicesOf(segment.First())
            .Where(x  => x != -1);

        foreach (int index in indexes)
        {
            StringSegment indexSegment = @this.Substring(index, segment.Length);

            if (indexSegment.Equals(segment))
            {
                return index;
            }
        }

        return -1;
    }
    
    /// <summary>
    /// Finds the index of a specified StringSegment within a string.
    /// </summary>
    /// <param name="str">The string to be searched.</param>
    /// <param name="segment">The StringSegment to search for.</param>
    /// <returns>The index at which the specified StringSegment can be found, or -1 if not found.</returns>
    public static int IndexOf(this string str, StringSegment segment)
    {
        if (str.Length < segment.Length || segment.Length == 0)
            return -1;
        
        IEnumerable<int> indexes = str.IndicesOf(segment.First()).Where(x  => x != -1);

        foreach (int index in indexes)
        {
            StringSegment indexSegment = str.Substring(index, segment.Length);

            if (indexSegment.Equals(segment))
            {
                return index;
            }
        }

        return -1;
    }
}