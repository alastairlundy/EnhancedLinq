/*
    ExtraLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/.
 */

namespace ExtraLinq.Memory.Immediate.Ranges;

public static partial class ExtraLinqMemoryImmediateRange
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
    /// <param name="range"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static Span<T> RemoveRange<T>(this Span<T> target, Range range)
        => RemoveRange(target, range.Start.Value, range.End.Value);

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
        if (target.IsEmpty)
            throw new ArgumentException();


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