# EnhancedLinq.Memory Methods

Immediate Execution extensions perform the relevant operation(s) immediately. Examples of LINQ's built in immediate mode extensions are ``IndexOf``, ``Any``, ``All``, and ``Count``.

## API Availability Comparison:

### Immediate Mode 
| Method Name | Span support | ReadOnlySpan support | Memory support | ReadOnlyMemory support |
|-|-|-|-|-|
| CountAtLeast(int) | :heavy_check_mark: | :heavy_check_mark: | :heavy_check_mark:, as of 1.0.0 Alpha 3 | :heavy_check_mark:, as of 1.0.0 Alpha 3 |
| CountAtLeast(Func<T, bool>, int) | :heavy_check_mark: | :heavy_check_mark: | :heavy_check_mark:, as of 1.0.0 Alpha 3 | :heavy_check_mark:, as of 1.0.0 Alpha 3 |
| CountAtMost(int) | :heavy_check_mark: | :heavy_check_mark: | :heavy_check_mark:, as of 1.0.0 Alpha 3 | :heavy_check_mark:, as of 1.0.0 Alpha 3 |
| CountAtMost(Func<T, bool>, int) | :heavy_check_mark: | :heavy_check_mark: | :heavy_check_mark:, as of 1.0.0 Alpha 3 | :heavy_check_mark:, as of 1.0.0 Alpha 3 |

## Immediate Mode

### Math Operations
**Average**
Calculates the arithmetic average of all TNumbers in a Span of TNumbers.

**Sum**
Calculates the sum of all TNumbers in a Span of TNumbers.

### LINQ style Operations

**All**
Returns true if all elements within a Span match a predicate condition. Returns false otherwise.

**Any**
Return true if any element within a Span matches a predicate condition. Returns false otherwise.

**Count**
Counts the number of elements within a Span that match a predicate condition.

This method has 1 overload.

**Distinct**
Creates a new Span with distinct elements from the provided Span.

This method has 1 overload.

**Except**
Creates a new Span with all elements that are in one Span (out of two) that aren't in the other Span.

**First**
Retrieves the first element in a Span that matches a predicate condition. Throws an exception if none match.

**FirstOrDefault**
Retrieves the first element in a Span that matches a predicate condition. Returns default if none match.

**ForEach**
Applies a specified action to each element within a Span.

This method has 1 overload.

**GroupBy**
Groups elements of a Span by a specified Key.

**IndicesOf**
Gets the indices of all elements that match the target value or predicate.

This method has 1 overload.

**Last**
Retrieves the last element in a Span that matches a predicate condition. Throws an exception if none match.

**LastOrDefault**
Retrieves the last element in a Span that matches a predicate condition. Returns default if none match.

**Minimum**
Retrieves the lowest number in the span of TNumbers. 

**Maximum**
Retrieves the highest number in the span of TNumbers.

**Select**
Transforms elements of a Span according to behaviour defined by the selector.

**Skip**
Returns a Span that skips the first specified number of elements.

**SkipLast**
Returns a Span that skips the last specified number of elements.

**SkipWhile**
Skips elements of a Span whilst the eleemnts meet a predicate condition.

**Where**
Retrieves all elements of a Span that meet the predicate condition.
