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

using System.Collections;

namespace EnhancedLinq.Collections;

/// <summary>
/// A read-only, enumerated collection of elements grouped by a common key.
/// </summary>
/// <typeparam name="TKey">The type of the grouping keys.</typeparam>
/// <typeparam name="TElement">The type of the elements being grouped.</typeparam>
public class GroupingCollection<TKey, TElement> : IGroupingCollection<TKey, TElement>
{
    private readonly ICollection<TElement> _elements;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <param name="isReadOnly"></param>
    public GroupingCollection(TKey key, bool isReadOnly = false)
    {
        Key = key;
        IsReadOnly = isReadOnly;
        _elements = new List<TElement>();
    }
    
    /// <summary>
    /// Instantiates a collection of grouped by a common key.
    /// </summary>
    /// <param name="key">The key to group elements by.</param>
    /// <param name="elements">The sequence of elements to group.</param>
    /// <param name="isReadOnly">Whether the GroupCollection is read-only or not.</param>
    public GroupingCollection(TKey key, IEnumerable<TElement> elements, bool isReadOnly = false)
    {
        _elements = new List<TElement>(elements);
        Key = key;
        IsReadOnly = isReadOnly;
    }

    /// <summary>
    /// Instantiates a collection of grouped by a common key.
    /// </summary>
    /// <param name="key">The key to group elements by.</param>
    /// <param name="elements">The collection of elements to group.</param>
    /// <param name="isReadOnly">Whether the GroupCollection is read-only or not.</param>
    public GroupingCollection(TKey key, ICollection<TElement> elements, bool isReadOnly = false)
    {
        _elements = new List<TElement>(elements);
        Key = key;
        IsReadOnly = isReadOnly;
    }
    
    /// <inheritdoc />
    public IEnumerator<TElement> GetEnumerator() => _elements.GetEnumerator();

    /// <inheritdoc />
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    /// <inheritdoc />
    public TKey Key { get; }

    /// <summary>
    /// The number of elements in the <see cref="GroupingCollection{TKey,TElement}"/>.
    /// </summary>
    public int Count => _elements.Count;

    /// <summary>
    /// Whether this <see cref="GroupingCollection{TKey,TElement}"/> is read-only or not.
    /// </summary>
    public bool IsReadOnly { get; }

    /// <summary>
    /// Adds an element to the <see cref="GroupingCollection{TKey,TElement}"/> if it is not read-only.
    /// </summary>
    /// <param name="element">The element to be added to the <see cref="GroupingCollection{TKey,TElement}"/>.</param>
    /// <exception cref="NotSupportedException">Thrown if the <see cref="GroupingCollection{TKey,TElement}"/> is read-only.</exception>
    public void Add(TElement element)
    {
        if (IsReadOnly)
            throw new NotSupportedException();
        
        _elements.Add(element);
    }

    /// <summary>
    /// Removes the first occurrence of the element in the <see cref="GroupingCollection{TKey,TElement}"/>.
    /// </summary>
    /// <param name="element">The element to be removed from the <see cref="GroupingCollection{TKey,TElement}"/>.</param>
    /// <returns>True if the first occurrence of the element was removed, false otherwise.</returns>
    /// <exception cref="NotSupportedException">Thrown if the <see cref="GroupingCollection{TKey,TElement}"/> is read-only.</exception>
    public bool Remove(TElement element)
    {
        if (IsReadOnly)
            throw new NotSupportedException();
        
        return _elements.Remove(element);
    }

    /// <summary>
    /// Determines whether the <see cref="GroupingCollection{TKey,TElement}"/> contains the specified element.
    /// </summary>
    /// <param name="element">The element to look for.</param>
    /// <returns>True if the element was found in the <see cref="GroupingCollection{TKey,TElement}"/>, false otherwise.</returns>
    public bool Contains(TElement element) => _elements.Contains(element);

    /// <summary>
    /// Removes all elements from the <see cref="GroupingCollection{TKey,TElement}"/>.
    /// </summary>
    public void Clear() => _elements.Clear();

    /// <summary>
    /// Copies each element in the <see cref="GroupingCollection{TKey,TElement}"/> to an array, beginning at the specified array index.
    /// </summary>
    /// <param name="array">The array to copy elements to.</param>
    /// <param name="arrayIndex">The index to begin copying elements to in the array.</param>
    public void CopyTo(TElement[] array, int arrayIndex)
    {
        ArgumentNullException.ThrowIfNull(array);
        
        ArgumentOutOfRangeException.ThrowIfNegative(arrayIndex);
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(arrayIndex, array.Length);
        
        _elements.CopyTo(array, arrayIndex);
    }
}