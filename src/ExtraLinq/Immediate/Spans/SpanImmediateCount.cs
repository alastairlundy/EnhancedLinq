using System;

namespace ExtraLinq.Immediate.Spans;

public static class SpanImmediateCount
{
    /// <summary>
    /// Returns the number of elements in a given span that satisfy a condition.
    /// </summary>
    /// <param name="source">The span to search.</param>
    /// <param name="predicate">A func that takes an element and returns a boolean indicating whether it should be counted.</param>
    /// <typeparam name="TSource">The type of elements in the span.</typeparam>
    /// <returns>The number of elements that satisfy the predicate.</returns>
    public static int Count<TSource>(this Span<TSource> source,
        Func<TSource, bool> predicate)
    {
        int count = 0;

        foreach (TSource item in source)
        {
            if (predicate.Invoke(item))
            {
                count++;
            }
        }
        
        return count;
    }
}