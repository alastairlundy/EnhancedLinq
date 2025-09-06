/*
    EnhancedLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/.
 */

using System;
using System.Collections.Generic;

using AlastairLundy.EnhancedLinq.MsExtensions.Internals.Localizations;

using Microsoft.Extensions.Primitives;

namespace AlastairLundy.EnhancedLinq.MsExtensions.StringSegments.Immediate;

public static partial class EnhancedLinqSegmentImmediate
{
    /// <summary>
    /// Retrieves a list of indices where the specified character can be found within the given <see cref="StringSegment"/>.
    /// </summary>
    /// <param name="source">The <see cref="StringSegment"/> to search.</param>
    /// <param name="c">The character to find and return its indices for.</param>
    /// <returns>A list containing the indices where the specified character can be found, or empty if not found.</returns>
    public static IList<int> IndicesOf(this StringSegment source, char c)
    {
        if(StringSegment.IsNullOrEmpty(source))
            throw new InvalidOperationException(Resources.Exceptions_Segments_InvalidOperation_EmptySequence);
        
        List<int> indices = new List<int>();
        
        for(int index = 0; index < source.Length; index++)
        {
            if (source[index] == c)
            {
                indices.Add(index);
            }
        }

        return indices;
    }
}