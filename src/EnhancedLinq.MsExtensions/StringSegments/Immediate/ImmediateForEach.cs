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
using Microsoft.Extensions.Primitives;

namespace AlastairLundy.EnhancedLinq.MsExtensions.StringSegments.Immediate;

/// <summary>
/// 
/// </summary>
public static partial class EnhancedLinqSegmentImmediate
{
    /// <summary>
    /// Applies the given action for each <see cref="char"/> in this <see cref="StringSegment"/>.
    /// </summary>
    /// <param name="action">The action to apply to each <see cref="char"/> in the <see cref="StringSegment"/>.</param>
    /// <param name="target">The <see cref="StringSegment"/> to have the action applied for.</param>
    public static void ForEach(this ref StringSegment target, Action<char> action)
    {
        for (int index = 0; index < target.Length; index++)
        {
            action.Invoke(target[index]);
        }
    }

    /// <summary>
    /// Applies the given func to each char in this <see cref="StringSegment"/>.
    /// </summary>
    /// <param name="target">The <see cref="StringSegment"/> to have the predicate applied to.</param>
    /// <param name="action">The func to apply to each element in the <see cref="StringSegment"/>.</param>
    public static void ForEach(this ref StringSegment target, Func<char, char> action)
    {
        char[] output = new char[target.Length];
        
        for (int i = 0; i < target.Length; i++)
        {
            output[i] = action.Invoke(target[i]);
        }

        target = new StringSegment(new string(output));
    }
}