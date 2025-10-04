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

public static partial class EnhancedLinqMemoryImmediate
{
    /// <summary>
    /// Determines if none of the characters in the <see cref="StringSegment"/> match a predicate condition.
    /// </summary>
    /// <param name="segment">The <see cref="StringSegment"/> to be searched.</param>
    /// <param name="predicate">The predicate to check characters against.</param>
    /// <returns>True if none of the characters matched the predicate, false otherwise.</returns>
    /// <exception cref="ArgumentNullException">Thrown if the source <see cref="StringSegment"/> or predicate are null.</exception>
    public static bool None(this StringSegment segment, Func<char, bool> predicate)
    {
        if (StringSegment.IsNullOrEmpty(segment))
            throw new NullReferenceException();
        
#if NET8_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(predicate, nameof(predicate));
#endif

        for (int index = 0; index < segment.Length; index++)
        {
            if (predicate(segment[index]) == true)
            {
                return false;
            }
        }
        
        return true;
    }
}