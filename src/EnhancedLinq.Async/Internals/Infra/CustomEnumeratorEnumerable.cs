/*
    MIT License
   
    Copyright (c) 2025 Alastair Lundy
   
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

namespace EnhancedLinq.Async.Internals.Infra;

/// <summary>
/// A sequence that wraps a custom enumerator.
/// </summary>
/// <remarks>The provided <see cref="IEnumerator{T}"/> should be a new Enumerator instance that has not been re-used.</remarks>
/// <typeparam name="T">The type of element stored in the sequence.</typeparam>
internal class CustomEnumeratorEnumerable<T> : IEnumerable<T>, IDisposable
{
    private readonly IEnumerator<T> _enumerator;
    
    /// <summary>
    /// Instantiates the <see cref="CustomEnumeratorEnumerable{T}"/> with the custom <see cref="IEnumerator{T}"/> to wrap.
    /// </summary>
    /// <param name="enumerator">The enumerator to use when the sequence is enumerated.</param>
    internal CustomEnumeratorEnumerable(IEnumerator<T> enumerator)
    {
        _enumerator = enumerator;
    }
    
    /// <summary>
    /// Retrieves the wrapped custom enumerator.
    /// </summary>
    /// <remarks>Tries to reset the enumerator if it has already been materialized and if supported. If the enumerator doesn't support resetting, it returns the enumerator in its current state.</remarks>
    /// <returns>The wrapped custom enumerator.</returns>
    public IEnumerator<T> GetEnumerator()
    {
        return _enumerator;
    }

    /// <summary>
    /// Retrieves the wrapped custom enumerator.
    /// </summary>
    /// <remarks>Tries to reset the enumerator if it has already been materialized and if supported. If the enumerator doesn't support resetting, it returns the enumerator in its current state.</remarks>
    /// <returns>The wrapped custom enumerator.</returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
    
    /// <summary>
    /// Disposes of the internal enumerator once the sequence is to be disposed of.
    /// </summary>
    public void Dispose()
    {
        _enumerator.Dispose();
    }
}