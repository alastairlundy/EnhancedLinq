/*
    EnhancedLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/.
 */

using System;
using System.Collections.Generic;

namespace AlastairLundy.EnhancedLinq.Immediate;

public static partial class EnhancedLinqImmediate
{
    /// <summary>
    /// Applies the given action for each element of this sequence.
    /// </summary>
    /// <param name="action">The action to apply to each element in the sequence.</param>
    /// <param name="target">The sequence to apply the action for.</param>
    /// <typeparam name="T">The type of elements in the sequence.</typeparam>
    public static void ForEach<T>(this IEnumerable<T> target, Action<T> action)
    {
        ArgumentNullException.ThrowIfNull(target);

        foreach (T item in target)
        {
            action.Invoke(item);
        }
    }

    /// <summary>
    /// Applies the given selector function to each element of this <see cref="ICollection{T}"/>.
    /// </summary>
    /// <param name="target">The <see cref="ICollection{T}"/> to apply the selector to.</param>
    /// <param name="selector">The func to apply to each element in the collection.</param>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <returns>A new <see cref="ICollection{T}"/> containing the result of applying each element to the selector.</returns>
    public static ICollection<T> ForEach<T>(this ICollection<T> target, Func<T, T> selector)
    {
        ArgumentNullException.ThrowIfNull(target);
        
        List<T> output = new List<T>(capacity:target.Count);
        
        foreach (T item in target)
        {
            output.Add(selector.Invoke(item));
        }
        
        return output;
    }
    
    /// <summary>
    /// Applies the given selector function to each element of this <see cref="IList{T}"/>.
    /// </summary>
    /// <param name="target">The <see cref="IList{T}"/> to apply the selector to.</param>
    /// <param name="selector">The func to apply to each element in the <see cref="IList{T}"/>.</param>
    /// <typeparam name="T">The type of items in the <see cref="IList{T}"/>.</typeparam>
    public static void ForEach<T>(this IList<T> target, Func<T, T> selector)
    {
        ArgumentNullException.ThrowIfNull(target);

        for (int index = 0; index < target.Count; index++)
        {
            target[index] = selector.Invoke(target[index]);
        }
    }
}