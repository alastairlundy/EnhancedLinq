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
using System.Collections;
using System.Collections.Generic;

namespace AlastairLundy.EnhancedLinq.Deferred.Enumerators;

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