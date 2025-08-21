/*
    EnhancedLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/.
 */

using System;
using System.Text;
using AlastairLundy.DotExtensions.MsExtensions.StringSegments;
using EnhancedLinq.MsExtensions.Internals.Localizations;
using Microsoft.Extensions.Primitives;

namespace EnhancedLinq.MsExtensions.StringSegments.Immediate;

internal static class ImmediateSegmentReverse
{
    /// <summary>
    /// Reverses the contents of the StringSegment.
    /// </summary>
    /// <param name="target">The StringSegment to reverse.</param>
    /// <returns>The reversed StringSegment.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the target StringSegment is Empty.</exception>
    internal static StringSegment Reverse(this StringSegment target)
    {
        if (target.IsEmpty())
            throw new InvalidOperationException(Resources.Exceptions_Segments_InvalidOperation_EmptySequence);
    
        StringBuilder stringBuilder = new();

        for (int i = 0; i < target.Length; i++)
        {
            if(target.Length - 1 - i >= 0)
                stringBuilder.Append(target[target.Length - 1 - i]);
        }
        
        return new StringSegment(stringBuilder.ToString());
    }
}