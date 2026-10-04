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

namespace EnhancedLinq.Shared.Infra;

/// <summary>
/// A sequence that wraps a custom enumerator factory.
/// </summary>
/// <remarks>A new <see cref="IEnumerator{T}"/> is created for each enumeration,
/// so the sequence can be enumerated multiple times.</remarks>
/// <typeparam name="T">The type of element stored in the sequence.</typeparam>
internal class CustomEnumeratorEnumerable<T> : IEnumerable<T>, IDisposable
{
    private readonly Func<IEnumerator<T>> _factory;
    
    /// <summary>
    /// Instantiates the <see cref="CustomEnumeratorEnumerable{T}"/> with a factory
    /// that creates a new <see cref="IEnumerator{T}"/> per enumeration.
    /// </summary>
    /// <param name="factory">A factory that creates a new enumerator that has not been used.</param>
    internal CustomEnumeratorEnumerable(Func<IEnumerator<T>> factory)
    {
        ArgumentNullException.ThrowIfNull(factory);
        _factory = factory;
    }
    
    /// <summary>
    /// Retrieves a new enumerator created by the factory.
    /// </summary>
    /// <returns>A new enumerator for the sequence.</returns>
    public IEnumerator<T> GetEnumerator()
    {
        return _factory();
    }

    /// <summary>
    /// Retrieves a new enumerator created by the factory.
    /// </summary>
    /// <returns>A new enumerator for the sequence.</returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
    
    /// <summary>
    /// No-op: individual enumerators are disposed by their consumers.
    /// </summary>
    public void Dispose()
    {
    }
}
