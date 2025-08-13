using System.Collections.Generic;
using System.Linq;
using AlastairLundy.DotExtensions.MsExtensions.System.StringSegments;

using ExtraLinq.MsExtensions.Deferred;

using Microsoft.Extensions.Primitives;

namespace ExtraLinq.MsExtensions.Immediate;

public static class ImmediateSegmentIndices
{
    
    
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

        IEnumerable<int> indexes = @this.IndicesOf(segment.First())
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
}