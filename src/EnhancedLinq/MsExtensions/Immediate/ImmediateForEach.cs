/*
    EnhancedLinq 
    Copyright (c) 2025-2026 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
    */

namespace EnhancedLinq.MsExtensions.Immediate;

/// <summary>
/// 
/// </summary>
public static partial class EnhancedLinqSegmentImmediate
{
    /// <param name="target">The <see cref="StringSegment"/> to have the action applied for.</param>
    extension(ref StringSegment target)
    {
        /// <summary>
        /// Applies the given action for each <see cref="char"/> in this <see cref="StringSegment"/>.
        /// </summary>
        /// <param name="action">The action to apply to each <see cref="char"/> in the <see cref="StringSegment"/>.</param>
        public void ForEach(Action<char> action)
        {
            ArgumentException.ThrowIfNullOrWhitespace(target);

            for (int index = 0; index < target.Length; index++)
            {
                action.Invoke(target[index]);
            }
        }

        /// <summary>
        /// Applies the given func to each char in this <see cref="StringSegment"/>.
        /// </summary>
        /// <param name="action">The func to apply to each element in the <see cref="StringSegment"/>.</param>
        public void ForEach(Func<char, char> action)
        {
            ArgumentException.ThrowIfNullOrWhitespace(target);
            ArgumentNullException.ThrowIfNull(action);
            
            char[] output = new char[target.Length];
        
            for (int i = 0; i < target.Length; i++)
            {
                output[i] = action.Invoke(target[i]);
            }

            target = new StringSegment(new string(output));
        }
    }
}