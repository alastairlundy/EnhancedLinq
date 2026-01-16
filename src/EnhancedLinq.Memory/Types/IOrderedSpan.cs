namespace EnhancedLinq.Memory;

/// <summary>
/// 
/// </summary>
/// <typeparam name="TElement"></typeparam>
/// <typeparam name="TKey"></typeparam>
public interface IOrderedSpan<out TElement, TKey>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="keySelector"></param>
    /// <param name="comparer"></param>
    /// <param name="descending"></param>
    /// <typeparam name="TKey"></typeparam>
    /// <returns></returns>
    IOrderedSpan<TElement, TKey> CreateOrderedSpan(Func<TElement, TKey> keySelector,
        IComparer<TKey>? comparer, bool descending);
    
    /// <summary>
    /// 
    /// </summary>
    public TElement[] OrderedResult { get; }
}