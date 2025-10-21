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

namespace AlastairLundy.EnhancedLinq.Deferred.Enumerators.Indices;

internal class IndicesEnumerator<T> : IEnumerator<int>
{
    private readonly IEnumerable<T> _source;
    private readonly Func<T, bool> _predicate;

    private IEnumerator<T> enumerator;

    private int _index;
    private int _current;

    private int _state;

    public IndicesEnumerator(IEnumerable<T> source, Func<T, bool> predicate)
    {
        _source = source;
        _predicate = predicate;
        
        _index = 0;
        _state = 1;
    }

    public bool MoveNext()
    {
        if (_state == 1)
        {
            enumerator = _source.GetEnumerator();
            _state = 2;
        }

        if (_state == 2)
        {
            try
            {
                while(enumerator.MoveNext())
                {
                    if (_predicate(enumerator.Current))
                    {
                        _current = _index;
                        return true;
                    }
                    _index++;
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

    int IEnumerator<int>.Current => _current;

    object? IEnumerator.Current => _current;

    public void Dispose()
    {
        enumerator.Dispose();
    }
}