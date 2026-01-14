/*
    EnhancedLinq.Memory
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
*/

using System.Linq;
using EnhancedLinq.Memory.Immediate;

namespace EnhancedLinq.Memory.Deferred.Enumerators;

internal class MemoryOrderByEnumerator<TSource, TKey> : IEnumerator<TSource>
{
    private readonly IComparer<TKey> _comparer;
    private readonly bool _descending;

    private IEnumerator<TSource>? _enumerator;

    private readonly IEnumerable<(TSource item, TKey key)> _pairs;

    private int _state;
    
    internal MemoryOrderByEnumerator(ReadOnlyMemory<TSource> source, Func<TSource, TKey> predicate,
        IComparer<TKey> comparer, bool descending)
    {
        _pairs = source
            .Select(s => (s, predicate(s)));

        _comparer = comparer;
        _descending = descending;
        
        _state = 0;
        // ReSharper disable once InvokeAsExtensionMethod
        Current = EnhancedLinqMemoryImmediate.First(source);
        _enumerator = null;
    }
    
    public bool MoveNext()
    {
        if (_state == 0)
        {
            IEnumerable<TSource> enumerable;

            if (_descending)
            {
                enumerable = _pairs.OrderByDescending(t => t.key, _comparer)
                    .Select(p => p.item);
            }
            else
            {
#if NET8_0_OR_GREATER
                (TSource item, TKey key)[] tempArray =_pairs.ToArray();

                tempArray.Sort((left, right) 
                    => _comparer.Compare(left.key, right.key));
                
                enumerable = Enumerable.Select(tempArray, p => p.item);
#else
                enumerable = _pairs.OrderBy(p => p.key, _comparer)
                    .Select(p => p.item);
#endif
            }
            
            _enumerator = (IEnumerator<TSource>?)enumerable.GetEnumerator();
            _state = 1;
        }
        if (_state == 1)
        {
            bool moveNext = (bool)_enumerator?.MoveNext();

            if (moveNext)
            {
                Current = _enumerator.Current;
                return true;
            }
        }

        Dispose();
        return false;
    }

    public void Reset()
    {
        throw new NotSupportedException();
    }

    public TSource Current { get; private set; }

    object? IEnumerator.Current => Current;

    public void Dispose()
    {
        _enumerator?.Dispose();
    }
}