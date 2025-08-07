using System.Diagnostics.CodeAnalysis;

using AlastairLundy.DotPrimitives.Collections.Groupings;

namespace ExtraLinq.Memory.Immediate;

public static class ImmediateGroupBy
{
    /// <summary>
    /// Groups the elements of the source span by a specified key selector function.
    /// </summary>
    /// <param name="source">The source span to group elements from.</param>
    /// <param name="keySelector">A function to extract the key for each element.</param>
    /// <typeparam name="TKey">The type of the key returned by the key selector function.</typeparam>
    /// <typeparam name="TElement">The type of elements in the source span.</typeparam>
    /// <returns>A span of groups, each containing a key and the elements that share that key.</returns>
    public static Span<IGrouping<TKey, TElement>> GroupBy<TKey, TElement>(
#if NET5_0_OR_GREATER
        [NotNull]
#endif
        this Span<TElement> source,
#if NET5_0_OR_GREATER
        [NotNull]
#endif
        Func<TElement, TKey> keySelector) where TKey : notnull
    {
        Dictionary<TKey, List<TElement>> dictionary = new();

        foreach (TElement item in source)
        {
            TKey key = keySelector.Invoke(item);
            
            if (dictionary.ContainsKey(key))
            {
                dictionary[key].Add(item);
            }
            else
            {
                dictionary.Add(key, new List<TElement>());
                dictionary[key].Add(item);
            }
        }

        IEnumerable<IGrouping<TKey, TElement>> groups = (from kvp in dictionary
            select new GroupingEnumerable<TKey, TElement>(kvp.Key, kvp.Value));
        
        return new  Span<IGrouping<TKey, TElement>>(groups.ToArray());
    }
}