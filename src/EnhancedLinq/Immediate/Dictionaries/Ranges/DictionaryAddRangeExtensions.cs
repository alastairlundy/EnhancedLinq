
/*
      EnhancedLinq
      Copyright (c) 2025 Alastair Lundy

     Licensed under the Apache License, Version 2.0 (the "License");
     you may not use this file except in compliance with the License.
     You may obtain a copy of the License at

         http://www.apache.org/licenses/LICENSE-2.0

     Unless required by applicable law or agreed to in writing, software
     distributed under the License is distributed on an "AS IS" BASIS,
     WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
     See the License for the specific language governing permissions and
     limitations under the License.
 */

using System;
using System.Collections.Generic;

namespace AlastairLundy.EnhancedLinq.Immediate.Dictionaries.Ranges;

/// <summary>
/// Provides extension methods for adding multiple items to dictionaries.
/// </summary>
public static partial class EnhancedLinqImmediateDictionary
{
    #if NETSTANDARD2_0
    /// <summary>
    /// Attempts to add a key-value pair to the dictionary.
    /// Returns true if the addition is successful, or false if the key already exists or the dictionary is read-only.
    /// </summary>
    /// <param name="dictionary">The dictionary to add the key-value pair to.</param>
    /// <param name="key">The key to add to the dictionary.</param>
    /// <param name="value">The value to associate with the specified key.</param>
    /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
    /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
    /// <returns>
    /// True if the key-value pair was added successfully; otherwise, false.
    /// </returns>
    private static bool TryAdd<TKey, TValue>(IDictionary<TKey, TValue> dictionary, TKey key, TValue value)
    {
        if (dictionary.IsReadOnly)
            return false;

        try
        {
            dictionary.Add(key, value);
            return true;
        }
        catch (ArgumentException)
        {
            return false;
        }
    }
    #endif
    
  #region Normal AddRange  
    /// <summary>
    /// Adds a sequence of KeyValuePair items to a Dictionary.
    /// </summary>
    /// <param name="source">The dictionary to add items to.</param>
    /// <param name="pairsToAdd">The items to be added.</param>
    /// <typeparam name="TKey">The type representing the Keys.</typeparam>
    /// <typeparam name="TValue">The type representing the Values.</typeparam>
    /// <exception cref="OverflowException">Thrown if the dictionary is unable to store all the Key Value Pairs to be added.</exception>
    /// <exception cref="InvalidOperationException">Thrown if the source dictionary is read-only.</exception>
    public static void AddRange<TKey, TValue>(this IDictionary<TKey, TValue> source,
        IEnumerable<KeyValuePair<TKey, TValue>> pairsToAdd)
    {
        if(source.IsReadOnly)
            throw new InvalidOperationException($"{nameof(source)} is read-only.");

#if NET8_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(pairsToAdd);
#endif
        
        foreach (KeyValuePair<TKey, TValue> pair in pairsToAdd)
        {
            if (source.Count == int.MaxValue)
                throw new OverflowException($"{nameof(source)} contains the maximum size of {int.MaxValue} and cannot be added to.");
            
            source.Add(pair.Key, pair.Value);
        }
    }

    /// <summary>
    /// Appends the contents of one Dictionary to the current dictionary.
    /// </summary>
    /// <param name="source">The dictionary to add items to.</param>
    /// <param name="dictionaryToAdd">The dictionary to be added to the existing dictionary.</param>
    /// <typeparam name="TKey">The type representing the Keys.</typeparam>
    /// <typeparam name="TValue">The type representing the Values.</typeparam>
    /// <exception cref="OverflowException">Thrown if the dictionary is unable to store all the dictionary values to be added.</exception>
    /// <exception cref="InvalidOperationException">Thrown if the source dictionary is read-only.</exception>
    public static void AddRange<TKey, TValue>(this IDictionary<TKey, TValue> source,
        IDictionary<TKey, TValue> dictionaryToAdd)
    {
        if(source.IsReadOnly)
            throw new InvalidOperationException($"{nameof(source)} is read-only.");
        
#if NET8_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(dictionaryToAdd);
#endif

        if (source.Count == int.MaxValue)
            throw new OverflowException(
                $"{nameof(source)} contains the maximum size of {int.MaxValue} and cannot be added to.");
        else if (dictionaryToAdd.Count == int.MaxValue)
            throw new OverflowException($"{nameof(dictionaryToAdd)} contains the maximum size of {int.MaxValue} and cannot be added to {nameof(source)}.");
         
        foreach (KeyValuePair<TKey, TValue> pair in dictionaryToAdd)
        {
            if (source.Count == int.MaxValue)
                throw new OverflowException($"{nameof(source)} contains the maximum size of {int.MaxValue} and cannot be added to.");
            
            source.Add(pair.Key, pair.Value);
        }
    }
    #endregion

    #region  Try AddRange
    
    /// <summary>
    /// Attempts to add a sequence of KeyValuePair items to a Dictionary.
    /// </summary>
    /// <param name="source">The dictionary to add items to.</param>
    /// <param name="pairsToAdd">The items to be added.</param>
    /// <typeparam name="TKey">The type representing the Keys.</typeparam>
    /// <typeparam name="TValue">The type representing the Values.</typeparam>
    /// <exception cref="OverflowException">Thrown if the dictionary is unable to store all the Key Value Pairs to be added.</exception>
    /// <exception cref="InvalidOperationException">Thrown if the source dictionary is read-only.</exception>
    public static void TryAddRange<TKey, TValue>(this IDictionary<TKey, TValue> source,
        IEnumerable<KeyValuePair<TKey, TValue>> pairsToAdd) where TKey : notnull
    {
        #if NET8_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(pairsToAdd);
        #endif
        
        if(source.IsReadOnly)
            throw new InvalidOperationException($"{nameof(source)} is read-only.");

        if (source.Count == int.MaxValue)
            throw new OverflowException($"{nameof(source)} contains the maximum size of {int.MaxValue} and cannot be added to.");
        
        foreach (KeyValuePair<TKey, TValue> pair in pairsToAdd)
        {
            if (source.Count == int.MaxValue)
                throw new OverflowException($"{nameof(source)} contains the maximum size of {int.MaxValue} and cannot be added to.");
            
            #if NETSTANDARD2_0
            TryAdd(source, pair.Key, pair.Value);
            #else
            source.TryAdd(pair.Key, pair.Value);
            #endif
        }
    }

    /// <summary>
    /// Attempts to append the contents of one Dictionary to the current dictionary.
    /// </summary>
    /// <param name="source">The dictionary to try to add items to.</param>
    /// <param name="dictionaryToAdd">The dictionary to attempt to be added to the existing dictionary.</param>
    /// <typeparam name="TKey">The type representing the Keys.</typeparam>
    /// <typeparam name="TValue">The type representing the Values.</typeparam>
    /// <exception cref="OverflowException">Thrown if the dictionary is unable to store all the dictionary values to be added.</exception>
    /// <exception cref="InvalidOperationException">Thrown if the source dictionary is read-only.</exception>
    public static void TryAddRange<TKey, TValue>(this IDictionary<TKey, TValue> source,
        IDictionary<TKey, TValue> dictionaryToAdd) where TKey : notnull
    {
#if NET8_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(dictionaryToAdd);
#endif
        
        if(source.IsReadOnly)
            throw new InvalidOperationException($"{nameof(source)} is read-only.");

        if (source.Count == int.MaxValue)
            throw new OverflowException(
                $"{nameof(source)} contains the maximum size of {int.MaxValue} and cannot be added to.");
        else if (dictionaryToAdd.Count == int.MaxValue)
            throw new OverflowException($"{nameof(dictionaryToAdd)} contains the maximum size of {int.MaxValue} and cannot be added to {nameof(source)}.");

        foreach (KeyValuePair<TKey, TValue> pair in dictionaryToAdd)
        {
            if (source.Count == int.MaxValue)
                throw new OverflowException($"{nameof(source)} contains the maximum size of {int.MaxValue} and cannot be added to.");
            
#if NETSTANDARD2_0
            TryAdd(source, pair.Key, pair.Value);
#else
            source.TryAdd(pair.Key, pair.Value);
#endif
        }
    }

    #endregion
}