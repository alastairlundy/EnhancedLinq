using ExtraLinq.Memory.Internals.Localizations;

namespace ExtraLinq.Memory.Immediate;

public static class ImmediateFirstAndLast
{
      /// <summary>
    /// Returns the first element in the Span.
    /// </summary>
    /// <param name="target">The span to be searched.</param>
    /// <typeparam name="T">The type of items stored in the span.</typeparam>
    /// <returns>The first item in the span if any items are in the Span.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the Span contains zero items.</exception>
    public static T First<T>(this Span<T> target)
    {
        if (target.IsEmpty)
            throw new InvalidOperationException(Resources.Exceptions_InvalidOperation_EmptySpan);
        
        return target[0];
    }

    /// <summary>
    /// Returns the first element of a span that satisfies a specified condition, or null if the Span is empty.
    /// </summary>
    /// <param name="target">The span to search for the first element.</param>
    /// <typeparam name="T">The type of elements in the span.</typeparam>
    /// <returns>The first element of the span that satisfies the condition, or null if the span is empty.</returns>
    public static T? FirstOrDefault<T>(this Span<T> target) 
        => target.IsEmpty == false ? target[0] : default;

    /// <summary>
    /// Returns the last element in the Span.
    /// </summary>
    /// <param name="target">The span to be searched.</param>
    /// <typeparam name="T">The type of items stored in the span.</typeparam>
    /// <returns>The last item in the span if any items are in the Span.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the Span contains zero items.</exception>
    public static T Last<T>(this Span<T> target)
    {
        if (target.IsEmpty)
            throw new InvalidOperationException(Resources.Exceptions_InvalidOperation_EmptySpan);

        return target.Length > 1 ? target[^1] : target.First();
    }

    /// <summary>
    /// Returns the last element of a span that satisfies a specified condition,
    /// or null if the Span is empty.
    /// </summary>
    /// <param name="target">The span to search for the last element.</param>
    /// <typeparam name="T">The type of elements in the span.</typeparam>
    /// <returns>The last element of the span, or null if the span is empty.</returns>
    public static T? LastOrDefault<T>(this Span<T> target)
    {
        if (target.IsEmpty)
            return default;
        
        return target.Length > 1 ? target[^1] : target.FirstOrDefault();
    }
}