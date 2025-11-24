
/*
    EnhancedLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
    */

using System;
using System.Collections.Generic;

namespace AlastairLundy.EnhancedLinq.Immediate.Dictionaries.Ranges;

/// <summary>
/// Provides extension methods for adding multiple items to dictionaries.
/// </summary>
public static partial class EnhancedLinqImmediateDictionary
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="source">The dictionary to add items to.</param>
  /// <typeparam name="TKey">The type representing the Keys.</typeparam>
  /// <typeparam name="TValue">The type representing the Values.</typeparam>
  extension<TKey, TValue>(IDictionary<TKey, TValue> source)
      where TKey : notnull
  {
      
      #region Normal AddRange
      /// <summary>
      /// Adds a sequence of KeyValuePair items to a Dictionary.
      /// </summary>
      /// <param name="pairsToAdd">The items to be added.</param>
      /// <exception cref="OverflowException">Thrown if the dictionary is unable to store all the Key Value Pairs to be added.</exception>
      /// <exception cref="InvalidOperationException">Thrown if the source dictionary is read-only.</exception>
      public void AddRange(IEnumerable<KeyValuePair<TKey, TValue>> pairsToAdd)
      {
          if(source.IsReadOnly)
              throw new InvalidOperationException($"{nameof(source)} is read-only.");

          ArgumentNullException.ThrowIfNull(source);
          ArgumentNullException.ThrowIfNull(pairsToAdd);
        
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
      /// <param name="dictionaryToAdd">The dictionary to be added to the existing dictionary.</param>
      /// <exception cref="OverflowException">Thrown if the dictionary is unable to store all the dictionary values to be added.</exception>
      /// <exception cref="InvalidOperationException">Thrown if the source dictionary is read-only.</exception>
      public void AddRange(IDictionary<TKey, TValue> dictionaryToAdd)
      {
          if(source.IsReadOnly)
              throw new InvalidOperationException($"{nameof(source)} is read-only.");
        
          ArgumentNullException.ThrowIfNull(source);
          ArgumentNullException.ThrowIfNull(dictionaryToAdd);

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
    /// <param name="pairsToAdd">The items to be added.</param>
    /// <exception cref="OverflowException">Thrown if the dictionary is unable to store all the Key Value Pairs to be added.</exception>
    /// <exception cref="InvalidOperationException">Thrown if the source dictionary is read-only.</exception>
    public void TryAddRange(
        IEnumerable<KeyValuePair<TKey, TValue>> pairsToAdd)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(pairsToAdd);
        
        if(source.IsReadOnly)
            throw new InvalidOperationException($"{nameof(source)} is read-only.");

        if (source.Count == int.MaxValue)
            throw new OverflowException($"{nameof(source)} contains the maximum size of {int.MaxValue} and cannot be added to.");
        
        foreach (KeyValuePair<TKey, TValue> pair in pairsToAdd)
        {
            if (source.Count == int.MaxValue)
                throw new OverflowException($"{nameof(source)} contains the maximum size of {int.MaxValue} and cannot be added to.");
            
            source.TryAdd(pair.Key, pair.Value);
        }
    }

    /// <summary>
    /// Attempts to append the contents of one Dictionary to the current dictionary.
    /// </summary>
    /// <param name="dictionaryToAdd">The dictionary to attempt to be added to the existing dictionary.</param>
    /// <exception cref="OverflowException">Thrown if the dictionary is unable to store all the dictionary values to be added.</exception>
    /// <exception cref="InvalidOperationException">Thrown if the source dictionary is read-only.</exception>
    public void TryAddRange(IDictionary<TKey, TValue> dictionaryToAdd) 
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(dictionaryToAdd);
        
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
            
            source.TryAdd(pair.Key, pair.Value);
        }
    }

    #endregion
  }
}