namespace EnhancedLinq.Memory;

/// <summary>
/// 
/// </summary>
/// <typeparam name="TElement"></typeparam>
/// <typeparam name="TKey"></typeparam>
public struct OrderedSpan<TElement, TKey> : IOrderedSpan<TElement, TKey>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="span"></param>
    /// <param name="keySelector"></param>
    /// <param name="comparer"></param>
    /// <param name="descending"></param>
    public OrderedSpan(Span<TElement> span, Func<TElement, TKey> keySelector,
        IComparer<TKey>? comparer, bool descending)
    {
        OrderedResult = span.ToArray();
        PrimaryKeySelector = keySelector;
        Comparer = comparer ?? Comparer<TKey>.Default;
        IsDescending = descending;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="span"></param>
    /// <param name="keySelector"></param>
    /// <param name="comparer"></param>
    /// <param name="descending"></param>
    public OrderedSpan(ReadOnlySpan<TElement> span, Func<TElement, TKey> keySelector,
        IComparer<TKey>? comparer, bool descending)
    {
        OrderedResult = span.ToArray();
        PrimaryKeySelector = keySelector;
        Comparer = comparer ?? Comparer<TKey>.Default;
        IsDescending = descending;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="keySelector"></param>
    /// <param name="comparer"></param>
    /// <param name="descending"></param>
    /// <returns></returns>
    public IOrderedSpan<TElement, TKey> CreateOrderedSpan(Func<TElement, TKey> keySelector,
        IComparer<TKey>? comparer, bool descending)
    {
        comparer ??= Comparer<TKey>.Default;
        
        
        
        Span<TElement> resultSpan = new();
        
        return new OrderedSpan<TElement, TKey>(resultSpan, keySelector, comparer, descending);
    }

    public TElement[] OrderedResult { get; }
    
    public Func<TElement, TKey> PrimaryKeySelector { get; }

    public IComparer<TKey> Comparer { get; }
    
    public bool IsDescending { get; }
}