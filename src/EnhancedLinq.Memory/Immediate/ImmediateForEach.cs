/*
    EnhancedLinq.Memory
    Copyright (c) 2025-2026 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
    */


// ReSharper disable ForCanBeConvertedToForeach

namespace EnhancedLinq.Memory.Immediate;

public static partial class EnhancedLinqMemoryImmediate
{
    /// <param name="target">The span to apply the action to.</param>
    /// <typeparam name="T">The type of items in the Span.</typeparam>
    extension<T>(ref Span<T> target)
    {
        /// <summary>
        /// Applies the given action to each element of this Span.
        /// </summary>
        /// <param name="action">The action to apply to each element in the span.</param>
        public void ForEach(Action<T> action)
        {
            InvalidOperationException.ThrowIfSpanIsEmpty(target);
            ArgumentNullException.ThrowIfNull(action);

            for (int index = 0; index < target.Length; index++)
            {
                action.Invoke(target[index]);
            }
        }
    }

    /// <param name="target">The span to apply the action to.</param>
    /// <typeparam name="T">The type of items in the Span.</typeparam>
    extension<T>(Span<T> target)
    {
        /// <summary>
        /// Applies the given func to each element of this Span.
        /// </summary>
        /// <param name="action">The func to apply to each element in the span.</param>
        public void ForEach(Func<T, T> action)
        {
            InvalidOperationException.ThrowIfSpanIsEmpty(target);
            ArgumentNullException.ThrowIfNull(action);
        
            for (int i = 0; i < target.Length; i++)
            {
                target[i] = action.Invoke(target[i]);
            }
        }
    }
    
    /// <param name="target">The memory to apply the action to.</param>
    /// <typeparam name="T">The type of items in the Memory.</typeparam>
    extension<T>(ref Memory<T> target)
    {
        /// <summary>
        /// Applies the given action to each element of this Memory.
        /// </summary>
        /// <param name="action">The action to apply to each element in the memory.</param>
        public void ForEach(Action<T> action)
        {
            InvalidOperationException.ThrowIfMemoryIsEmpty(target);
            ArgumentNullException.ThrowIfNull(action);
            
            T[] array = new T[target.Length];

            for (int index = 0; index < target.Length; index++)
            {
                T item = target.ElementAt(index);
            
                action.Invoke(item);
                array[index] = item;
            }

            target = new(array);
        }

        /// <summary>
        /// Applies the given action to each element of this Memory.
        /// </summary>
        /// <param name="action">The action to apply to each element in the memory.</param>
        public void ForEach(Func<T, T> action)
        {
            InvalidOperationException.ThrowIfMemoryIsEmpty(target);
            ArgumentNullException.ThrowIfNull(action);
            
            T[] array = new T[target.Length];

            for (int index = 0; index < target.Length; index++)
            {
                T item = target.ElementAt(index);

                array[index] = action.Invoke(item);
            }

            target = new(array);
        }
    }
}