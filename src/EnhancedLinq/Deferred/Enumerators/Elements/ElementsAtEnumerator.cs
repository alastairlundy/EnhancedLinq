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
using AlastairLundy.EnhancedLinq.Immediate;

namespace AlastairLundy.EnhancedLinq.Deferred.Enumerators;

internal class ElementsAtEnumerator<TSource> : IEnumerator<TSource>
{
    private IEnumerator<int> _indicesEnumerator;
    
    private readonly IEnumerable<TSource> _source;
    private readonly IEnumerable<int> _indices;

    private int _state;
    
    private TSource _current;
    
    internal ElementsAtEnumerator(IEnumerable<TSource> source, IEnumerable<int> indices)
    {
        _source = source;
        _indices = indices;
        _state = 1;
    }

    public bool MoveNext()
    {
        if (_state == 1)
        {
            _indicesEnumerator = _indices.GetEnumerator();
            
            _state = 2;
        }

        if (_state == 2)
        {
            try
            {
                if(_indicesEnumerator.MoveNext())
                {
                    _current = _source.ElementAt(_indicesEnumerator.Current);
                    return true;
                }

                return false;
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

    public TSource Current => _current;

    object? IEnumerator.Current => _current;

    public void Dispose()
    {
        _indicesEnumerator?.Dispose();
    }
}