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

internal class StringIndicesEnumerator : IEnumerator<int>
{
    private readonly string _str;
    private readonly string _substring;
    
    private readonly IEnumerable<int> _indices;
    
    private readonly IEnumerator<int> _indicesEnumerator;
    
    private int _index;
    private int _current;

    private int _state;

    internal StringIndicesEnumerator(string str, string substring)
    {
        _str = str;
        _substring = substring;
        _index = 0;

        _indices = str.IndicesOf(substring[0]);
        _indicesEnumerator = _indices.GetEnumerator();
    }

    public bool MoveNext()
    {
        if (_state == 1)
        {
            try
            {
                while (_indicesEnumerator.MoveNext())
                {
                    string compare = _str.Substring(_indicesEnumerator.Current,
                        _substring.Length);

                    if (_substring.Equals(compare))
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
            finally
            {
                _state = -1;
            }
        }
        
        Dispose();
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
      _indicesEnumerator.Dispose();
    }
}