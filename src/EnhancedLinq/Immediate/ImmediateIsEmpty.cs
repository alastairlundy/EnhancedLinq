/*
    EnhancedLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
    */

using System;
using System.Collections.Generic;
using System.Linq;

namespace AlastairLundy.EnhancedLinq.Immediate;

public static partial class EnhancedLinqImmediate
{
    /// <param name="source">The sequence to check.</param>
    /// <typeparam name="T">The type of element stored in the sequence.</typeparam>
    extension<T>(IEnumerable<T> source)
    {
        /// <summary>
        /// Determines if a sequence is empty or not.
        /// </summary>
        /// <returns>True if the sequence is empty, false otherwise.</returns>
        public bool IsEmpty()
        {
            ArgumentNullException.ThrowIfNull(source);
            
            if (source is ICollection<T> collection)
                return collection.Count == 0;

            return !source.Any();
        }
    }
}