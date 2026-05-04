# EnhancedLinq.Async

Additional LINQ style Deferred and Immediate enumeration mode extension methods for .NET.

## Comparison: EnhancedLinq vs EnhancedLinq.Async

Use `EnhancedLinq` when working with synchronous collections (`IEnumerable<T>`). Use `EnhancedLinq.Async` when working with asynchronous streams (`IAsyncEnumerable<T>`). The async versions are designed to handle asynchronous data sources without blocking the calling thread.

## Deferred Mode

Methods in this section use deferred execution, meaning they return an `IAsyncEnumerable<T>` and do not perform any work until the sequence is enumerated.

| Name | Description | Overloads | Notes |
| :--- | :--- | :---: | :--- |
| `Exclude` | Excludes elements that match a specified condition or match a specified sequence. | 2 | |
| `SplitByItemCount` | Splits the sequence into chunks of a specified size. | 1 | |
| `SplitByProcessorCount` | Splits the sequence based on the number of available processors. | 1 | |
| `SplitBy` | Splits the sequence whenever a specified condition is met or a specific separator is encountered. | 2 | |
| `GenerateNumberRange` | Generates a range of numbers. | 2 | |
| `ElementsAt` | Returns elements at specified indices, a range, or starting from a specific index for a given count. | 3 | |
| `FindDuplicates` | Returns elements that appear more than once in the sequence. | 2 | Supports custom `IEqualityComparer<T>`. |
| `FirstNIndicesOf` | Returns the first $N$ indices where the elements match a target or satisfy a predicate. | 2 | |
| `LastNIndicesOf` | Returns the last $N$ indices where the elements match a target or satisfy a predicate. | 2 | |
| `IndicesOfAsync` | Returns all indices where the elements match a target or satisfy a predicate. | 2 | |
| `WhereAsync` | Filters a sequence based on an asynchronous predicate. | 1 | |

## Immediate Mode

Methods in this section use immediate execution. They process the `IAsyncEnumerable<T>` and return a result (often as a `Task` or a value) immediately upon call.

| Name | Description | Overloads | Notes |
| :--- | :--- | :---: | :--- |
| `LastIndexOfAsync` | Returns the index of the last occurrence of a specified value or element that matches a predicate. | 2 | |
| `ForEachAsync` | Performs a specified action on each element of the sequence asynchronously. | 2 | Implemented as an `IAsyncEnumerable<T>` generator yielding results. |
| `ContainsDuplicates` | Determines whether the sequence contains any duplicate elements. | 2 | Supports custom `IEqualityComparer<T>`. |
| `HasNoMatchesAsync` | Determines whether no elements in the sequence satisfy a specified predicate. | 1 | |
| `IndexOfAsync` | Returns the index of the first occurrence of a specified value or element that matches a predicate. | 2 | |
| `CountAtMostAsync` | Returns true if the number of elements that satisfy a condition is less than or equal to a specified count. | 2 | |
| `CountAtLeastAsync` | Returns true if the number of elements that satisfy a condition is greater than or equal to a specified count. | 2 | |


**Performance Warning:** 
Repeatedly enumerating an `IAsyncEnumerable<T>` can lead to significant performance penalties as the underlying data source may be re-queried each time. If you need to use the results of an asynchronous sequence multiple times, consider materializing it first using `ToListAsync()` or `ToArrayAsync()`.

For projects targeting versions prior to .NET 10, the `System.Linq.AsyncEnumerable` NuGet package is required for these materialization methods.
