/*
    EnhancedLinq 
    Copyright (c) 2025-2026 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
    */

namespace EnhancedLinq.Immediate.Lists;

/// <summary>
/// Provides extension methods to determine the last index of an element in a collection
/// with immediate execution. These methods are designed to enable efficient retrieval
/// of the last index in scenarios where deferred execution is not appropriate or desired.
/// </summary>
public static class ImmediateLastIndexExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <typeparam name="T">The type of element in the collection.</typeparam>
    extension<T>(ICollection<T> source)
    {
        /// <summary>
        /// Provides the last index of an item in a collection.
        /// </summary>
        /// <returns>The last index of an item in a collection.</returns>
        public int LastIndex()
        {
            ArgumentNullException.ThrowIfNull(source);
        
            if(source.Count > 0)
                return source.Count - 1;

            return -1;
        }
    }
}