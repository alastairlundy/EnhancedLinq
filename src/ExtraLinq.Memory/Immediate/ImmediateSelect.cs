using System.Diagnostics.CodeAnalysis;

namespace ExtraLinq.Memory.Immediate;

public static class ImmediateSelect
{
    /// <summary>
    /// Transforms elements of a Span according to behaviour defined by the Selector.
    /// </summary>
    /// <param name="source">The span to search.</param>
    /// <param name="selector">The selector to use.</param>
    /// <typeparam name="TSource">The type of elements in the source Span.</typeparam>
    /// <typeparam name="TResult">The type of elements the selector transforms elements into.</typeparam>
    /// <returns>The newly created Span with the elements transformed by the selector.</returns>
    public static Span<TResult> Select<TSource, TResult>(
        [NotNull]
        this Span<TSource> source,
        [NotNull]
        Func<TSource, TResult> selector)
    {
        TResult[] array = new  TResult[source.Length];
        
        int index = 0;
        
        source.ForEach(x =>
        {
            array[index] = selector.Invoke(x);
            index++;
        });

        return new Span<TResult>(array);
    }
}