# EnhancedLinq Methods

Deferred Execution extensions perform the relevant operation(s) upon the relevant enumerable being enumerated. Examples of LINQ's built in deferred execution extensions are ``Select``, ``GroupBy``, and ``Where``.

Immediate Execution extensions perform the relevant operation(s) immediately. Examples of LINQ's built in immediate mode extensions are ``IndexOf``, ``Any``, ``All``, and ``Count``.

## Deferred Execution

**AppendRange**
Appends a sequence to the end of another sequence.

**RemoveRange**
Removes every element within a sequence of elements from a sequence of elements.

**ElementsAt**
Gets the elements in a source sequence at the corresponding sequence of indices.

**IndicesOf**
Gets the indices of all elements that match the target value or predicate. 

This method has 3 overloads.

**GenerateNumberRange**
Generates a range of numbers of type TNumber where TNumber implements ``INumber<TNumber>`` starting from the specified start value and ending at the specified end value.

**SplitByCount**
Splits a sequence into a sequence of sequences based on the maximum number of elements to include in a sequence.

**SplitByProcessorCount**
Splits a sequence into a sequence of sequences based on the number of Logical Processors a CPU/SOC has.

### KeyValuePairs

**ToKeys**
Selects the Keys from a sequence of ``KeyValuePair<TKey, TValue>``.

**ToValues**
Selects the Values from a sequence of ``KeyValuePair<TKey, TValue>``.

## Immediate Execution
**ContainsDuplicates**
Determines whether a sequence contains duplicate instances of an object.

**ElementAt**
Retrieves the element at the specified index from a sequence.

**IndexOf**
Retrieves the index of an element or a specified predicate within the sequence.

This method has 1 overload.

**ElementsAt**
Gets the elements in a source sequence at the corresponding list of indices.

This method has 1 overload.

**Distinct**
Creates a new list with distinct elements from the source list.

This method has 3 overloads.

**GenerateNumberRangeAsArray**
Generates an array of TNumber values starting from a specified value and continuing for a specified count.

**GenerateNumberRangeAsList**
Generates a list of TNumber values starting from a specified value and continuing for a specified count.


**GetRange**
Returns a list of elements from the list at the specified start index up to a distance of 'count' elements.

This method has 1 overload.

**InsertRange**
Inserts a sequence of elements starting from the specified index.

**Replace**
Replaces all occurences of an old value within a list with a new value.

**Reverse**
Reverses a list and returns it.

This method has 1 overload.

### Concurrent

**GetRange**
Retrieves a specified number of elements from a concurrent collections starting from a start index.

This method has 1 overload.

**Remove**
Removes all occurrences of a specified object from within a ``ConcurrentBag<T>`` and returns the new ``ConcurrentBag<T>``.

**RemoveRange**
Removes a sequence of elements from a concurrent collection.

This method has 3 overloads.

### Dictionary

**GetKeyByValue**
Gets the Key associated with the specified value from an IDictionary.
