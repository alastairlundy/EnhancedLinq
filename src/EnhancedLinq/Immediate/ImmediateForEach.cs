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

/// <summary>
/// Provides additional immediate execution Linq style methods.
/// </summary>
public static partial class EnhancedLinqImmediate
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="target">The sequence to apply the action for.</param>
    /// <typeparam name="T">The type of elements in the sequence.</typeparam>
    extension<T>(IEnumerable<T> target)
    {
        /// <summary>
        /// Applies the given action for each element of this sequence.
        /// </summary>
        /// <param name="action">The action to apply to each element in the sequence.</param>

        public void ForEach(Action<T> action)
        {
            ArgumentNullException.ThrowIfNull(target);
            ArgumentNullException.ThrowIfNull(action);
        
            foreach (T item in target)
            {
                action.Invoke(item);
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="target">The <see cref="ICollection{T}"/> to apply the predicate to.</param>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    extension<T>(ICollection<T> target)
    {
        /// <summary>
        /// Applies the given predicate function to each element of this <see cref="ICollection{T}"/>.
        /// </summary>
        /// <param name="predicate">The func to apply to each element in the collection.</param>
        /// <returns>A new <see cref="ICollection{T}"/> containing the result of applying each element to the predicate.</returns>
        public ICollection<T> ForEach(Func<T, T> predicate)
        {
            ArgumentNullException.ThrowIfNull(target);
            ArgumentNullException.ThrowIfNull(predicate);
        
            List<T> output = new List<T>(capacity:target.Count);
        
            foreach (T item in target)
            {
                output.Add(predicate.Invoke(item));
            }
        
            return output;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="target">The <see cref="IList{T}"/> to apply the selector to.</param>
    /// <typeparam name="T">The type of items in the <see cref="IList{T}"/>.</typeparam>
    extension<T>(IList<T> target)
    {
        /// <summary>
        /// Applies the given predicate function to each element of this <see cref="IList{T}"/>.
        /// </summary>
        /// <param name="predicate">The func to apply to each element in the <see cref="IList{T}"/>.</param>
        public void ForEach(Func<T, T> predicate)
        {
            ArgumentNullException.ThrowIfNull(target);
            ArgumentNullException.ThrowIfNull(predicate);
        
            for (int index = 0; index < target.Count; index++)
            {
                target[index] = predicate.Invoke(target[index]);
            }
        }
    }
}