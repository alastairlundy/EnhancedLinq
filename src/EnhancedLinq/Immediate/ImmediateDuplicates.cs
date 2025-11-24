/*
    EnhancedLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
    */

using System;
using System.Collections.Generic;

namespace AlastairLundy.EnhancedLinq.Immediate;

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
        {
            ArgumentNullException.ThrowIfNull(source);
            
            HashSet<T> hash = new();
        
            foreach (T item in source)
            {
                bool result = hash.Add(item);

                if (result == false)
                    return true;
            }

            return false;
        }
    }
}