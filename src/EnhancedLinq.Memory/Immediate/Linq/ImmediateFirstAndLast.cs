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

namespace AlastairLundy.EnhancedLinq.Memory.Immediate;

/// <summary>
/// 
/// </summary>
public static partial class EnhancedLinqMemoryImmediate
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
            if (target.IsEmpty)
                throw new InvalidOperationException(Resources.Exceptions_InvalidOperation_EmptySpan);
        
            return target[0];
        }
        
        /// <summary>
        /// Returns the first element of a <see cref="Span{T}"/> that satisfies a specified condition, or null if the <see cref="Span{T}"/> is empty.
        /// </summary>
        /// <returns>The first element of the <see cref="Span{T}"/> that satisfies the condition, or null if the <see cref="Span{T}"/> is empty.</returns>
        public T? FirstOrDefault() 
            => !target.IsEmpty ? target[0] : default;
        
        /// <summary>
        /// Returns the last element of a span that satisfies a specified condition,
        /// or null if the Span is empty.
        /// </summary>
        /// <typeparam name="T">The type of elements in the span.</typeparam>
        /// <returns>The last element of the span, or null if the span is empty.</returns>
        public T? LastOrDefault()
        {
            if (target.IsEmpty)
                return default;
        
#if NET8_0_OR_GREATER
            return target[^1];
#else
            return target[target.Length - 1];
#endif
        }
        
        /// <summary>
        /// Returns the last element in the Span.
        /// </summary>
        /// <typeparam name="T">The type of items stored in the span.</typeparam>
        /// <returns>The last item in the span if any items are in the Span.</returns>
        /// <exception cref="InvalidOperationException">Thrown if the Span contains zero items.</exception>
        public T Last()
        {
            if (target.IsEmpty)
                throw new InvalidOperationException(Resources.Exceptions_InvalidOperation_EmptySpan);

#if NET8_0_OR_GREATER
            return target[^1];
#else
            return target[target.Length - 1];
#endif
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
        /// Returns the last element of a <see cref="ReadOnlySpan{T}"/> that satisfies a specified condition,
        /// or null if the Span is empty.
        /// </summary>
        /// <typeparam name="T">The type of elements in the <see cref="ReadOnlySpan{T}"/>.</typeparam>
        /// <returns>The last element of the <see cref="ReadOnlySpan{T}"/>, or null if the span is empty.</returns>
        public T? LastOrDefault()
        {
            if (target.IsEmpty)
                return default;
        
#if NET8_0_OR_GREATER
            return target[^1];
#else
            return target[target.Length - 1];
#endif
        }
        
        /// <summary>
        /// Returns the last element in the <see cref="ReadOnlySpan{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of items stored in the <see cref="ReadOnlySpan{T}"/>.</typeparam>
        /// <returns>The last item in the <see cref="ReadOnlySpan{T}"/> if any items are in the Span.</returns>
        /// <exception cref="InvalidOperationException">Thrown if the <see cref="ReadOnlySpan{T}"/> contains zero items.</exception>
        public T Last()
        {
            if (target.IsEmpty)
                throw new InvalidOperationException(Resources.Exceptions_InvalidOperation_EmptySpan);

#if NET8_0_OR_GREATER
            return target[^1];
#else
            return target[target.Length - 1];
#endif
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
        public T First() =>
            source.IsEmpty ? source.ElementAt(0) :
                throw new InvalidOperationException("The source Memory is empty.");

        /// <summary>
        /// Returns the last element of a Memory sequence.
        /// </summary>
        /// <returns>The last element of the Memory sequence.</returns>
        public T Last() =>
            source.IsEmpty ? source.ElementAt(source.Length - 1) :
                throw new InvalidOperationException("The source Memory is empty.");

        /// <summary>
        /// Returns the first element of a Memory sequence or default if it is empty.
        /// </summary>
        /// <returns>The first element of the Memory or default if no elements were found.</returns>
        public T? FirstOrDefault() 
            => source.IsEmpty ? default : source.ElementAt(0);

        /// <summary>
        /// Returns the last element of a Memory sequence or default if it is empty.
        /// </summary>
        /// <returns>The last element of the Memory or default if no elements were found.</returns>
        public T? LastOrDefault() => source.IsEmpty ? default : source.ElementAt(source.Length - 1);
    }
    
    /// <param name="source">The source Memory sequence.</param>
    /// <typeparam name="T">The type of elements in the Memory sequence.</typeparam>
    extension<T>(ReadOnlyMemory<T> source)
    {
        /// <summary>
        /// Returns the first element of a <see cref="ReadOnlyMemory{T}"/> sequence.
        /// </summary>
        /// <returns>The first element of the <see cref="ReadOnlyMemory{T}"/>  sequence.</returns>
        public T First() =>
            source.IsEmpty ? source.ElementAt(0) :
                throw new InvalidOperationException("The source Memory is empty.");

        /// <summary>
        /// Returns the last element of a <see cref="ReadOnlyMemory{T}"/>  sequence.
        /// </summary>
        /// <returns>The last element of the <see cref="ReadOnlyMemory{T}"/>  sequence.</returns>
        public T Last() =>
            source.IsEmpty ? source.ElementAt(source.Length - 1) :
                throw new InvalidOperationException("The source Memory is empty.");

        /// <summary>
        /// Returns the first element of a <see cref="ReadOnlyMemory{T}"/>  sequence or default if it is empty.
        /// </summary>
        /// <returns>The first element of the <see cref="ReadOnlyMemory{T}"/>  or default if no elements were found.</returns>
        public T? FirstOrDefault() 
            => source.IsEmpty ? default : source.ElementAt(0);

        /// <summary>
        /// Returns the last element of a <see cref="ReadOnlyMemory{T}"/>  sequence or default if it is empty.
        /// </summary>
        /// <returns>The last element of the <see cref="ReadOnlyMemory{T}"/>  or default if no elements were found.</returns>
        public T? LastOrDefault() => source.IsEmpty ? default : source.ElementAt(source.Length - 1);
    }
}