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
    public static TNumber Minimum<TNumber>(this Span<TNumber> source) where TNumber : INumber<TNumber>
    {
        TNumber total = source[0];

        foreach (TNumber item in source)
        {
            if (item <= total)
            {
                total = item;
            }
        }

        return total;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <typeparam name="TNumber"></typeparam>
    /// <returns></returns>
    public static TNumber Maximum<TNumber>(this Span<TNumber> source) where TNumber : INumber<TNumber>
    {
        TNumber total = TNumber.Zero;

        foreach (TNumber item in source)
        {
            if (item > total)
            {
                total = item;
            }
        }

        return total;
    }
}