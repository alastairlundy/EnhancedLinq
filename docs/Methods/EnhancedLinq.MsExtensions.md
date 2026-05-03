# EnhancedLinq.MsExtensions Methods

Deferred Execution extensions perform the relevant operation(s) upon the relevant enumerable being enumerated. Examples of LINQ's built in deferred execution extensions are ``Select``, ``GroupBy``, and ``Where``.

Immediate Execution extensions perform the relevant operation(s) immediately. Examples of LINQ's built in immediate mode extensions are ``IndexOf``, ``Any``, ``All``, and ``Count``.

## Deferred Mode

| Name | Description | Overloads | Notes |
| :--- | :--- | :--- | :--- |
| AsEnumerable | Enumerates the specified StringSegment. | | |
| GroupBy | Groups the characters in the specified StringSegment according to a specified key predicate function. | | |
| IndicesOf | Finds all occurrences of a specified char or StringSegment within a StringSegment. | 2 | |
| SplitBy | Splits the given StringSegment into segments separated by the specified character, StringSegment, or predicate. | 3 | Functions similarly to `Split` but executes in Deferred mode and returns an `IEnumerable`. |
| Where | Returns an IEnumerable of chars that match the predicate. | | |

Multiple enumeration of `IEnumerable<<TT>` is computationally expensive and should be avoided. If multiple uses of an enumerable is required, convert to an `IList<<TT>` type such as `T[]` with .NET LINQ's ``ToArray()`` or `List<<TT>` with ``ToList()`` methods.

## Immediate Mode

| Name | Description | Overloads | Notes |
| :--- | :--- | :--- | :--- |
| All | Returns whether all chars in a StringSegment match the predicate condition. | | |
| Any | Returns whether a StringSegment has any chars or matches a predicate condition. | 2 | |
| Count | Counts the number of chars in the StringSegment that match the predicate. | | |
| CountAtLeast | Determines whether there are at least a specified number of elements in the StringSegment or meet a given condition. | 2 | |
| CountAtMost | Determines whether there is at most a maximum number of elements in the StringSegment or satisfy a given condition. | 2 | |
| First | Retrieves the first char in the StringSegment or the first char that matches a predicate condition. | 2 | |
| FirstOrDefault | Retrieves the first char in the StringSegment or the first char that matches a predicate condition, returning null if none match. | 2 | |
| Last | Retrieves the last char in the StringSegment or the last char that matches a predicate condition. | 2 | |
| LastOrDefault | Retrieves the last char in the StringSegment or the last char that matches a predicate condition, returning null if none match. | 2 | |
| ForEach | Applies a specified action or transformation function to each char in the StringSegment. | 2 | |
| HasNoMatches | Determines if none of the characters in the StringSegment match a predicate condition. | | |
| IndexOf | Finds the index of a specified StringSegment within another StringSegment or string. | 2 | |
| IndicesOf | Returns the indices of all occurrences of a specified character or StringSegment within the StringSegment. | 2 | |
| Split | Splits a StringSegment into StringSegment subsegments using a specified char or StringSegment separator. | 2 | Functions similarly to `SplitBy` but executes in Immediate mode and returns an array/list. |
