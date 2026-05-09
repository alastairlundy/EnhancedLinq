/*
      MIT License
     
      Copyright (c) 2025-2026 Alastair Lundy
     
      Permission is hereby granted, free of charge, to any person obtaining a copy
      of this software and associated documentation files (the "Software"), to deal
      in the Software without restriction, including without limitation the rights
      to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
      copies of the Software, and to permit persons to whom the Software is
      furnished to do so, subject to the following conditions:
     
      The above copyright notice and this permission notice shall be included in all
      copies or substantial portions of the Software.
     
      THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
      IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
      FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
      AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
      LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
      OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
      SOFTWARE.
   */

using System.Linq;

namespace EnhancedLinq.Collections;

/// <summary>
/// 
/// </summary>
public static class AsGroupingExtensions
{
    /*/// <summary>
    /// 
    /// </summary>
    /// <param name="source">The sequence to group with a key.</param>
    /// <typeparam name="TElement">The type of the elements being grouped.</typeparam>
    extension<TElement>(IEnumerable<TElement> source)
    {
        /// <summary>
        /// Groups a sequence of elements with a key.
        /// </summary>
        /// <param name="key">The key to group elements by.</param>
        /// <typeparam name="TKey">The type of the grouping key.</typeparam>
        /// <returns>A new <see cref="IGrouping{TKey,TElement}"/> with the specified key and sequence elements.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the source <see cref="IEnumerable{T}"/> is null.</exception>
        public IGrouping<TKey, TElement> AsGrouping<TKey>
            (TKey key)
        {
#if NET8_0_OR_GREATER
            ArgumentNullException.ThrowIfNull(source);
#endif
            
            return new GroupingEnumerable<TKey, TElement>(key, source);
        }
    }*/

    /// <summary>
    /// 
    /// </summary>
    /// <param name="grouping">The grouping to create a grouping collection from.</param>
    /// <typeparam name="TKey">The type of the grouping keys.</typeparam>
    /// <typeparam name="TElement">The type of the elements being grouped.</typeparam>
    extension<TKey, TElement>(IGrouping<TKey, TElement> grouping)
    {
        /// <summary>
        /// Creates a new grouping collection from a grouping.
        /// </summary>
        /// <typeparam name="TKey">The type of the grouping keys.</typeparam>
        /// <typeparam name="TElement">The type of the elements being grouped.</typeparam>
        /// <returns>A <see cref="IGroupingCollection{TKey,TElement}"/> collection of the elements grouped by a key.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the source <see cref="IGrouping{TKey,TElement}"/> is null.</exception>
        public IGroupingCollection<TKey, TElement> AsGroupingCollection()
        {
            ArgumentNullException.ThrowIfNull(grouping);
        
            return new GroupingCollection<TKey, TElement>(grouping.Key, grouping);
        }
    }
}