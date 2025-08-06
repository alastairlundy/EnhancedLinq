using System;
using System.Collections.Generic;
using System.Linq;

using AlastairLundy.DotExtensions.MsExtensions.System.StringSegments;
using ExtraLinq.MsExtensions.Internals.Localizations;
using Microsoft.Extensions.Primitives;

namespace ExtraLinq.MsExtensions.Immediate.StringSegments;

public static class ImmediateSegmentReverse
{
    /// <summary>
    /// Reverses the contents of the StringSegment.
    /// </summary>
    /// <param name="target">The StringSegment to reverse.</param>
    /// <returns>The reversed StringSegment.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the target StringSegment is Empty.</exception>
    public static StringSegment Reverse(this StringSegment target)
    {
        if (target.IsEmpty())
            throw new InvalidOperationException(Resources.Exceptions_Segments_InvalidOperation_EmptySequence);
        
        char[] array = target.ToCharArray();
        
        IEnumerable<int> indices = Enumerable.Range(0, target.Length);
        
        IEnumerable<char> reversedEnumerable = (from c in array
                join i in indices
                    on c equals target[i]
                orderby i descending 
                select c
            );
        
        return new StringSegment(string.Join("", reversedEnumerable));
    }
}