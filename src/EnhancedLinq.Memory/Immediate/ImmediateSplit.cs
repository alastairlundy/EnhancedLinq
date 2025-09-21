/*
    EnhancedLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/.
 */

using System;
using System.Collections.Generic;

namespace AlastairLundy.EnhancedLinq.Memory.Immediate;

public static partial class EnhancedLinqMemoryImmediate
{
    
    /// <summary>
    /// Splits a span into an <see cref="IList{T}"/> of arrays of type <see cref="T"/> based on a maximum number of elements.
    /// </summary>
    /// <param name="span">The span to split.</param>
    /// <param name="count">The maximum number of elements to have in each array.</param>
    /// <typeparam name="T">The type of elements within the span.</typeparam>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public static IList<T[]> SplitByItemCount<T>(this Span<T> span, int count)
    {
        if(span.IsEmpty)
            return Array.Empty<T[]>();
        
        if(count <= 0)
            throw new ArgumentOutOfRangeException(nameof(count));
        
        List<T[]> list = new();

        if (span.Length > count == false)
        {
            return [span.ToArray()];
        }

        int start = 0;
        int nextSplit = 0;

        for (int i = 0; i < span.Length; i++)
        {
            if (start == -1)
            {
                start = i;
            }
            
            nextSplit += 1;

            if (nextSplit - start == count)
            {
                list.Add(span.Slice(start, count).ToArray());
                start = -1;
            }
            else if (i == span.Length - 1 && start != -1)
            {
                list.Add(span.Slice(start, span.Length - start).ToArray());
            }
        }
        
        return list;
    }

    /// <summary>
    /// Splits a span into an <see cref="IList{T}"/> of arrays of type <see cref="T"/> based on the number of processors available.
    /// </summary>
    /// <param name="span">The span to split.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public static IList<T[]> SplitByProcessorCount<T>(this Span<T> span)
        => SplitByItemCount(span, Environment.ProcessorCount);

    /// <summary>
    /// Splits a span into an <see cref="IList{T}"/> of arrays of type <see cref="T"/> based on the number of elements specified.
    /// </summary>
    /// <param name="span">The span to split.</param>
    /// <param name="maximumNumberOfArrays">The desired maximum number of arrays of which to store elements in.</param>
    /// <typeparam name="T">The type of elements within the span.</typeparam>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public static IList<T[]> SplitByArrayCount<T>(this Span<T> span, int maximumNumberOfArrays)
    {
        if (span.IsEmpty)
            return Array.Empty<T[]>();

        double maxItems = Convert.ToDouble(span.Length / maximumNumberOfArrays);
        int maxItemCount;
        
        if (maxItems % 1 != 0)
        {
            maxItemCount = Convert.ToInt32(maxItems) + 1;
        }
        else
        {
            maxItemCount = Convert.ToInt32(maxItems);
        }
        
        return SplitByItemCount(span, maxItemCount);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="span"></param>
    /// <param name="separator"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IList<T[]> SplitBy<T>(this Span<T> span, T separator)
    {
        if (span.IsEmpty)
            return Array.Empty<T[]>();

        return SplitBy(span, x => x is not null && x.Equals(separator));
    }

    /// <summary>
    /// Splits a span into an <see cref="IList{T}"/> of arrays of type <see cref="T"/> based on the provided predicate.
    /// </summary>
    /// <param name="span">The span to split.</param>
    /// <param name="predicate">A function that returns true or false indicating if an element should start a new array.</param>
    /// <typeparam name="T">The type of elements within the span.</typeparam>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public static IList<T[]> SplitBy<T>(this Span<T> span, Func<T, bool> predicate)
    {
        if (span.IsEmpty)
            return Array.Empty<T[]>();

        List<T[]> list = new();

        int start = 0;
        int nextSplit = 0;

        for (int i = 0; i < span.Length; i++)
        {
            if (start == -1)
            {
                start = i;
            }
            
            nextSplit += 1;
            
            if (predicate(span[i]))
            {
                list.Add(span.Slice(start, Math.Abs(nextSplit - start)).ToArray());
                start = -1;
            }
            else if (i == span.Length - 1 && start != -1)
            {
                list.Add(span.Slice(start, span.Length - start).ToArray());
            }
        }
        
        return list;
    }
}