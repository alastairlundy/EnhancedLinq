/*
    EnhancedLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
    */

using EnhancedLinq.Deferred.Enumerators;

namespace EnhancedLinq.Deferred;

public static partial class EnhancedLinqDeferred
{
    /// <param name="source">The sequence to find duplicates in.</param>
    /// <typeparam name="TSource">The type of elements in the source sequence.</typeparam>
    extension<TSource>(IEnumerable<TSource> source) where TSource : IEquatable<TSource>
    {
        /// <summary>
        /// Returns a sequence of duplicate elements from the source sequence using the default equality comparer.
        /// </summary>
        /// <returns>A sequence that contains only duplicate elements from the source sequence.</returns>
        public IEnumerable<TSource> FindDuplicates() => source.FindDuplicates(EqualityComparer<TSource>.Default);
    }

    /// <param name="source">The sequence to find duplicates in.</param>
    /// <typeparam name="TSource">The type of elements in the source sequence.</typeparam>
    extension<TSource>(IEnumerable<TSource> source) where TSource : IEquatable<TSource>
    {
        /// <summary>
        /// Returns a sequence of duplicate elements from the source sequence using the specified equality comparer.
        /// </summary>
        /// <param name="comparer">The equality comparer to use for determining duplicates.</param>
        /// <returns>A sequence that contains only duplicate elements from the source sequence.</returns>
        public IEnumerable<TSource> FindDuplicates(IEqualityComparer<TSource> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(comparer);
        
            return new CustomEnumeratorEnumerable<TSource>(
                new DuplicatesEnumerator<TSource>(source, comparer));
        }
    }


}