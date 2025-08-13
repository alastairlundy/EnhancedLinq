# ExtraLinq's Extension Methods

Method signatures shown below exclude the ``this`` static member parameter of the extension method but show all other parameters and the return type.

## Deferred Execution


## Immediate Execution


### Dictionary

#### Add Range :
* ``void AddRange<TKey, TValue>(IEnumerable<KeyValuePair<TKey, TValue> values)``: Adds a sequence of KeyValuePair items to a Dictionary.
* ``void AddRange<TKey, TValue>(IDictionary<TKey, TValue> dictionaryToAdd)``: Adds items in one dictionary to a dictionary. 
