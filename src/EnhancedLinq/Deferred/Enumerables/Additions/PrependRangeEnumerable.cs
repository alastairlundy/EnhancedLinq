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

using System.Collections;
using System.Collections.Generic;

namespace AlastairLundy.EnhancedLinq.Deferred.Enumerables;

internal class PrependRangeEnumerable<TSource> : IEnumerable<TSource>
{
    private readonly IEnumerable<TSource> _source;
    private readonly IEnumerable<TSource> _toBePrepended;

    internal PrependRangeEnumerable(IEnumerable<TSource> source, IEnumerable<TSource> toBePrepended)
    {
        _source = source;
        _toBePrepended = toBePrepended;
    }
    
    public IEnumerator<TSource> GetEnumerator()
    {
        IEnumerator<TSource> sourceEnumerator = _source.GetEnumerator();
        IEnumerator<TSource> prependEnumerator = _toBePrepended.GetEnumerator();
        
        try
        {
            while (prependEnumerator.MoveNext())
            {
                yield return prependEnumerator.Current;
            }
            
            while (sourceEnumerator.MoveNext())
            {
                yield return sourceEnumerator.Current;
            }
        }
        finally
        {
            sourceEnumerator.Dispose();
            prependEnumerator.Dispose();
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}