# ExtraLinq.Memory
A set of Immediate Mode LINQ lambda-style extensions specifically designed for working with Spans and System.Memory data structures in .NET. 

## License

This project is licensed under the **MPL 2.0** License. See the [LICENSE](../../LICENSE.txt) file for more details.

## Features

- **Immediate Mode LINQ Extensions**: Most extensions in this package operate in immediate mode, meaning they execute as soon as they are called, making them suitable for use with `Span<T>`.

## Installation

You can install the **ExtraLinq.Memory** package via NuGet Package Manager with the following command:

```bash
Install-Package ExtraLinq.Memory
```

Alternatively, you can add it to your project file:

```xml
<PackageReference Include="ExtraLinq.Memory" Version="1.0.0" />
```

## Usage
Here are some examples of how to use the extensions provided by **ExtraLinq.Memory**:

### Example 1: Using Immediate LINQ Extensions

```csharp
using ExtraLinq.Memory;

Span<int> numbers = new int[] { 1, 2, 3, 4, 5 };

// Using the Sum extension
int sum = numbers.Sum(); // Returns 15

// Using the Where extension
var evenNumbers = numbers.Where(n => n % 2 == 0); // Returns { 2, 4 }
```

### Example 2: Working with Memory<T>

```csharp
using ExtraLinq.Memory;

Memory<int> memoryNumbers = new Memory<int>(new int[] { 1, 2, 3, 4, 5 });

// Using the Average extension
double average = memoryNumbers.Average(); // Returns 3.0
```

## Contributing

Contributions are welcome! If you would like to contribute to **ExtraLinq.Memory**, please follow the [contributing guide](../../CONTRIBUTING.md)

## Support

For any issues or feature requests, please open an issue in the GitHub repository.