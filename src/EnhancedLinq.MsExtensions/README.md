# EnhancedLinq.MsExtensions
A set of LINQ lambda-style extension methods specifically designed for working with `StringSegment` in .NET. 

This library enhances the usability of `StringSegment` by providing deferred execution and immediate mode LINQ style extensions.

## License

This project is licensed under the **Apache 2.0 license**. See the [LICENSE](../../LICENSE.txt) file for more details.

## Features

- **Deferred Execution Extensions**: These extensions return a sequence that is enumerated upon usage of the returned sequence.
- **Immediate Mode Extensions**: These extensions operate in immediate mode, executing as soon as they are called.

## Installation

You can install the **EnhancedLinq.MsExtensions** package via NuGet Package Manager with the following command:

```bash
Install-Package AlastairLundy.EnhancedLinq.MsExtensions
```

Alternatively, you can add it to your project file:

```xml
<PackageReference Include="AlastairLundy.EnhancedLinq.MsExtensions" Version="0.1.0" />
```

## Usage
Here are some examples of how to use the extensions provided by **EnhancedLinq.MsExtensions**:

### Example 1: Using Immediate Mode Extensions

```csharp
using AlastairLundy.EnhancedLinq.MsExtensions.StringSegments.Immediate;
using Microsoft.Extensions.Primitives;

StringSegment segment = new StringSegment("Hello, World!");

// Using the Count extension
int count = segment.Count(c => c == 'o'); // Returns 2
```


### Example 2: Using Deferred Execution Extensions

```csharp
using AlastairLundy.EnhancedLinq.MsExtensions.StringSegments.Deferred;
using Microsoft.Extensions.Primitives;

// Using the Where extension
IEnumerable<T> filteredChars = segment.Where(c => char.IsUpper(c)); // Returns { 'H', 'W' }
```

## Contributing

Contributions are welcome! If you would like to contribute to **EnhancedLinq.MsExtensions**, please follow the [contributing guide](../../CONTRIBUTING.md).

## Support

For any issues or feature requests, please open an issue in the GitHub repository.