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

using System.Collections;
using System.Collections.Generic;
using System.Numerics;

using AlastairLundy.EnhancedLinq.Deferred.Enumerators.NumberRanges;

namespace AlastairLundy.EnhancedLinq.Deferred.Enumerables.NumberRanges;

internal class NumberRangeEnumerable<TNumber> : IEnumerable<TNumber> where TNumber : INumber<TNumber>
{
    private List<TNumber> _source;
    
    internal NumberRangeEnumerable(TNumber start, TNumber count, TNumber incrementor)
    {
        _source = new List<TNumber>();
        
        TNumber current = start;
        TNumber end = start + count;
        
        while (current != end + TNumber.One)
        {
            _source.Add(current);
            
            current += incrementor;
        }
    }
    
    public IEnumerator<TNumber> GetEnumerator()
    {
        return new NumberRangeEnumerator<TNumber>(_source);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
#endif