# EnhancedLinq.Memory Methods

Immediate Execution extensions perform the relevant operation(s) immediately. Examples of LINQ's built in immediate mode extensions are ``IndexOf``, ``Any``, ``All``, and ``Count``.

## Deferred Execution
These methods provide deferred execution by converting memory segments into enumerables.

| Name | Description | Overloads | Notes |
| :--- | :--- | :--- | :--- |
| **AsEnumerable** | Converts a `Memory<T>` or `ReadOnlyMemory<T>` to an `IEnumerable<T>`. | 1 | |
| **Where** | Filters elements from the specified memory segment based on a specified predicate. | 1 | |
| **Index** | Returns a sequence of indices of all elements within the given memory segment, starting from an initial index of zero. | 1 | |
| **Select** | Projects each element of a memory-based enumerable into a new form. | 1 | |
| **OrderBy** | Sorts the elements of a sequence in ascending order according to a specified key. | 2 | |
| **OrderByDescending** | Sorts the elements of a sequence in descending order according to a specified key. | 2 | |

> [!WARNING]
> Multiple enumerations of `IEnumerable<T>` can be computationally expensive. Consider materializing the result using `.ToList()` or `.ToArray()` if the sequence needs to be iterated multiple times.

## Immediate Mode

### Immediate Mode Extensions
General utility extensions for immediate memory operations.

| Name | Description | Overloads | Notes |
| :--- | :--- | :--- | :--- |
| **CountAtLeast** | Determines whether there are at least a specified number of elements in the memory segment (optionally matching a predicate). | 2 | |
| **CountAtMost** | Determines whether there are at most a specified number of elements in the memory segment (optionally matching a predicate). | 2 | |
| **HasNoMatches** | Determines if none of the elements in a memory segment match a predicate condition. | 1 | |
| **Exclude** | Returns a new span containing elements that do not satisfy the given predicate or are not in the provided span. | 2 | |
| **SplitBy** | Splits a span into a list of arrays based on a separator or a predicate. | 2 | |
| **SplitByItemCount** | Splits a span into a list of arrays based on the specified item count per array. | 1 | Throws `ArgumentOutOfRangeException` if count $\le 0$. |
| **SplitByProcessorCount** | Splits a span into a list of arrays based on the number of processors available. | 1 | |
| **SplitByArrayCount** | Splits a span into a list of arrays based on a specified maximum number of arrays. | 1 | Throws `ArgumentOutOfRangeException` if count $\le 0$. |
| **ForEach** | Applies a specified action to each element within a span. | 1 | |
| **LastIndex** | Retrieves the index of the last occurrence of a value or element matching a predicate. | 2 | |

### LINQ Style Extensions
Immediate mode extensions providing LINQ-like functionality for memory segments. Most methods do not throw if the span/memory is empty, except for `ForEach` and the non-predicate overloads of `First` and `Last`.

| Name | Description | Overloads | Notes |
| :--- | :--- | :--- | :--- |
| **All** | Returns whether all items in a span match the predicate condition. | 1 | |
| **Any** | Returns true if any element within a span matches a predicate condition. | 1 | |
| **Count** | Counts the number of elements within a span that match a predicate condition. | 2 | |
| **Distinct** | Returns a new span containing distinct elements from the source span. | 2 | |
| **Except** | Returns a new span containing elements present in one span but not the other. | 1 | |
| **First** | Retrieves the first element that matches a predicate or simply the first element. | 2 | Non-predicate overload throws `InvalidOperationException` if empty. Predicate overload throws `ArgumentException` if no match. |
| **FirstOrDefault** | Retrieves the first element that matches a predicate or returns default if none match/empty. | 2 | |
| **Last** | Retrieves the last element that matches a predicate or simply the last element. | 2 | Non-predicate overload throws `InvalidOperationException` if empty. Predicate overload throws `ArgumentException` if no match. |
| **LastOrDefault** | Retrieves the last element that matches a predicate or returns default if none match/empty. | 2 | |
| **Select** | Transforms elements of a span according to a selector function. | 1 | |
| **SelectMany** | Projects each element into a collection and flattens the result into a single array. | 2 | Throws `OverflowException` if results exceed `int.MaxValue`. |
| **Skip** | Returns a new span excluding the specified first number of elements. | 1 | Throws `ArgumentOutOfRangeException` if count is too large. |
| **SkipLast** | Returns a new span excluding the specified last number of elements. | 1 | Throws  `ArgumentOutOfRangeException` if count is too large. |
| **SkipWhile** | Returns a new span starting from the first element for which the predicate returns false. | 1 | |
| **Where** | Retrieves all elements of a span that meet the predicate condition. | 1 | |
| **GroupBy** | Groups elements of a span by a specified key. | 1 | |
| **IndicesOf** | Gets a collection of indices of all elements that match the target value or predicate. | 2 | |
| **ForEach** | Applies a specified action or transformation function to each element within a span or memory. | 2 | Throws `InvalidOperationException` if empty. |

### Range Extensions
Extensions for manipulating and retrieving ranges within memory segments.

| Name | Description | Overloads | Notes |
| :--- | :--- | :--- | :--- |
| **GetRange** | Retrieves a specific range of elements (via `Range`, indices, or start/end) as a new span. | 3 | |
| **InsertRange** | Inserts a collection of elements at the specified start index into the span. | 1 | |
| **RemoveRange** | Creates a new span excluding items at the specified indices or range. | 3 | |

### Maths Extensions
Numeric operations constrained to `INumber<TNumber>` (.NET 8.0+).

| Name | Description | Overloads | Notes |
| :--- | :--- | :--- | :--- |
| **Average** | Calculates the arithmetic average of all numbers in a span. | 1 | |
| **Sum** | Calculates the sum of all numbers in a span. | 1 |  |
| **Minimum** | Retrieves the lowest number in the span. | 1 | |
| **Maximum** | Retrieves the highest number in the span. | 1 | |
