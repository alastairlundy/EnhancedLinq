/*
    EnhancedLinq.Memory
    Copyright (c) 2025-2026 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
    */

using EnhancedLinq.Memory.Deferred;

namespace EnhancedLinq.Memory.Immediate;

/// <summary>
/// 
/// </summary>
public static class ImmediateMemoryFirstAndLastExtensions
{
    /// <param name="target">The span to be searched.</param>
    /// <typeparam name="T">The type of items stored in the <see cref="Span{T}"/>.</typeparam>
    extension<T>(Span<T> target)
    {
        /// <summary>
        /// Returns the first element in the <see cref="Span{T}"/>.
        /// </summary>
        /// <returns>The first item in the <see cref="Span{T}"/> if any items are in the <see cref="Span{T}"/>.</returns>
        /// <exception cref="InvalidOperationException">Thrown if the <see cref="Span{T}"/> contains zero items.</exception>
        public T First()
        {
            InvalidOperationException.ThrowIfSpanIsEmpty(target);
        
            return target[0];
        }
        
        /// <summary>
        /// Returns the first element of a <see cref="Span{T}"/> that satisfies a specified condition, or null if the <see cref="Span{T}"/> is empty.
        /// </summary>
        /// <returns>The first element of the <see cref="Span{T}"/> that satisfies the condition, or null if the <see cref="Span{T}"/> is empty.</returns>
        public T? FirstOrDefault() 
            => !target.IsEmpty ? target[0] : default;
        
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
        /// Returns the last element of a span that satisfies a specified condition,
        /// or null if the Span is empty.
        /// </summary>
        /// <returns>The last element of the span, or null if the span is empty.</returns>
        public T? LastOrDefault()
        {
            if (target.IsEmpty)
                return default;
        
#if NET8_0_OR_GREATER
            return target[^1];
#else
            return target[target.LastIndex];
#endif
        }
        
        /// <summary>
        /// Returns the last element in the Span.
        /// </summary>
        /// <returns>The last item in the span if any items are in the Span.</returns>
        /// <exception cref="InvalidOperationException">Thrown if the Span contains zero items.</exception>
        public T Last()
        {
            InvalidOperationException.ThrowIfSpanIsEmpty(target);

#if NET8_0_OR_GREATER
            return target[^1];
#else
            return target[target.LastIndex];
#endif
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
    
    /// <param name="target">The span to be searched.</param>
    /// <typeparam name="T">The type of items stored in the span.</typeparam>
    extension<T>(ReadOnlySpan<T> target)
    {
        /// <summary>
        /// Returns the first element in the <see cref="ReadOnlySpan{T}"/>.
        /// </summary>
        /// <returns>The first item in the span if any items are in the <see cref="ReadOnlySpan{T}"/>.</returns>
        /// <exception cref="InvalidOperationException">Thrown if the <see cref="ReadOnlySpan{T}"/> contains zero items.</exception>
        public T First()
        {
            if (target.IsEmpty)
                throw new InvalidOperationException(Resources.Exceptions_InvalidOperation_EmptySpan);
        
            return target[0];
        }
        
        /// <summary>
        /// Returns the first element of a <see cref="ReadOnlySpan{T}"/> that satisfies a specified condition, or null if the <see cref="ReadOnlySpan{T}"/> is empty.
        /// </summary>
        /// <returns>The first element of the <see cref="ReadOnlySpan{T}"/> or null if the span is empty.</returns>
        public T? FirstOrDefault() 
            => !target.IsEmpty ? target[0] : default;
        
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
        /// Returns the last element of a <see cref="ReadOnlySpan{T}"/> that satisfies a specified condition,
        /// or null if the Span is empty.
        /// </summary>
        /// <returns>The last element of the <see cref="ReadOnlySpan{T}"/>, or null if the span is empty.</returns>
        public T? LastOrDefault()
        {
            if (target.IsEmpty)
                return default;
        
#if NET8_0_OR_GREATER
            return target[^1];
#else
            return target[target.LastIndex];
#endif
        }
        
        /// <summary>
        /// Returns the last element in the <see cref="ReadOnlySpan{T}"/>.
        /// </summary>
        /// <returns>The last item in the <see cref="ReadOnlySpan{T}"/> if any items are in the Span.</returns>
        /// <exception cref="InvalidOperationException">Thrown if the <see cref="ReadOnlySpan{T}"/> contains zero items.</exception>
        public T Last()
        {
            InvalidOperationException.ThrowIfSpanIsEmpty(target);

#if NET8_0_OR_GREATER
            return target[^1];
#else
            return target[target.LastIndex];
#endif
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

    /// <param name="source">The source Memory sequence.</param>
    /// <typeparam name="T">The type of elements in the Memory sequence.</typeparam>
    extension<T>(Memory<T> source)
    {
        /// <summary>
        /// Returns the first element of a Memory sequence.
        /// </summary>
        /// <returns>The first element of the Memory sequence.</returns>
        public T First()
        {
            InvalidOperationException.ThrowIfMemoryIsEmpty(source);

            foreach (T item in source.AsEnumerable())
            {
                return item;
            }

            throw new ArgumentException();
        }

        /// <summary>
        /// Returns the first element of a <see cref="Memory{T}"/> that satisfies a specified condition.
        /// </summary>
        /// <param name="predicate">A function that defines the condition to be met.</param>
        /// <returns>The first element of the <see cref="Memory{T}"/> that satisfies the condition.</returns>
        /// <exception cref="ArgumentException">Thrown when no element satisfies the condition.</exception>
        public T First(Func<T, bool> predicate)
        {
            for (int index = 0; index < source.Length; index++)
            {
                T item = source.ElementAt(index);
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
            for (int index = 0; index < source.Length; index++)
            {
                T item = source.ElementAt(index);
                if (predicate.Invoke(item))
                    return item;
            }

            return default;
        }
        
        /// <summary>
        /// Returns the last element of a Memory sequence.
        /// </summary>
        /// <returns>The last element of the Memory sequence.</returns>
        public T Last() =>
            source.IsEmpty ? source.ElementAt(source.LastIndex) :
                throw new InvalidOperationException("The source Memory is empty.");

        /// <summary>
        /// Returns the first element of a Memory sequence or default if it is empty.
        /// </summary>
        /// <returns>The first element of the Memory or default if no elements were found.</returns>
        public T? FirstOrDefault()
        {
            if (source.IsEmpty)
                return default;

            foreach (T item in source.AsEnumerable())
            {
                return item;
            }

            return default;
        }

        /// <summary>
        /// Returns the last element of a Memory sequence or default if it is empty.
        /// </summary>
        /// <returns>The last element of the Memory or default if no elements were found.</returns>
        public T? LastOrDefault() => source.IsEmpty ? default : source.ElementAt(source.LastIndex);
        
        /// <summary>
        /// Returns the last element of a <see cref="Memory{T}"/> that satisfies a specified condition.
        /// </summary>
        /// <param name="predicate">A function that defines the condition to be met.</param>
        /// <returns>The last element of the <see cref="Memory{T}"/> that satisfies the condition.</returns>
        /// <exception cref="ArgumentException">Thrown when no element satisfies the condition.</exception>
        public T Last(Func<T, bool> predicate)
        {
            for (int index = 0; index < source.Length; index++)
            {
                T item = source.ElementAt(source.LastIndex - index);
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
            for (int index = 0; index < source.Length; index++)
            {
                T item = source.ElementAt(source.LastIndex - index);
                if (predicate.Invoke(item))
                {
                    return item;
                }
            }

            return default;
        }
    }
    
    /// <param name="source">The source Memory sequence.</param>
    /// <typeparam name="T">The type of elements in the Memory sequence.</typeparam>
    extension<T>(ReadOnlyMemory<T> source)
    {
        /// <summary>
        /// Returns the first element of a <see cref="ReadOnlyMemory{T}"/> sequence.
        /// </summary>
        /// <returns>The first element of the <see cref="ReadOnlyMemory{T}"/>  sequence.</returns>
        public T First()
        {
            InvalidOperationException.ThrowIfMemoryIsEmpty(source);
            
            foreach (T item in source.AsEnumerable())
            {
                return item;
            }

            throw new ArgumentException();
        }
        
        /// <summary>
        /// Returns the first element of a <see cref="ReadOnlyMemory{T}"/>  sequence or default if it is empty.
        /// </summary>
        /// <returns>The first element of the <see cref="ReadOnlyMemory{T}"/>  or default if no elements were found.</returns>
        public T? FirstOrDefault()
        {
            if (source.IsEmpty)
                return default;

            foreach (T item in source.AsEnumerable())
            {
                return item;
            }

            return default;
        }

        /// <summary>
        /// Returns the first element of a <see cref="ReadOnlyMemory{T}"/> that satisfies a specified condition.
        /// </summary>
        /// <param name="predicate">A function that defines the condition to be met.</param>
        /// <returns>The first element of the <see cref="ReadOnlyMemory{T}"/> that satisfies the condition.</returns>
        /// <exception cref="ArgumentException">Thrown when no element satisfies the condition.</exception>
        public T First(Func<T, bool> predicate)
        {
            for (int index = 0; index < source.Length; index++)
            {
                T item = source.ElementAt(index);
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
            for (int index = 0; index < source.Length; index++)
            {
                T item = source.ElementAt(index);
                if (predicate.Invoke(item))
                    return item;
            }

            return default;
        }
        
        /// <summary>
        /// Returns the last element of a <see cref="ReadOnlyMemory{T}"/>  sequence.
        /// </summary>
        /// <returns>The last element of the <see cref="ReadOnlyMemory{T}"/>  sequence.</returns>
        public T Last()
        {
            InvalidOperationException.ThrowIfMemoryIsEmpty(source);
            
            return source.ElementAt(source.LastIndex);
        }

        /// <summary>
        /// Returns the last element of a <see cref="ReadOnlyMemory{T}"/>  sequence or default if it is empty.
        /// </summary>
        /// <returns>The last element of the <see cref="ReadOnlyMemory{T}"/>  or default if no elements were found.</returns>
        public T? LastOrDefault() => source.IsEmpty ? default : source.ElementAt(source.LastIndex);
        
        /// <summary>
        /// Returns the last element of a <see cref="Memory{T}"/> that satisfies a specified condition.
        /// </summary>
        /// <param name="predicate">A function that defines the condition to be met.</param>
        /// <returns>The last element of the <see cref="Memory{T}"/> that satisfies the condition.</returns>
        /// <exception cref="ArgumentException">Thrown when no element satisfies the condition.</exception>
        public T Last(Func<T, bool> predicate)
        {
            for (int index = 0; index < source.Length; index++)
            {
                T item = source.ElementAt(source.LastIndex - index);
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
            for (int index = 0; index < source.Length; index++)
            {
                T item = source.ElementAt(source.LastIndex - index);
                if (predicate.Invoke(item))
                {
                    return item;
                }
            }

            return default;
        }
    }
}