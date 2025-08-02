using System;
using Microsoft.Extensions.Primitives;

using AlastairLundy.DotExtensions.MsExtensions.System.StringSegments;

namespace ExtraLinq.MsExtensions.Immediate.StringSegments;

public static class ImmediateSegmentLast
{
        /// <summary>
    /// Returns the last char in the StringSegment.
    /// </summary>
    /// <param name="target">The StringSegment to be searched.</param>
    /// <returns>The last char in the StringSegment.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the StringSegment contains zero chars.</exception>
    public static char Last(this StringSegment target)
    {
        if (target.IsEmpty())
            throw new InvalidOperationException(Resources.Exceptions_Enumerables_InvalidOperation_EmptySequence);

#if NET6_0_OR_GREATER
            return target[^1];
#else
        // ReSharper disable once UseIndexFromEndExpression
        return target[target.Length - 1];
#endif
    }

    /// <summary>
    /// Returns the last character of the specified <see cref="StringSegment"/> that meets the predicate condition.
    /// </summary>
    /// <param name="target">The StringSegment to be searched.</param>
    /// <param name="predicate">The predicate func condition to be checked against each char in the StringSegment.</param>
    /// <returns>The last character of the segment that meets the predicate condition if any match.</returns>
    /// <exception cref="ArgumentException">Thrown if no characters in the StringSegment meet the predicate condition.</exception>
    public static char Last(this StringSegment target, Func<char, bool> predicate)
    {
        return target.Reverse().First(predicate);
    }

    /// <summary>
    /// Returns the last character of the specified <see cref="StringSegment"/> that meets the predicate condition or a null if the segment is empty.
    /// </summary>
    /// <param name="target">The <see cref="StringSegment"/> from which to retrieve the last character.</param>
    /// <returns>The last character of the segment if it contains any characters; otherwise, null.</returns>
    public static char? LastOrDefault(this StringSegment target)
    {
        if (target.IsEmpty())
        {
            return null;
        }
        
#if NET6_0_OR_GREATER || NETSTANDARD2_1
        return target[^1];
#else
        return target[target.Length - 1];
#endif
    }

    /// <summary>
    /// Returns the last character of the specified <see cref="StringSegment"/> that matches the predicate condition or a default value if the segment is empty.
    /// </summary>
    /// <param name="target">The StringSegment to be searched.</param>
    /// <param name="predicate">The predicate func condition to be checked against each char in the StringSegment.</param>
    /// <returns>The last character of the segment that meets the predicate condition if any match; otherwise, null.</returns>
    public static char? LastOrDefault(this StringSegment target, Func<char, bool> predicate)
    {
        return target.Reverse().FirstOrDefault(predicate);
    }
}