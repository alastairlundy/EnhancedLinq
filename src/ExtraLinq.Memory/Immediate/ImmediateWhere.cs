namespace ExtraLinq.Memory.Immediate;

public static class ImmediateWhere
{
    /// <summary>
    /// Returns a new Span with all items in the Span that match the predicate condition.
    /// </summary>
    /// <param name="target">The Span to be searched.</param>
    /// <param name="predicate">The predicate func to be invoked on each item in the Span.</param>
    /// <typeparam name="T">The type of items stored in the span.</typeparam>
    /// <returns>A new Span with the items that match the predicate condition.</returns>
    public static Span<T> Where<T>(this Span<T> target, Func<T, bool> predicate)
    {
        List<T> list;

        if (target.Length <= 100)
            list = new(capacity: target.Length);
        else
            list = new();
        
        foreach (T item in target)
        {
            if (predicate.Invoke(item))
            {
                list.Add(item);
            }
        }
        
        return new Span<T>(list.ToArray());
    }
}