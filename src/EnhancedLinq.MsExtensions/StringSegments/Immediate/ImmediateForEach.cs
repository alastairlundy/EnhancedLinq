/*
    EnhancedLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/.
 */

using System;
using Microsoft.Extensions.Primitives;

namespace AlastairLundy.EnhancedLinq.MsExtensions.StringSegments.Immediate;

/// <summary>
/// 
/// </summary>
public static partial class EnhancedLinqImmediateSegment
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
    /// <param name="target">The <see cref="StringSegment"/> to have the selector applied to.</param>
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