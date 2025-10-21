/*
      EnhancedLinq 
      Copyright (c) 2025 Alastair Lundy
      
     Licensed under the Apache License, Version 2.0 (the "License");
     you may not use this file except in compliance with the License.
     You may obtain a copy of the License at

         http://www.apache.org/licenses/LICENSE-2.0

     Unless required by applicable law or agreed to in writing, software
     distributed under the License is distributed on an "AS IS" BASIS,
     WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
     See the License for the specific language governing permissions and
     limitations under the License.
 */

using System;
// ReSharper disable ForCanBeConvertedToForeach

namespace AlastairLundy.EnhancedLinq.Memory.Immediate;

public static partial class EnhancedLinqMemoryImmediate
{
    /// <summary>
    /// Applies the given action to each element of this Span.
    /// </summary>
    /// <param name="action">The action to apply to each element in the span.</param>
    /// <param name="target">The span to apply the action to.</param>
    /// <typeparam name="T">The type of items in the Span.</typeparam>
    public static void ForEach<T>(this ref Span<T> target, Action<T> action)
    {
        for (int index = 0; index < target.Length; index++)
        {
            action.Invoke(target[index]);
        }
    }

    /// <summary>
    /// Applies the given func to each element of this Span.
    /// </summary>
    /// <param name="target">The span to apply the action to.</param>
    /// <param name="action">The func to apply to each element in the span.</param>
    /// <typeparam name="T">The type of items in the Span.</typeparam>
    public static void ForEach<T>(this Span<T> target, Func<T, T> action)
    {
        for (int i = 0; i < target.Length; i++)
        {
            target[i] = action.Invoke(target[i]);
        }
    }


    /// <summary>
    /// Applies the given action to each element of this Memory.
    /// </summary>
    /// <param name="target">The memory to apply the action to.</param>
    /// <param name="action">The action to apply to each element in the memory.</param>
    /// <typeparam name="T">The type of items in the Memory.</typeparam>
    public static void ForEach<T>(this ref Memory<T> target, Action<T> action)
    {
        T[] array = new T[target.Length];

        for (int index = 0; index < target.Length; index++)
        {
            T item = target.ElementAt(index);
            
            action.Invoke(item);
            array[index] = item;
        }

        target = new Memory<T>(array);
    }

    /// <summary>
    /// Applies the given action to each element of this Memory.
    /// </summary>
    /// <param name="target">The memory to apply the action to.</param>
    /// <param name="action">The action to apply to each element in the memory.</param>
    /// <typeparam name="T">The type of items in the Memory.</typeparam>
    public static void ForEach<T>(this ref Memory<T> target, Func<T, T> action)
    {
        T[] array = new T[target.Length];

        for (int index = 0; index < target.Length; index++)
        {
            T item = target.ElementAt(index);

            array[index] = action.Invoke(item);
        }

        target = new Memory<T>(array);
    }
}