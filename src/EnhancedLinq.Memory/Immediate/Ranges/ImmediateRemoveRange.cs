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
using System.Collections.Generic;
using System.Linq;

using AlastairLundy.EnhancedLinq.Memory.Internals.Localizations;

// ReSharper disable ConvertClosureToMethodGroup

namespace AlastairLundy.EnhancedLinq.Memory.Immediate.Ranges;

public static partial class EnhancedLinqMemoryImmediateRange
{
    
    /// <summary>
    /// Creates a new Span with all items of the original Span minus the items to be removed.
    /// </summary>
    /// <param name="target">The span to remove a range of items from.</param>
    /// <param name="indices">The indices of the items to be removed.</param>
    /// <typeparam name="T">The type of elements in the span.</typeparam>
    /// <returns>A new Span with all items of the original Span minus the items to be removed.</returns>
    public static Span<T> RemoveRange<T>(this Span<T> target, IEnumerable<int> indices) where T : IEquatable<T>?
    {
        if (target.IsEmpty)
            throw new ArgumentException(Resources.Exceptions_InvalidOperation_EmptySpan);
        
        IEnumerable<int> newIndices = target.Index().SkipWhile(x => indices.Contains(x));
        
        return target.GetRange(newIndices);
    }
    
#if NET8_0_OR_GREATER
    /// <summary>
    /// Creates a new Span with all items of the original Span minus the items to be removed.
    /// </summary>
    /// <param name="target">The span to remove a range of items from.</param>
    /// <param name="range">The index <see cref="Range"/> of items to be removed.</param>
    /// <typeparam name="T">The type of elements in the span.</typeparam>
    /// <returns>A new Span with all items of the original Span minus the items to be removed.</returns>
    public static Span<T> RemoveRange<T>(this Span<T> target, Range range) where T : IEquatable<T>? 
        => RemoveRange(target, range.Start.Value, range.End.Value - range.Start.Value);
#endif
    
    /// <summary>
    /// Creates a new Span with all items of the original Span minus the items to be removed.
    /// </summary>
    /// <param name="target">The span to remove a range of items from.</param>
    /// <param name="startIndex">The zero-based index to start removing items at.</param>
    /// <param name="count">The number of items to remove from the Span from the start index.</param>
    /// <typeparam name="T">The type of elements in the span.</typeparam>
    /// <returns>A new Span with all items of the original Span minus the items to be removed.</returns>
    public static Span<T> RemoveRange<T>(this Span<T> target, int startIndex, int count) where T : IEquatable<T>?
    {
        if (target.IsEmpty)
            throw new ArgumentException(Resources.Exceptions_InvalidOperation_EmptySpan);
        
        if (startIndex < 0 || startIndex > target.Length)
            throw new IndexOutOfRangeException();
        
        if(count < 0 || count > target.Length)
            throw new ArgumentOutOfRangeException(nameof(count));

        return RemoveRange(target, Enumerable.Range(startIndex, count));
    }
}