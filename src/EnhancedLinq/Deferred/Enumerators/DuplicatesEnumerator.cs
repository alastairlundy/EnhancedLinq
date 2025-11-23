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

internal class DuplicatesEnumerator<TSource> : IEnumerator<TSource>
{
    private readonly HashSet<TSource> _hashSet;

    private int _state;
    
    private readonly IEnumerator<TSource> _enumerator;

    internal DuplicatesEnumerator(IEnumerable<TSource> source, IEqualityComparer<TSource> comparer)
    {
        _hashSet = new HashSet<TSource>(comparer);
        _state = 0;
        
        _enumerator = source.GetEnumerator();
        Current = _enumerator.Current;
    }
    
    public bool MoveNext()
    {
        if (_state == 1)
        {
            while(_enumerator.MoveNext())
            {
                bool isDuplicate =  _hashSet.Add(_enumerator.Current);

                if (isDuplicate)
                {
                    Current = _enumerator.Current;
                    return true;
                }
            }

            _state = 2;
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