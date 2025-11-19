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

namespace AlastairLundy.EnhancedLinq.Deferred.Enumerators.Ranges;

internal class InsertRangeEnumerator<T> : IEnumerator<T>
{
    private readonly IEnumerable<T> _source;
    private readonly int _indexToInsertAt;
    private readonly IEnumerable<T> _toBeInserted;
    
    private IEnumerator<T> _sourceEnumerator;
    private IEnumerator<T> _toBeInsertedEnumerator;

    private int _state;
    private int _index;

    internal InsertRangeEnumerator(IEnumerable<T> source, int indexToInsertAt, IEnumerable<T> toBeInserted)
    {
        _source = source;
        _indexToInsertAt = indexToInsertAt;
        _toBeInserted = toBeInserted;
        _state = 1;
    }
    
    public bool MoveNext()
    {
        if (_state == 1)
        {
            _index = 0;

            _sourceEnumerator = _source.GetEnumerator();
            _toBeInsertedEnumerator = _toBeInserted.GetEnumerator();

            _state = 2;
        }
        if (_state == 2)
        {
            if (_indexToInsertAt == 0)
            {
                while (_toBeInsertedEnumerator.MoveNext())
                {
                    Current = _toBeInsertedEnumerator.Current;
                    _index++;
                    return true;
                }

                while (_sourceEnumerator.MoveNext())
                {
                    Current = _sourceEnumerator.Current;
                    _index++;
                    return true;
                }
            }
            else
            {
                while (_sourceEnumerator.MoveNext())
                {
                    if (_index == _indexToInsertAt)
                    {
                        while (_toBeInsertedEnumerator.MoveNext())
                        {
                            Current = _toBeInsertedEnumerator.Current;
                            _index++;
                            return true;
                        }
                    }
                    else
                    {
                        Current = _sourceEnumerator.Current;
                        _index++;
                        return true;
                    }
                }
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

    public T Current { get; private set; }

    object? IEnumerator.Current => Current;

    public void Dispose()
    {
        _sourceEnumerator?.Dispose();
        _toBeInsertedEnumerator?.Dispose();
    }
}