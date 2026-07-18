/*
    EnhancedLinq 
    Copyright (c) 2025-2026 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
    */

using EnhancedLinq.Internals.Localizations;

namespace EnhancedLinq.MsExtensions.Internals;

internal static class StringSegmentGuard
{
    internal static void ThrowIfNullOrEmpty(in StringSegment segment)
    {
        if (!segment.HasValue || segment.Length == 0)
            throw new ArgumentException(Resources.Exceptions_StringSegment_Empty);
    }

    internal static void ThrowIfNullOrWhitespace(in StringSegment segment)
    {
        if (!segment.HasValue || segment.AsSpan().IsWhiteSpace())
            throw new ArgumentException(Resources.Exceptions_StringSegment_NullOrWhitespace);
    }
}
