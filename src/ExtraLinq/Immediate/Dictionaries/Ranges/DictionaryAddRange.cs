using System;
using System.Collections.Generic;

namespace ExtraLinq.Immediate.Dictionaries.Ranges;

public static class DictionaryAddRange
{
        /// <summary>
    /// Adds an IEnumerable of KeyValuePair items to a Dictionary.
    /// </summary>
    /// <param name="source">The dictionary to add items to.</param>
    /// <param name="pairsToAdd">The items to be added.</param>
    /// <typeparam name="TKey">The type representing the Keys.</typeparam>
    /// <typeparam name="TValue">The type representing the Values.</typeparam>
    /// <exception cref="OverflowException">Thrown if the dictionary is unable to store all the Key Value Pairs to be added.</exception>
    public static void AddRange<TKey, TValue>(this IDictionary<TKey, TValue> source,
        IEnumerable<KeyValuePair<TKey, TValue>> pairsToAdd)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(pairsToAdd);
        
        if (source.IsReadOnly)
            throw new NotSupportedException();
        
        foreach (KeyValuePair<TKey, TValue> pair in pairsToAdd)
        {
            if (source.Count == int.MaxValue)
            {
                throw new OverflowException($"{nameof(source)} contains the maximum size of {int.MaxValue} and cannot be added to.");
            }
            
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
    public static void AddRange<TKey, TValue>(this IDictionary<TKey, TValue> source,
        IDictionary<TKey, TValue> dictionaryToAdd)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(dictionaryToAdd);
        
        if (source.IsReadOnly)
            throw new NotSupportedException();
        
        if (source.Count == int.MaxValue)
        {
            throw new OverflowException($"{nameof(source)} contains the maximum size of {int.MaxValue} and cannot be added to.");
        }

        if (dictionaryToAdd.Count == int.MaxValue)
        {
            throw new OverflowException($"{nameof(dictionaryToAdd)} contains the maximum size of {int.MaxValue} and cannot be added to {nameof(source)}.");
        }

        foreach (KeyValuePair<TKey, TValue> pair in dictionaryToAdd)
        {
            if (source.Count == int.MaxValue)
            {
                throw new OverflowException($"{nameof(source)} contains the maximum size of {int.MaxValue} and cannot be added to.");
            }
            
            source.Add(pair.Key, pair.Value);
        }
    }
}