

using System;
using System.Collections.Generic;

namespace AlastairLundy.EnhancedLinq.Immediate.Dictionaries.Ranges;

public static partial class EnhancedLinqImmediateDictionary
{
    #if NETSTANDARD2_0
    /// <summary>
    /// 
    /// </summary>
    /// <param name="dictionary"></param>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    private static bool TryAdd<TKey, TValue>(IDictionary<TKey, TValue> dictionary,  TKey key, TValue value)
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