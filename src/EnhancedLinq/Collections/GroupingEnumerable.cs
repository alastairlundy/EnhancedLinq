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
using System.Linq;

namespace EnhancedLinq.Collections;

/// <summary>
/// A read-only, enumerated sequence of elements grouped by a common key.
/// </summary>
/// <typeparam name="TKey">The type of the grouping keys.</typeparam>
/// <typeparam name="TElement">The type of the elements being grouped.</typeparam>
public class GroupingEnumerable<TKey, TElement> : IGrouping<TKey, TElement>
{
    private readonly IEnumerable<TElement> _elements;

    /// <summary>
    /// Instantiates an IEnumerable of elements grouped by a common key.
    /// </summary>
    /// <param name="key">The key to group elements by.</param>
    /// <param name="elements">The sequence of elements to group.</param>
    public GroupingEnumerable(TKey key, IEnumerable<TElement> elements)
    {
        Key = key;
        _elements = new List<TElement>(elements);
    }

    /// <inheritdoc />
    public IEnumerator<TElement> GetEnumerator() => _elements.GetEnumerator();

    /// <inheritdoc />
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    /// <inheritdoc />
    public TKey Key { get; }
}