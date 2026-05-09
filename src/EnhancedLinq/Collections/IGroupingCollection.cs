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
/// Represents a read-only, enumerated sequence of elements grouped by a common key with a count of grouped items.
/// </summary>
/// <typeparam name="TKey">The type of the grouping keys.</typeparam>
/// <typeparam name="TElement">The type of the elements being grouped.</typeparam>
public interface IGroupingCollection<out TKey, TElement> : IGrouping<TKey, TElement>, ICollection<TElement>
{
    /// <summary>
    /// The number of elements in the <see cref="IGroupingCollection{TKey,TElement}"/>.
    /// </summary>
    public new int Count { get; }
    
    /// <summary>
    /// Whether this <see cref="IGroupingCollection{TKey,TElement}"/> is read-only or not.
    /// </summary>
    public new bool IsReadOnly { get; }
    
    /// <summary>
    /// Adds an element to the <see cref="IGroupingCollection{TKey,TElement}"/> if it is not read-only.
    /// </summary>
    /// <param name="element">The element to be added to the <see cref="IGroupingCollection{TKey,TElement}"/>.</param>
    new void Add(TElement element);
    
    /// <summary>
    /// Removes the first occurrence of the element in the <see cref="IGroupingCollection{TKey,TElement}"/>.
    /// </summary>
    /// <param name="element">The element to be removed from the <see cref="IGroupingCollection{TKey,TElement}"/>.</param>
    /// <returns>True if the first occurrence of the element was removed, false otherwise.</returns>
    new bool Remove(TElement element);
    
    /// <summary>
    /// Determines whether the <see cref="IGroupingCollection{TKey,TElement}"/> contains the specified element.
    /// </summary>
    /// <param name="element">The element to look for.</param>
    /// <returns>True if the element was found in the <see cref="IGroupingCollection{TKey,TElement}"/>, false otherwise.</returns>
    new bool Contains(TElement element);
    
    /// <summary>
    /// Removes all elements from the <see cref="IGroupingCollection{TKey,TElement}"/>.
    /// </summary>
    new void Clear();
    
    /// <summary>
    /// Copies each element in the <see cref="IGroupingCollection{TKey,TElement}"/> to an array, beginning at the specified array index.
    /// </summary>
    /// <param name="array">The array to copy elements to.</param>
    /// <param name="arrayIndex">The index to begin copying elements to in the array.</param>
    new void CopyTo(TElement[] array, int arrayIndex);
}