using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

using ExtraLinq.Internals.Localizations;

namespace ExtraLinq.Immediate.Lists.Ranges;

public static class ImmediateListNumberRange
{
    /// <summary>
    /// Generates a list of unsigned short (ushort) values starting from a specified value and continuing for a specified count,
    /// with each value incremented by 1 from the starting point.
    /// </summary>
    /// <param name="startIndex">The starting value of the list.</param>
    /// <param name="count">The number of values to generate in the list.</param>
    /// <returns>An <see cref="IList{T}"/> containing the generated list of unsigned short values,
    /// incremented by 1 from the starting point.</returns>
    /// <exception cref="ArgumentException">Thrown when the count + startIndex are greater than ushort.MaxValue </exception>
    public static TNumber[] RangeAsArray<TNumber>(this TNumber startIndex, TNumber count) where TNumber : INumber<TNumber>, IMinMaxValue<TNumber>?
    {
        if (startIndex + count >= TNumber.MaxValue)
        {
            throw new ArgumentException(Resources.Exceptions_Count_LessThanZero);
        }

        int arrayCount = int.Parse((count + TNumber.One).ToString() ?? throw new InvalidOperationException());
        
        TNumber[] output = new TNumber[arrayCount];

        int index = 0;
        
        for (TNumber i = startIndex; i < count; i++)
        {
            output[index] = i;
            index++;

        }
        
        return output;
    }
    
    /// <summary>
    /// Generates a list of short (short) values starting from a specified value and continuing for a specified count,
    /// with each value incremented by 1 from the starting point.
    /// </summary>
    /// <param name="startIndex">The starting value of the list.</param>
    /// <param name="count">The number of values to generate in the list.</param>
    /// <returns>An <see cref="IList{T}"/> containing the generated list of short values,
    /// incremented by 1 from the starting point.</returns>
    /// <exception cref="ArgumentException">Thrown if the count is less than zero.</exception>
    public static short[] RangeAsArray(this short startIndex, short count)
    {
        if (startIndex + count > short.MaxValue)
            throw new ArgumentException(Resources.Exceptions_Count_LessThanZero);
        
        if (count < 0)
            throw new ArgumentException(Resources.Exceptions_Count_LessThanZero
                .Replace("{x}", $"{count}"));
        
        short[] output = new short[count + 1];

        int index = 0;
        
        for (short i = startIndex; i < count; i++)
        {
            output[index] = i;
            index++;
        }
        
        return output;
    }
    
    /// <summary>
    /// Returns an enumerable list of integers from <paramref name="startIndex"/> up to start + count.
    /// </summary>
    /// <param name="startIndex">The starting integer of the list.</param>
    /// <param name="count">The number of integers to generate.</param>
    /// <returns>An IList list of integers from the start index up to count.</returns>
    /// <exception cref="ArgumentException">Thrown if the count is less than zero.</exception>
    public static int[] RangeAsArray(this int startIndex, int count)
    {
        if (count < 0)
        {
            throw new ArgumentException(Resources.Exceptions_Count_LessThanZero
                .Replace("{x}", $"{count}"));
        }
        
        int[] output = new int[count + 1];

        int index = 0;
        
        for (int i = startIndex; i < count; i++)
        {
            output[index] = i;
            index++;
        }

        return output;
    }
    
    /// <summary>
    /// Generates a list of long values starting from a specified value and continuing for a specified count,
    /// with each value incremented by 1 from the starting point.
    /// </summary>
    /// <param name="startIndex">The starting value of the list.</param>
    /// <param name="count">The number of values to generate in the list.</param>
    /// <returns>An <see cref="IList{T}"/> containing the generated list of long values,
    /// incremented by 1 from the starting point.</returns>
    /// <exception cref="ArgumentException">Thrown when the count is less than zero.</exception>
    public static long[] RangeAsArray(this long startIndex, long count)
    {
        if (count <= 0)
            throw new ArgumentException(Resources.Exceptions_Count_LessThanZero
                .Replace("{x}", $"{count}"));
        
        long[] output = new long[count + 1];

        int index = 0;
        
        for (long i = startIndex; i < count; i++)
        {
            output[index] = i;
            index++;
        }
        
        return output;
    }
    

    /// <summary>
    /// Generates a list of unsigned integer (uint) values starting from a specified value and continuing for a specified count,
    /// with each value incremented by 1 from the starting point.
    /// </summary>
    /// <param name="startIndex">The starting value of the list.</param>
    /// <param name="count">The number of values to generate in the list.</param>
    /// <returns>An <see cref="IList{T}"/> containing the generated list of unsigned integer values,
    /// incremented by 1 from the starting point.</returns>
    public static uint[] RangeAsArray(this uint startIndex, uint count)
    {
        if (count == 0)
            throw new ArgumentException(Resources.Exceptions_Count_LessThanZero);
        
        uint[] output = new uint[count + 1];
        int index = 0;
        
        for (uint i = startIndex; i < count; i++)
        {
            output[index] = i;
        }
        
        return output;
    }
    
    
    /// <summary>
    /// Generates a list of unsigned long (ulong) values starting from a specified value and continuing for a specified count,
    /// with each value incremented by 1 from the starting point.
    /// </summary>
    /// <param name="startIndex">The starting value of the list.</param>
    /// <param name="count">The number of values to generate in the list.</param>
    /// <returns>An <see cref="IList{T}"/> containing the generated list of unsigned long values,
    /// incremented by 1 from the starting point.</returns>
    public static ulong[] RangeAsArray(this ulong startIndex, ulong count)
    {
        if (count == 0)
            throw new ArgumentException(Resources.Exceptions_Count_LessThanZero);
        
        ulong[] output = new  ulong[count + 1];
        
        int index = 0;
        
        for (ulong i = startIndex; i < count; i++)
        {
            output[index] = i;
        }
        
        return output;
    }

    /// <summary>
    /// Returns an enumerable list of integers from <paramref name="startIndex"/> up to start + count.
    /// </summary>
    /// <param name="startIndex">The starting integer of the list.</param>
    /// <param name="count">The number of integers to generate.</param>
    /// <param name="numbersToSkip">The numbers to skip from the range.</param>
    /// <returns>An IList list of integers from the start index up to count.</returns>
    /// <exception cref="ArgumentException">Thrown if the count is less than zero.</exception>
    public static int[] RangeAsArray(this int startIndex, int count, int[] numbersToSkip)
    {
        if (count <= 0)
            throw new ArgumentException(Resources.Exceptions_Count_LessThanZero
                .Replace("{x}", $"{count}"));

        if (numbersToSkip.Length > count)
            throw new ArgumentException();
        
        if (numbersToSkip.Length == 0)
            return RangeAsArray(startIndex, count);
        
        int[] output = new int[count + 1];
        
        int index = 0;
        
        for (int i = startIndex; i < count; i++)
        {
            if (numbersToSkip.Contains(i) == false)
            {
                output[index] = i;
            }
        }
        
        return output;
    }
    

    /// <summary>
    /// Generates a list of long values starting from a specified value and continuing for a specified count,
    /// while skipping specified numbers in the list, with each value incremented by 1 from the starting point.
    /// </summary>
    /// <param name="startIndex">The starting value of the list.</param>
    /// <param name="count">The number of values to generate in the list.</param>
    /// <param name="numbersToSkip">An <see cref="IList{T}"/> of long values to skip in the generated list.</param>
    /// <returns>An <see cref="IList{T}"/> containing the generated list of long values,
    /// excluding the skipped numbers.</returns>
    /// <exception cref="ArgumentException">Thrown if the count is less than zero.</exception>
    public static long[] RangeAsArray(this long startIndex, long count, long[] numbersToSkip)
    {
        if (count <= 0)
            throw new ArgumentException(Resources.Exceptions_Count_LessThanZero
                .Replace("{x}", $"{count}"));
        
        if (numbersToSkip.Length > (int)count)
            throw new ArgumentException();
        
        if (numbersToSkip.Length == 0)
            return RangeAsArray(startIndex, count);
        
        long[] output = new long[(int)count + 1];
        int index = 0;
        
        for (long i = startIndex; i < count; i++)
        {
            if (numbersToSkip.Contains(i) == false)
            {
                output[index] = i;
            }
        }
        
        return output;
    }
    

    /// <summary>
    /// Generates a list of unsigned long (ulong) values starting from a specified value and continuing for a specified count,
    /// while skipping specified numbers in the list, with each value incremented by 1 from the starting point.
    /// </summary>
    /// <param name="startIndex">The starting value of the list.</param>
    /// <param name="count">The number of values to generate in the list.</param>
    /// <param name="numbersToSkip">An <see cref="IList{T}"/> of unsigned long values to skip in the generated list.</param>
    /// <returns>An <see cref="IList{T}"/> containing the generated list of unsigned long values,
    /// excluding the skipped numbers.</returns>
    public static ulong[] RangeAsArray(this ulong startIndex, ulong count, ulong[] numbersToSkip)
    {
        if (count == 0)
            throw new ArgumentException(Resources.Exceptions_Count_LessThanZero);
        
        if (numbersToSkip.Length > (int)count)
            throw new ArgumentException();
        
        if (numbersToSkip.Length == 0)
            return RangeAsArray(startIndex, count);
        
        ulong[] output = new ulong[(int)count + 1];
        
        int index = 0;
        
        for (ulong i = startIndex; i < count; i++)
        {
            if (numbersToSkip.Contains(i) == false)
            {
                output[index] = i;
                index++;
            }
        }
        
        Array.Resize(ref output, index);

        return output;
    }
    
    
    /// <summary>
    /// Generates a list of unsigned integer (uint) values starting from a specified value and continuing for a specified count,
    /// while skipping specified numbers in the list, with each value incremented by 1 from the starting point.
    /// </summary>
    /// <param name="startIndex">The starting value of the list.</param>
    /// <param name="count">The number of values to generate in the list.</param>
    /// <param name="numbersToSkip">An <see cref="IList{T}"/> of unsigned integer values to skip in the generated list.</param>
    /// <returns>An <see cref="IList{T}"/> containing the generated list of unsigned integer values,
    /// excluding the skipped numbers.</returns>
    public static uint[] RangeAsArray(this uint startIndex, uint count, uint[] numbersToSkip)
    {
        if (count == 0)
            throw new ArgumentException(Resources.Exceptions_Count_LessThanZero);
        
        uint[] output = new uint[(int)count + 1];

        if (numbersToSkip.Length > (int)count)
            throw new ArgumentException();
        
        if (numbersToSkip.Length == 0)
            return RangeAsArray(startIndex, count);
        
        int index = 0;
        
        for (uint i = startIndex; i < count; i++)
        {
            if (numbersToSkip.Contains(i) == false)
            {
                output[index] = i;
                index++;
            }
        }
        
        Array.Resize(ref output, index);

        return output;
    }
}