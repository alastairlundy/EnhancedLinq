namespace ExtraLinq.Memory.Immediate.Ranges;

public static class ImmediateRemoveRange
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="target"></param>
    /// <param name="indices"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static Span<T> RemoveRange<T>(this Span<T> target, ICollection<int> indices) where T : IEquatable<T>?
    {
        T[] elements = target.GetRange(indices)
            .ToArray();

        return (from item in target
            where elements.Contains(item) == false
            select item);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="target"></param>
    /// <param name="startIndex"></param>
    /// <param name="count"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static Span<T> RemoveRange<T>(this Span<T> target, int startIndex, int count)
    {
#if NET8_0_OR_GREATER
        if (target.IsEmpty)
            throw new ArgumentException();
#endif

        if (startIndex < 0 || startIndex > target.Length)
            throw new IndexOutOfRangeException();
        
        if(count < 0 || count > target.Length)
            throw new ArgumentOutOfRangeException(nameof(count));
        
        T[] output = new T[target.Length - (startIndex + count)];
        int index = 0;

        for (int i = 0; i < target.Length; i++)
        {
            if (i < startIndex && i > startIndex + count)
            {
                output[index] = target[i];
                index++;
            }
        }
        
        return new Span<T>(output);
    }
}