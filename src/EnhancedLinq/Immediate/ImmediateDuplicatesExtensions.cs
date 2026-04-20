/*
    EnhancedLinq 
    Copyright (c) 2025-2026 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
    */

namespace EnhancedLinq.Immediate;

/// <summary>
/// Extension methods for detecting duplicates in immediate sequences.
/// </summary>
public static class ImmediateDuplicatesExtensions
{
    /// <param name="source">The <see cref="IEnumerable{T}"/> to be searched.</param>
    /// <typeparam name="T">The type of objects in the <see cref="IEnumerable{T}"/>.</typeparam>
    extension<T>(IEnumerable<T> source) where T : notnull
    {
        /// <summary>
        /// Determines whether an <see cref="IEnumerable{T}"/> contains duplicate instances of an object.
        /// </summary>
        /// <param name="comparer">The <see cref="IEqualityComparer{T}"/> to be used to check for duplicates, or the default equality comparer if null.</param>
        /// <returns>True if the <see cref="IEnumerable{T}"/> contains duplicate objects; false otherwise.</returns>
        public bool ContainsDuplicates(IEqualityComparer<T>? comparer = null)
        { 
            ArgumentNullException.ThrowIfNull(source);
        
            comparer ??= EqualityComparer<T>.Default;

#if NET8_0_OR_GREATER
            HashSet<T> hash;
            
            if (source is ICollection<T> collection)
            {
                hash = new HashSet<T>(collection.Count, comparer);
            }
            else
            {
                hash =  new HashSet<T>(comparer: comparer);
            }
#else
            HashSet<T> hash = new HashSet<T>(comparer);
#endif
        
            foreach (T item in source)
            {
                if (!hash.Add(item))
                    return true;
            }
        
            return false;
        }
    }
}
