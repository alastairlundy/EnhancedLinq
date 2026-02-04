/*
    EnhancedLinq 
    Copyright (c) 2025-2026 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
    */

using System.Collections;

namespace EnhancedLinq.Deferred.Enumerators;

internal class SplitByEnumerableCountEnumerator<T> : IEnumerator<IEnumerable<T>>
{
    private readonly IEnumerator<IEnumerable<T>> _enumerator;
    
    public SplitByEnumerableCountEnumerator(IEnumerable<T> source, int maxEnumerableCount)
    {
       List<T> list = new List<T>(source);
       
        double maxItems = Convert.ToDouble(list.Count / maxEnumerableCount);
        int maxItemCount;
        
        if (maxItems % 1 != 0)
        {
            maxItemCount = Convert.ToInt32(maxItems) + 1;
        }
        else
        {
            maxItemCount = Convert.ToInt32(maxItems);
        }
        
        _enumerator = new SplitByItemCountEnumerator<T>(list, maxItemCount, maxEnumerableCount);
    }
    
    public bool MoveNext()
    {
       return _enumerator.MoveNext();
    }

    public void Reset()
    {
        throw new NotSupportedException();
    }

    IEnumerable<T> IEnumerator<IEnumerable<T>>.Current => _enumerator.Current;

    object? IEnumerator.Current => _enumerator.Current;

    public void Dispose()
    {
       _enumerator?.Dispose();
    }
}