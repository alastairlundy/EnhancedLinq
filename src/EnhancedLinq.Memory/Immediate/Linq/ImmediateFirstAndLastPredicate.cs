/*
    EnhancedLinq.Memory
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
    */

namespace EnhancedLinq.Memory.Immediate;

public static partial class EnhancedLinqMemoryImmediate
{
    /// <param name="target">The span to search for the first element.</param>
    /// <typeparam name="T">The type of elements in the span.</typeparam>
    extension<T>(Span<T> target)
    {
        /// <summary>
        /// Returns the first element of a <see cref="Span{T}"/> that satisfies a specified condition.
        /// </summary>
        /// <param name="predicate">A function that defines the condition to be met.</param>
        /// <returns>The first element of the <see cref="Span{T}"/> that satisfies the condition.</returns>
        /// <exception cref="ArgumentException">Thrown when no element satisfies the condition.</exception>
        public T First(Func<T, bool> predicate)
        {
            ArgumentNullException.ThrowIfNull(predicate);

            foreach (T item in target)
            {
                if (predicate.Invoke(item))
                    return item;
            }

            throw new ArgumentException();
        }

        /// <summary>
        /// Returns the first element of a <see cref="Span{T}"/> that satisfies a specified condition,
        /// or a default value if no such element is found.
        /// </summary>
        /// <param name="predicate">A function that defines the condition to be met.</param>
        /// <returns>The first element of the <see cref="Span{T}"/> that satisfies the condition, or null if no such element is found.</returns>
        /// <exception cref="ArgumentException">Thrown when the <see cref="Span{T}"/> is empty or no element satisfies the condition.</exception>
        public T? FirstOrDefault(Func<T, bool> predicate)
        {
            ArgumentNullException.ThrowIfNull(predicate);

            foreach (T item in target)
            {
                if (predicate.Invoke(item))
                    return item;
            }

            return default;
        }

        /// <summary>
        /// Returns the last element of a <see cref="Span{T}"/> that satisfies a specified condition.
        /// </summary>
        /// <param name="predicate">A function that defines the condition to be met.</param>
        /// <returns>The last element of the <see cref="Span{T}"/> that satisfies the condition.</returns>
        /// <exception cref="ArgumentException">Thrown when no element satisfies the condition.</exception>
        public T Last(Func<T, bool> predicate)
        {
            ArgumentNullException.ThrowIfNull(predicate);
            
            for (int index = 0; index < target.Length; index++)
            {
                T item = target[target.LastIndex - index];
                
                if (predicate.Invoke(item))
                    return item;
            }

            throw new ArgumentException();
        }

        /// <summary>
        /// Returns the last element of a <see cref="Span{T}"/> that satisfies a specified condition, or a default value if no such element is found.
        /// </summary>
        /// <param name="predicate">A function that defines the condition to be met.</param>
        /// <returns>The last element of the <see cref="Span{T}"/> that satisfies the condition, or null if no such element is found.</returns>
        public T? LastOrDefault(Func<T, bool> predicate)
        {
            ArgumentNullException.ThrowIfNull(predicate);
            
            for (int index = 0; index < target.Length; index++)
            {
                T item = target[target.LastIndex - index];
                
                if (predicate.Invoke(item))
                    return item;
            }

            return default;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="target"></param>
    /// <typeparam name="T"></typeparam>
    extension<T>(ReadOnlySpan<T> target)
    {
        /// <summary>
        /// Returns the first element of a <see cref="ReadOnlySpan{T}"/> that satisfies a specified condition.
        /// </summary>
        /// <param name="predicate">A function that defines the condition to be met.</param>
        /// <returns>The first element of the <see cref="ReadOnlySpan{T}"/> that satisfies the condition.</returns>
        /// <exception cref="ArgumentException">Thrown when no element satisfies the condition.</exception>
        public T First(Func<T, bool> predicate)
        {
            foreach (T item in target)
            {
                if (predicate.Invoke(item))
                    return item;
            }

            throw new ArgumentException();
        }

        /// <summary>
        /// Returns the first element of a <see cref="ReadOnlySpan{T}"/> that satisfies a specified condition,
        /// or a default value if no such element is found.
        /// </summary>
        /// <param name="predicate">A function that defines the condition to be met.</param>
        /// <returns>The first element of the <see cref="ReadOnlySpan{T}"/> that satisfies the condition, or null if no such element is found.</returns>
        /// <exception cref="ArgumentException">Thrown when the <see cref="ReadOnlySpan{T}"/> is empty or no element satisfies the condition.</exception>
        public T? FirstOrDefault(Func<T, bool> predicate)
        {
            foreach (T item in target)
            {
                if (predicate.Invoke(item))
                    return item;
            }

            return default;
        }

        /// <summary>
        /// Returns the last element of a  <see cref="ReadOnlySpan{T}"/> that satisfies a specified condition.
        /// </summary>
        /// <param name="predicate">A function that defines the condition to be met.</param>
        /// <returns>The last element of the  <see cref="ReadOnlySpan{T}"/> that satisfies the condition.</returns>
        /// <exception cref="ArgumentException">Thrown when no element satisfies the condition.</exception>
        public T Last(Func<T, bool> predicate)
        {
            for (int index = 0; index < target.Length; index++)
            {
                T item = target[target.LastIndex - index];
                
                if (predicate.Invoke(item))
                    return item;
            }

            throw new ArgumentException();
        }

        /// <summary>
        /// Returns the last element of a <see cref="ReadOnlySpan{T}"/> that satisfies a specified condition, or a default value if no such element is found.
        /// </summary>
        /// <param name="predicate">A function that defines the condition to be met.</param>
        /// <returns>The last element of the <see cref="ReadOnlySpan{T}"/> that satisfies the condition, or null if no such element is found.</returns>
        public T? LastOrDefault(Func<T, bool> predicate)
        {
            for (int index = 0; index < target.Length; index++)
            {
                T item = target[target.LastIndex - index];
                if (predicate.Invoke(item))
                    return item;
            }

            return default;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="target"></param>
    /// <typeparam name="T"></typeparam>
    extension<T>(Memory<T> target)
    {
        /// <summary>
        /// Returns the first element of a <see cref="Memory{T}"/> that satisfies a specified condition.
        /// </summary>
        /// <param name="predicate">A function that defines the condition to be met.</param>
        /// <returns>The first element of the <see cref="Memory{T}"/> that satisfies the condition.</returns>
        /// <exception cref="ArgumentException">Thrown when no element satisfies the condition.</exception>
        public T First(Func<T, bool> predicate)
        {
            for (int index = 0; index < target.Length; index++)
            {
                T item = target.ElementAt(index);
                if (predicate.Invoke(item))
                    return item;
            }

            throw new ArgumentException();
        }

        /// <summary>
        /// Returns the first element of a <see cref="Memory{T}"/> that satisfies a specified condition,
        /// or a default value if no such element is found.
        /// </summary>
        /// <param name="predicate">A function that defines the condition to be met.</param>
        /// <returns>The first element of the <see cref="Memory{T}"/> that satisfies the condition, or null if no such element is found.</returns>
        /// <exception cref="ArgumentException">Thrown when the <see cref="Memory{T}"/> is empty or no element satisfies the condition.</exception>
        public T? FirstOrDefault(Func<T, bool> predicate)
        {
            for (int index = 0; index < target.Length; index++)
            {
                T item = target.ElementAt(index);
                if (predicate.Invoke(item))
                    return item;
            }

            return default;
        }

        /// <summary>
        /// Returns the last element of a <see cref="Memory{T}"/> that satisfies a specified condition.
        /// </summary>
        /// <param name="predicate">A function that defines the condition to be met.</param>
        /// <returns>The last element of the <see cref="Memory{T}"/> that satisfies the condition.</returns>
        /// <exception cref="ArgumentException">Thrown when no element satisfies the condition.</exception>
        public T Last(Func<T, bool> predicate)
        {
            for (int index = 0; index < target.Length; index++)
            {
                T item = target.ElementAt(target.LastIndex - index);
                if (predicate.Invoke(item))
                {
                    return item;
                }
            }

            throw new ArgumentException();
        }

        /// <summary>
        /// Returns the last element of a <see cref="Memory{T}"/> that satisfies a specified condition, or a default value if no such element is found.
        /// </summary>
        /// <param name="predicate">A function that defines the condition to be met.</param>
        /// <returns>The last element of the <see cref="Memory{T}"/> that satisfies the condition, or null if no such element is found.</returns>
        public T? LastOrDefault(Func<T, bool> predicate)
        {
            for (int index = 0; index < target.Length; index++)
            {
                T item = target.ElementAt(target.LastIndex - index);
                if (predicate.Invoke(item))
                {
                    return item;
                }
            }

            return default;
        }
    }
}