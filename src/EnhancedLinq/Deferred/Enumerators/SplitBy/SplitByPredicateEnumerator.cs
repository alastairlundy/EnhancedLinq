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

internal class SplitByPredicateEnumerator<T> : IEnumerator<IEnumerable<T>>
{
    private readonly IEnumerable<T> _source;
    private readonly Func<T, bool> _predicate;

    private IEnumerator<T> _enumerator;
    
    private IEnumerable<T> _current;
    
    private int _state;
    
    internal SplitByPredicateEnumerator(IEnumerable<T> source, Func<T, bool> predicate)
    {
        _predicate = predicate;
        _source = source;
        _state = 1;
    }
    
    public bool MoveNext()
    {
        if (_state == 1)
        {
            _enumerator =  _source.GetEnumerator();
            _state = 2;
        }
        if (_state == 2)
        {
            try
            {
                List<T> tempList = new List<T>();
                
                while(_enumerator.MoveNext())
                {
                    bool split = _predicate(_enumerator.Current);
                    
                    if (split == false)
                    {
                        tempList.Add(_enumerator.Current);
                    }
                    else
                    {
                        _current = new List<T>(tempList);
                        tempList.Clear();
                        return true;
                    }
                }
            }
            catch
            {
                Dispose();
                throw;
            }
        }

        Dispose();
        _state = -1;
        return false;
    }

    public void Reset()
    {
       throw new NotSupportedException();
    }

    public IEnumerable<T> Current => _current;

    object? IEnumerator.Current => Current;

    public void Dispose()
    {
        _enumerator?.Dispose();
    }
}