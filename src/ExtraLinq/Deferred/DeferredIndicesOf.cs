using System;
using System.Collections.Generic;
using ExtraLinq.Deferred.Enumerables;

namespace ExtraLinq.Deferred;

public static partial class EnumerableLinqExtra
{
    /// <summary>
    /// Gets the indices of the specified item within an IEnumerable.
    /// </summary>
    /// <param name="source">The IEnumerable to be searched.</param>
    /// <param name="target">The item to search for.</param>
    /// <typeparam name="T"></typeparam>
    /// <returns>The indices if the object is found; an empty sequence otherwise.</returns>
    public static IEnumerable<int> IndicesOf<T>(this IEnumerable<T> source, T target) where T : notnull
    {
        #if NET8_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(source);
        #endif
        
        return IndicesOf(source, x => x.Equals(target));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="predicate"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IEnumerable<int> IndicesOf<T>(this IEnumerable<T> source, Func<T, bool> predicate)
    {
#if NET8_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(source);
#endif
        
        return new IndicesEnumerable<T>(source, predicate);
    }
    
    /// <summary>
    /// Finds all occurrences of a specified char within a string, starting from the beginning of the string.
    /// </summary>
    /// <param name="str">The input string.</param>
    /// <param name="c">The character to find in the string.</param>
    /// <returns>
    /// A sequence of indices where the character is found; an empty sequence if the character could not be found.
    /// </returns>
    public static IEnumerable<int> IndicesOf(this string str, char c)
    {
#if NET8_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(str);
#endif
        
        return new IndicesEnumerable<char>(str, x => x.Equals(c));
    }

    
    /// <summary>
    /// Finds all occurrences of a specified substring within a string, starting from the beginning of the string.
    /// </summary>
    /// <param name="str">The input string.</param>
    /// <param name="value">The substring to look for.</param>
    /// <returns>A sequence of indices where the character is found; an empty sequence if the character could not be found.</returns>
    public static IEnumerable<int> IndicesOf(this string str, string value)
    {
#if NET8_0_OR_GREATER
        ArgumentException.ThrowIfNullOrEmpty(str);
        ArgumentException.ThrowIfNullOrEmpty(value);
#endif
        
        return new StringIndicesEnumerable(str, value.ToCharArray());
    }

}