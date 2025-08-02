using System;
using System.Collections.Generic;

using ExtraLinq.MsExtensions.Deferred;

using Microsoft.Extensions.Primitives;

using AlastairLundy.DotExtensions.MsExtensions.System.StringSegments;

namespace ExtraLinq.MsExtensions.Immediate.StringSegments;

public static class ImmediateSegmentFirst
{
    
    /// <summary>
    /// Returns the first char in the StringSegment.
    /// </summary>
    /// <param name="target">The StringSegment to be searched.</param>
    /// <returns>The first char in the StringSegment.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the StringSegment contains zero chars.</exception>
    public static char First(this StringSegment target)
    {
        if (target.IsEmpty())
            throw new InvalidOperationException(Resources.Exceptions_Enumerables_InvalidOperation_EmptySequence);
        
        return target[0];
    }

    /// <summary>
    /// Returns the first char in the StringSegment that matches the predicate condition.
    /// </summary>
    /// <param name="target">The StringSegment to be searched.</param>
    /// <param name="predicate">The predicate func condition to be checked against each char in the StringSegment.</param>
    /// <returns>The first char in the StringSegment that matches the predicate condition.</returns>
    /// <exception cref="ArgumentException">Thrown if no characters in the StringSegment meet the predicate condition.</exception>
    public static char First(this StringSegment target, Func<char, bool> predicate)
    {
        IEnumerable<char> results = (from c in target
            where predicate(c)
            select c);

        foreach (char result in results)
        {
            return result;
        }
        
        throw new ArgumentException(Resources.Exceptions_StringSegment_NoPredicateMatches);
    }

    /// <summary>
    /// Returns the first character of the specified <see cref="StringSegment"/> or null if the segment is empty.
    /// </summary>
    /// <param name="target">The <see cref="StringSegment"/> from which to retrieve the first character.</param>
    /// <returns>The first character of the segment if it exists; otherwise, null.</returns>
    public static char? FirstOrDefault(this StringSegment target)
    {
        if (StringSegment.IsNullOrEmpty(target))
            return default;

        return target[0];
    }

    /// <summary>
    /// Returns the first character of the specified <see cref="StringSegment"/> that meets the predicate condition or null if the segment is empty.
    /// </summary>
    /// <param name="target">The StringSegment to be searched.</param>
    /// <param name="predicate">The predicate func condition to be checked against each char in the StringSegment.</param>
    /// <returns>The first character of the segment that meets the predicate condition if any match; otherwise, null.</returns>
    public static char? FirstOrDefault(this StringSegment target, Func<char, bool> predicate)
    {
        IEnumerable<char> results = (from c in target
            where predicate(c)
            select c);

        foreach (char result in results)
        {
            return result;
        }
        
        return default;
    }
}