/*
    EnhancedLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
    */

using System;
using System.Collections.Generic;
using AlastairLundy.DotExtensions.MsExtensions.StringSegments;
using EnhancedLinq.MsExtensions.Internals.Infra;
using EnhancedLinq.MsExtensions.StringSegments.Deferred.Enumerables;
using EnhancedLinq.MsExtensions.StringSegments.Deferred.Enumerators;
using Microsoft.Extensions.Primitives;

namespace EnhancedLinq.MsExtensions.StringSegments.Deferred;

public static partial class EnhancedLinqSegmentDeferred
{
    /// <param name="source">The <see cref="StringSegment"/> to split.</param>
    extension(StringSegment source)
    {
        /// <summary>
        /// Splits the given <see cref="StringSegment"/> into segments separated by the specified character.
        /// </summary>
        /// <param name="separator">The character separator to split by.</param>
        /// <returns>An <see cref="IEnumerable{StringSegment}"/> containing the split segments. Returns an empty sequence if the separator is not found.</returns>
        public IEnumerable<StringSegment> SplitBy(char separator)
        {
            if (StringSegment.IsNullOrEmpty(source))
                throw new ArgumentException();
        
            if (source.Contains(separator) == false)
                return [source];

            return SplitBy(source, x => x == separator);
        }

        /// <summary>
        /// Splits the given <see cref="StringSegment"/> into segments separated by the specified <see cref="StringSegment"/> separator.
        /// </summary>
        /// <param name="separator">The <see cref="StringSegment"/> separator to split by.</param>
        /// <returns>An <see cref="IEnumerable{StringSegment}"/> containing the split segments. Returns an empty sequence if the separator is not found.</returns>
        /// <exception cref="ArgumentException">Thrown if <paramref name="separator"/> or <paramref name="source"/> is null or empty.</exception>
        public IEnumerable<StringSegment> SplitBy(StringSegment separator)
        {
            if (StringSegment.IsNullOrEmpty(separator) || StringSegment.IsNullOrEmpty(source))
                throw new ArgumentException();
            
            if (source.Contains(separator) == false)
                return [source];
        
            return new SegmentSplitEnumerable(source, separator);
        }

        /// <summary>
        /// Splits the given <see cref="StringSegment"/> into segments when the predicate evaluates to true.
        /// </summary>
        /// <param name="predicate">The predicate to split on when true.</param>
        /// <returns>An <see cref="IEnumerable{StringSegment}"/> containing the split segments.</returns>
        public IEnumerable<StringSegment> SplitBy(Func<char, bool> predicate)
        {
            if (StringSegment.IsNullOrEmpty(source))
                throw new ArgumentException();

            return new CustomEnumeratorEnumerable<StringSegment>
                (new SegmentSplitPredicateEnumerator(source, predicate));
        }
    }
}