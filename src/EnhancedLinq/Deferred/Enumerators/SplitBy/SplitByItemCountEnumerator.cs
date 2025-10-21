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

internal class SplitByItemCountEnumerator<T> : IEnumerator<IEnumerable<T>>
{
    private IEnumerator<T> _enumerator;
    
    private readonly IEnumerable<T> _source;
    private readonly int _maximumItemCount;
    private readonly int _maxEnumerableCount;
    
    private List<T> _current;

    private int _currentEnumerableCount;
    private int _currentItemCount;
    
    private int _state;
    
    internal SplitByItemCountEnumerator(IEnumerable<T> source, int maximumItemCount)
    {
        _source = source;
        _maximumItemCount = maximumItemCount;
        
        _state = 0;
        _currentItemCount = 0;

        if (maximumItemCount <= 0)
            throw new ArgumentOutOfRangeException(nameof(maximumItemCount));

        _maxEnumerableCount = -1;
    }
    
    internal SplitByItemCountEnumerator(IEnumerable<T> source, int maximumItemCount, int maxEnumerableCount)
    {
        _source = source;
        _maximumItemCount = maximumItemCount;
        
        _state = 0;
        _currentItemCount = 0;

        if (maximumItemCount <= 0)
            throw new ArgumentOutOfRangeException(nameof(maximumItemCount));
        
        if(maxEnumerableCount > 0)
            _maxEnumerableCount = maxEnumerableCount;
        else
            _maxEnumerableCount = -1;
    }

    public bool MoveNext()
    {
        if (_state == 1)
        {
            _enumerator = _source.GetEnumerator();
            _state = 2;
        }
        
        if (_state == 2)
        {
            try
            {
                _currentEnumerableCount = 0;
                List<T> tempList = new List<T>();
                
                while(_enumerator.MoveNext())
                {
                    if (_currentItemCount < _maximumItemCount)
                    {
                        tempList.Add(_enumerator.Current);
                        _currentItemCount++;
                    }
                    else if(_currentEnumerableCount < _maxEnumerableCount)
                    {
                        _current = new List<T>(tempList);
                        _currentEnumerableCount++;
                        tempList.Clear();
                        return true;
                    }
                    else
                    {
                        _current = new List<T>(tempList);
                        _currentEnumerableCount++;
                        tempList.Clear();
                        break;
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

    IEnumerable<T> IEnumerator<IEnumerable<T>>.Current => _current;

    object? IEnumerator.Current => _current;

    public void Dispose()
    {
        _enumerator.Dispose();
    }
}