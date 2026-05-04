# EnhancedLinq Methods

Deferred Execution extensions perform the relevant operation(s) upon the relevant enumerable being enumerated. Examples of LINQ's built in deferred execution extensions are ``Select``, ``GroupBy``, and ``Where``.

Immediate Execution extensions perform the relevant operation(s) immediately. Examples of LINQ's built in immediate mode extensions are ``IndexOf``, ``Any``, ``All``, and ``Count``.

## Deferred Execution

| Name | Description | Overloads | Notes |
| :--- | :--- | :--- | :--- |
| AppendRange | Appends a sequence to the end of another sequence. | | |
| Exclude | Excludes items in one sequence from another sequence, or those that match a predicate. | | |
| RemoveRange | Removes every element within a sequence of elements from a sequence of elements. | | |
| ElementsAt | Gets the elements in a source sequence at the corresponding sequence of indices. | | |
| IndicesOf | Gets the indices of all elements that match the target value or predicate. | 4 | |
| FirstNIndicesOf | Gets the first N indices of the specified item, predicate, char, or substring. | | |
| LastNIndicesOf | Gets the last N indices of the specified item, predicate, char, or substring. | | |
| GenerateNumberRange | Generates a range of numbers of type TNumber where TNumber implements ``INumber<<TNumberTNumber>`` starting from the specified start value and ending at the specified end value. | | |
| SplitByCount | Splits a sequence into a sequence of sequences based on the maximum number of elements to include in a sequence. | | |
| SplitByProcessorCount | Splits a sequence into a sequence of sequences based on the number of Logical Processors a CPU/SOC has. | | |

### KeyValuePairs

| Name | Description | Overloads | Notes |
| :--- | :--- | :--- | :--- |
| ToKeys | Selects the Keys from a sequence of ``KeyValuePair<<TTKey, TValue>``. | | |
| ToValues | Selects the Values from a sequence of ``KeyValuePair<<TTKey, TValue>``. | | |

## Immediate Execution

| Name | Description | Overloads | Notes |
| :--- | :--- | :--- | :--- |
| LastIndexOf | Gets the index of the last element that matches a predicate or object. | | |
| CountAtLeast | Determines if the sequence contains at least the specified number of elements matching a condition. | | |
| CountAtMost | Determines if the sequence contains at most the specified number of elements. | | |
| HasNoMatches | Determines whether a sequence contains no elements that match a predicate. | | |
| ForEach | Performs a specified action on each element of the sequence. | | |
| ContainsDuplicates | Determines whether a sequence contains duplicate instances of an object. | | |
| ElementAt | Retrieves the element at the specified index from a sequence. | | |
| IndexOf | Retrieves the index of an element or a specified predicate within the sequence. | 1 | |
| ElementsAt | Gets the elements in a source sequence at the corresponding list of indices. | 1 | |
| Distinct | Creates a new list with distinct elements from the source list. | 3 | |
| GenerateNumberRangeAsArray | Generates an array of TNumber values starting from a specified value and continuing for a specified count. | | |
| GenerateNumberRangeAsList | Generates a list of TNumber values starting from a specified value and continuing for a specified count. | | |
| GetRange | Returns a list of elements from the list at the specified start index up to a distance of 'count' elements. | 1 | |
| InsertRange | Inserts a sequence of elements starting from the specified index. | | |
| AddRange | Adds the elements of a collection to the end of the list. | | |
| Replace | Replaces all occurences of an old value within a list with a new value. | | |
| Reverse | Reverses a list and returns it. | 1 | |

### Concurrent

| Name | Description | Overloads | Notes |
| :--- | :--- | :--- | :--- |
| GetRange | Retrieves a specified number of elements from a concurrent collections starting from a start index. | 1 | |
| Remove | Removes all occurrences of a specified object from within a ``ConcurrentBag<<TT>`` and returns the new ``ConcurrentBag<<TT>``. | | |
| RemoveRange | Removes a sequence of elements from a concurrent collection. | 3 | |

### Dictionary

| Name | Description | Overloads | Notes |
| :--- | :--- | :--- | :--- |
| GetKeyByValue | Gets the Key associated with the specified value from an IDictionary. | | |
| TryAddRange | Attempts to add a range of elements to a dictionary. | | |
