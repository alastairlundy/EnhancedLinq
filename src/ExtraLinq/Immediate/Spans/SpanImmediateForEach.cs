using System;

namespace ExtraLinq.Immediate.Spans;

public static class SpanImmediateForEach
{
    /// <summary>
    /// Applies the given action to each element of this Span.
    /// </summary>
    /// <param name="action">The action to apply to each element in the span.</param>
    /// <param name="target">The span to apply the elements to.</param>
    /// <typeparam name="T">The type of items in the Span.</typeparam>
    public static void ForEach<T>(this ref Span<T> target, Action<T> action)
    {
        for (int index = 0; index < target.Length; index++)
        {
            action.Invoke(target[index]);
        }
    }

    /// <summary>
    /// Applies the given func to each element of this Span.
    /// </summary>
    /// <param name="target">The span to apply the elements to.</param>
    /// <param name="action">The func to apply to each element in the span.</param>
    /// <typeparam name="T">The type of items in the Span.</typeparam>
    public static void ForEach<T>(this Span<T> target, Func<T, T> action)
    {
        for (int i = 0; i < target.Length; i++)
        {
            target[i] = action.Invoke(target[i]);
        }
    }
}