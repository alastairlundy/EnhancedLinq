/*
    EnhancedLinq 
    Copyright (c) 2025-2026 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
    */

namespace EnhancedLinq.Immediate;

public static partial class EnhancedLinqImmediate
{
    /// <param name="source">The <see cref="IEnumerable{T}"/> to be searched.</param>
    /// <typeparam name="T">The type of objects in the <see cref="IEnumerable{T}"/>.</typeparam>
    extension<T>(IEnumerable<T> source) where T : notnull
    {
        /// <summary>
        /// Determines whether an <see cref="IEnumerable{T}"/> contains duplicate instances of an object.
        /// </summary>
        /// <returns>True if the <see cref="IEnumerable{T}"/> contains duplicate objects; false otherwise.</returns>
        public bool ContainsDuplicates()
            => source.ContainsDuplicates(EqualityComparer<T>.Default);

        /// <summary>
        /// Determines whether an <see cref="IEnumerable{T}"/> contains duplicate instances of an object.
        /// </summary>
        /// <param name="comparer">The <see cref="IEqualityComparer{T}"/> to be used to check for duplicates, or the default equality comparer if null.</param>
        /// <returns>True if the <see cref="IEnumerable{T}"/> contains duplicate objects; false otherwise.</returns>
        public bool ContainsDuplicates(IEqualityComparer<T>? comparer)
        {
            comparer ??= EqualityComparer<T>.Default;
            ArgumentNullException.ThrowIfNull(source);
            
            HashSet<T> hash = new(comparer: comparer);
        
            foreach (T item in source)
            {
                bool result = hash.Add(item);

                if (!result)
                    return true;
            }

            return false;
        }
    }
}