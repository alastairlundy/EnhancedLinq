/*
    EnhancedLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
    */

namespace EnhancedLinq.Memory.Immediate;

public static partial class EnhancedLinqMemoryImmediate
{
    /// <param name="source">The input Span whose elements are to be transformed.</param>
    /// <typeparam name="TSource">The type of the elements in the source Span.</typeparam>
    extension<TSource>(Span<TSource> source)
    {
        /// <summary>
        /// Projects each element of a Span into a collection and flattens the resulting collections into a single array.
        /// </summary>
        /// <param name="resultSelector">A transform function to apply to each element of the source Span.</param>
        /// <typeparam name="TResult">The type of the elements in the resulting collection.</typeparam>
        /// <returns>An array containing all the transformed elements of the input Span.</returns>
        /// <exception cref="OverflowException">Thrown when the resulting collection exceeds the maximum allowed size.</exception>
        public TResult[] SelectMany<TResult>(Func<TSource, ICollection<TResult>> resultSelector)
        {
            ArgumentNullException.ThrowIfNull(resultSelector);

            Span<ICollection<TResult>> newCollections = (from item in source
                select resultSelector(item));

            List<TResult> results = new List<TResult>();

            foreach (ICollection<TResult> collection in newCollections)
            {
                ArgumentNullException.ThrowIfNull(collection);
            
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
        /// <param name="collectionSelector">A function to extract a collection of intermediate elements from the input Span elements.</param>
        /// <param name="resultSelector">A function to transform an element from the source Span and an element of the intermediate collection into a final result.</param>
        /// <typeparam name="TCollection">The type of the elements in the intermediate collections.</typeparam>
        /// <typeparam name="TResult">The type of the elements in the resulting array.</typeparam>
        /// <returns>An array containing all the transformed elements produced from the input Span.</returns>
        /// <exception cref="ArgumentException">Thrown when the source Span is empty.</exception>
        /// <exception cref="ArgumentNullException">Thrown when either the collectionSelector or resultSelector is null.</exception>
        /// <exception cref="OverflowException">Thrown when the total number of resulting elements exceeds the maximum allowed size.</exception>
        public TResult[] SelectMany<TCollection, TResult>(Func<TSource, ICollection<TCollection>> collectionSelector, Func<TSource, TCollection, TResult> resultSelector)
        {
            ArgumentNullException.ThrowIfNull(collectionSelector);
            ArgumentNullException.ThrowIfNull(resultSelector);

            if (source.IsEmpty)
                throw new ArgumentException();

            List<TResult> results = new List<TResult>(capacity: source.Length);

            foreach (TSource item in source)
            {
                ICollection<TCollection> collection = collectionSelector(item);
           
                ArgumentNullException.ThrowIfNull(collection, nameof(collectionSelector));
           
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
}