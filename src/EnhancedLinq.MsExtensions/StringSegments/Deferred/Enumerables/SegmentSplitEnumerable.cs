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

internal class SegmentSplitEnumerable : IEnumerable<StringSegment>
{
    private readonly StringSegment _segment;
    private readonly StringSegment _separator;

    internal SegmentSplitEnumerable(StringSegment segment,StringSegment separator)
    {
        _segment = segment;
        _separator = separator;
    }
    
    public IEnumerator<StringSegment> GetEnumerator()
    {
        if(_separator.Length == 1)
            return new SegmentSplitCharEnumerator(_segment, _separator.First());
        else
            return new SegmentSplitEnumerator(_segment, _separator);
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}