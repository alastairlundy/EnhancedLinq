using System.Globalization;
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
    public static TNumber Average<TNumber>(this Span<TNumber> source) where TNumber : INumber<TNumber>
    {
        TNumber sum = source.Sum();

        return sum / source.InternalCount();
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <typeparam name="TNumber"></typeparam>
    /// <returns></returns>
    public static TNumber Average<TNumber>(this Memory<TNumber> source) where TNumber : INumber<TNumber>
        => Average(source.Span);
}