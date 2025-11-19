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

#if NET8_0_OR_GREATER

using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;

namespace AlastairLundy.EnhancedLinq.Deferred.Enumerators.NumberRanges;

internal class NumberRangeEnumerator<TNumber> : IEnumerator<TNumber> where TNumber : INumber<TNumber>
{
    private readonly IEnumerable<TNumber> _source;   
    private IEnumerator<TNumber> _enumerator;
    
    private TNumber _current;

    private int _state;

    internal NumberRangeEnumerator(IEnumerable<TNumber> source)
    {
        _source = source;
        _current = TNumber.Zero;
        _state = 0;
        _enumerator = _source.GetEnumerator();
    }
    
    public bool MoveNext()
    {
        if (_state == 1)
        {
            try
            {
                while (_enumerator.MoveNext())
                {
                    _current = _enumerator.Current;
                    return true;
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

    TNumber IEnumerator<TNumber>.Current => _current;

    object? IEnumerator.Current => _current;

    public void Dispose()
    {
        _enumerator.Dispose();
    }
}
#endif