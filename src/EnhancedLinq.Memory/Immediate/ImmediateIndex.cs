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
using System.Collections.Generic;

namespace AlastairLundy.EnhancedLinq.Memory.Immediate;

public static partial class EnhancedLinqMemoryImmediate
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="span"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static ICollection<int> Index<T>(this Span<T> span)
        => Index(span, 0);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="span"></param>
    /// <param name="startIndex"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public static ICollection<int> Index<T>(this Span<T> span, int startIndex)
    {
        if(startIndex < 0 || startIndex >= span.Length)
            throw new ArgumentOutOfRangeException(nameof(startIndex));
        
        List<int> output = new();
        
        for (int i = 0; i < span.Length; i++)
        {
            if(i >= startIndex)
                output.Add(i);
        }
        
        return output;
    }

}