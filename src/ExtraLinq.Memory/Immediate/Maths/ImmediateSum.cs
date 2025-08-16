using System.Numerics;

namespace ExtraLinq.Memory.Immediate.Maths;

public static partial class ExtraLinqMemoryImmediateMaths
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <typeparam name="TNumber"></typeparam>
    /// <returns></returns>
    public static TNumber Sum<TNumber>(this Span<TNumber> source) where TNumber : INumber<TNumber>
    {
        TNumber total = TNumber.Zero;

        foreach (TNumber item in source)
        {
            total += item;
        }

        return total;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <typeparam name="TNumber"></typeparam>
    /// <returns></returns>
    public static TNumber Sum<TNumber>(this Memory<TNumber> source) where TNumber : INumber<TNumber>
        => Sum<TNumber>(source.Span);
}