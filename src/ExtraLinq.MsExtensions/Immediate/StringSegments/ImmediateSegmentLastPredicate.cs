using System;
using Microsoft.Extensions.Primitives;

namespace ExtraLinq.MsExtensions.Immediate.StringSegments;

public static class ImmediateSegmentLastPredicate
{


    /// <summary>
    /// Returns the last character of the specified <see cref="StringSegment"/> that meets the predicate condition.
    /// </summary>
    /// <param name="target">The StringSegment to be searched.</param>
    /// <param name="predicate">The predicate func condition to be checked against each char in the StringSegment.</param>
    /// <returns>The last character of the segment that meets the predicate condition if any match.</returns>
    /// <exception cref="ArgumentException">Thrown if no characters in the StringSegment meet the predicate condition.</exception>
    public static char Last(this StringSegment target, Func<char, bool> predicate) => 
        target.Reverse().First(predicate);

    /// <summary>
    /// Returns the last character of the specified <see cref="StringSegment"/> that matches the predicate condition or a default value if the segment is empty.
    /// </summary>
    /// <param name="target">The StringSegment to be searched.</param>
    /// <param name="predicate">The predicate func condition to be checked against each char in the StringSegment.</param>
    /// <returns>The last character of the segment that meets the predicate condition if any match; otherwise, null.</returns>
    public static char? LastOrDefault(this StringSegment target, Func<char, bool> predicate) 
        => target.Reverse().FirstOrDefault(predicate);
}