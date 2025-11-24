/*
    EnhancedLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
    */

namespace AlastairLundy.EnhancedLinq.Memory.Immediate;

public static partial class EnhancedLinqMemoryImmediate
{
    /// <summary>
    /// Projects each element of a Span into a collection and flattens the resulting collections into a single array.
    /// </summary>
    /// <param name="source">The input Span whose elements are to be transformed.</param>
    /// <param name="resultSelector">A transform function to apply to each element of the source Span.</param>
    /// <typeparam name="TSource">The type of the elements in the source Span.</typeparam>
    /// <typeparam name="TResult">The type of the elements in the resulting collection.</typeparam>
    /// <returns>An array containing all the transformed elements of the input Span.</returns>
    /// <exception cref="OverflowException">Thrown when the resulting collection exceeds the maximum allowed size.</exception>
    public static TResult[] SelectMany<TSource, TResult>(this Span<TSource> source,
        Func<TSource, ICollection<TResult>> resultSelector)
    {
#if NET8_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(resultSelector, nameof(resultSelector));
#endif

        Span<ICollection<TResult>> newCollections = (from item in source
            select resultSelector(item));

        List<TResult> results = new List<TResult>();

        foreach (ICollection<TResult> collection in newCollections)
        {
#if NET8_0_OR_GREATER
            ArgumentNullException.ThrowIfNull(collection, nameof(resultSelector));
#endif
            
            foreach (TResult obj in collection)
            {
                if (results.Count == int.MaxValue || collection.Count + results.Count >= int.MaxValue)
                {
                    throw new OverflowException($"{nameof(results)} contains the maximum size of {int.MaxValue} and cannot be added to.");
                }
                
                results.Add(obj);
            }
        }
       
        return results.ToArray();
    }

    /// <summary>
    /// Projects each element of a Span into a collection, applies a projection function to each element of the resulting collections, and flattens them into a single array.
    /// </summary>
    /// <param name="source">The input Span whose elements are to be transformed.</param>
    /// <param name="collectionSelector">A function to extract a collection of intermediate elements from the input Span elements.</param>
    /// <param name="resultSelector">A function to transform an element from the source Span and an element of the intermediate collection into a final result.</param>
    /// <typeparam name="TSource">The type of the elements in the source Span.</typeparam>
    /// <typeparam name="TCollection">The type of the elements in the intermediate collections.</typeparam>
    /// <typeparam name="TResult">The type of the elements in the resulting array.</typeparam>
    /// <returns>An array containing all the transformed elements produced from the input Span.</returns>
    /// <exception cref="ArgumentException">Thrown when the source Span is empty.</exception>
    /// <exception cref="ArgumentNullException">Thrown when either the collectionSelector or resultSelector is null.</exception>
    /// <exception cref="OverflowException">Thrown when the total number of resulting elements exceeds the maximum allowed size.</exception>
    public static TResult[] SelectMany<TSource, TCollection, TResult>(this Span<TSource> source,
        Func<TSource, ICollection<TCollection>> collectionSelector, Func<TSource, TCollection, TResult> resultSelector)
    {
#if NET8_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(collectionSelector, nameof(collectionSelector));
        ArgumentNullException.ThrowIfNull(resultSelector, nameof(resultSelector));
#endif

        if (source.IsEmpty)
            throw new ArgumentException();

        List<TResult> results = new List<TResult>(capacity: source.Length);

       foreach (TSource item in source)
       {
           ICollection<TCollection> collection = collectionSelector(item);
           
#if NET8_0_OR_GREATER
           ArgumentNullException.ThrowIfNull(collection, nameof(collectionSelector));
#endif
           
           foreach (TCollection obj in collection)
           {
               if (results.Count == int.MaxValue || collection.Count + results.Count >= int.MaxValue)
               {
                   throw new OverflowException($"{nameof(results)} contains the maximum size of {int.MaxValue} and cannot be added to.");
               }
               
               results.Add(resultSelector(item, obj));
           }
       }
       
       return results.ToArray();
    }
}