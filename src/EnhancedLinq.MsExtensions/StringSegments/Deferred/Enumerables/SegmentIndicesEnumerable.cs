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
using AlastairLundy.EnhancedLinq.MsExtensions.StringSegments.Deferred.Enumerators;
using AlastairLundy.EnhancedLinq.MsExtensions.StringSegments.Immediate;
using Microsoft.Extensions.Primitives;

namespace AlastairLundy.EnhancedLinq.MsExtensions.StringSegments.Deferred.Enumerables;

internal class SegmentIndicesEnumerable : IEnumerable<int>
{
    private readonly StringSegment _source;
    private readonly StringSegment _segment;

    internal SegmentIndicesEnumerable(StringSegment source, char c)
    {
        _source = source;
        _segment = new  StringSegment($"{c}");
    }
    
    internal SegmentIndicesEnumerable(StringSegment source, StringSegment segment)
    {
        _source = source;
        _segment = segment;
    }

    public IEnumerator<int> GetEnumerator()
    {
        if(_segment.Length == 1)
            return new SegmentIndicesCharEnumerator(_source, _segment.First());
        else
            return new SegmentIndicesOfEnumerator(_source, _segment);
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}