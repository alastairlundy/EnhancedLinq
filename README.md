# EnhancedLinq
This library adds Additional LINQ style Deferred execution and Immediate enumeration mode extension methods for .NET .



## 🚀 Included Methods
For a comprehensive list of included methods, check out the following resources:

- [EnhancedLinq Methods](./docs/Methods/EnhancedLinq.md) [![NuGet](https://img.shields.io/nuget/v/AlastairLundy.EnhancedLinq.svg)](https://www.nuget.org/packages/AlastairLundy.EnhancedLinq/)  [![NuGet](https://img.shields.io/nuget/dt/AlastairLundy.EnhancedLinq.svg)](https://www.nuget.org/packages/AlastairLundy.EnhancedLinq/)
- [EnhancedLinq.Memory Methods](./docs/Methods/EnhancedLinq.Memory.md) [![NuGet](https://img.shields.io/nuget/v/AlastairLundy.EnhancedLinq.Memory.svg)](https://www.nuget.org/packages/AlastairLundy.EnhancedLinq.Memory/)  [![NuGet](https://img.shields.io/nuget/dt/AlastairLundy.EnhancedLinq.Memory.svg)](https://www.nuget.org/packages/AlastairLundy.EnhancedLinq.Memory/)
- [EnhancedLinq.MsExtensions Methods](./docs/Methods/EnhancedLinq.MsExtensions.md) [![NuGet](https://img.shields.io/nuget/v/AlastairLundy.EnhancedLinq.MsExtensions.svg)](https://www.nuget.org/packages/AlastairLundy.EnhancedLinq.MsExtensions/)  [![NuGet](https://img.shields.io/nuget/dt/AlastairLundy.EnhancedLinq.MsExtensions.svg)](https://www.nuget.org/packages/AlastairLundy.EnhancedLinq.MsExtensions/)

## 📦 NuGet Packages

**EnhancedLinq** comes with several packages tailored to your needs:

- **`EnhancedLinq`**: The core package that enhances your LINQ experience.
- **`EnhancedLinq.Memory`**: This package is specifically for `Span<T>` and `Memory<T>`, providing helpful immediate mode extensions.
- **`EnhancedLinq.MsExtensions`**: Focused on `StringSegment` from `Microsoft.Extensions.Primitives`, with plans to potentially expand support for other Microsoft.Extensions packages.

### 🛠️ Installing EnhancedLinq

Getting started with **EnhancedLinq** is easy! You can install the packages using the .NET SDK CLI, your IDE's package manager, or directly from the NuGet website.

| Package Id                              | NuGet Link | .NET SDK CLI Command |
|-----------------------------------------|-------------|----------------------|
| AlastairLundy.EnhancedLinq.MsExtensions | [EnhancedLinq NuGet](https://nuget.org/packages/AlastairLundy.EnhancedLinq) | `dotnet add package AlastairLundy.EnhancedLinq.` |
| AlastairLundy.EnhancedLinq.EnhancedLinq.Memory                     | [EnhancedLinq.Memory NuGet](https://nuget.org/packages/AlastairLundy.EnhancedLinq.Memory) | `dotnet add package AlastairLundy.EnhancedLinq.Memory` |
| AlastairLundy.EnhancedLinq.EnhancedLinq.MsExtensions               | [EnhancedLinq.MsExtensions NuGet](https://nuget.org/packages/AlastairLundy.EnhancedLinq.MsExtensions) | `dotnet add package AlastairLundy.EnhancedLinq.MsExtensions` |

## 📖 Usage
Here are some examples demonstrating how to use some methods provided by EnhancedLinq.

### Deferred Execution Examples

**ElementsAt**
This example shows how to find the elements at a given sequence of indices.

```csharp

        var fruits = ["Apple", "Banana", "Cherry", "Date", "Elderberry" ];
        var indices = [1, 3]; // We want to retrieve "Banana" and "Date"
        
        IEnumerable<string> selectedFruits = fruits.ElementsAt(indices);

        Console.WriteLine("Selected Fruits:");
        foreach (string fruit in selectedFruits)
        {
            Console.WriteLine(fruit); // Outputs: Banana, Date
        }
```

**IndicesOf**
This example shows how to find all the indices of a specific element in an IEnumerable<T>.
```csharp

        var numbers = [ 1, 2, 3, 2, 4, 2, 5 ];
        int target = 2; // We want to find the indices of the number 2

        IEnumerable<int> indices = numbers.IndicesOf(target);

        Console.WriteLine("Indices of target element:");
        foreach (var index in indices)
        {
            Console.WriteLine(index); // Outputs: 1, 3, 5
        }
```

### Immediate Enumeration Mode Examples

**ContainsDuplicates**
This example shows how to check if an IEnumerable<T> contains any duplicate elements.

```csharp
        var fruits = ["Apple", "Banana", "Cherry", "Apple" ]; // Contains a duplicate

        bool hasDuplicates = fruits.ContainsDuplicates();

        Console.WriteLine($"Does the list contain duplicates? {hasDuplicates}"); // Output: True
```

**IndexOf**
This example shows how to find the index of the first element that matches a predicate in an IEnumerable<T>.
```csharp
        var numbers = new List<int> { 10, 20, 30, 40, 50 };

        // Define a predicate to find the first number greater than 25
        Func<int, bool> predicate = n => n > 25;

        int index = numbers.IndexOf(predicate);

        Console.WriteLine($"Zero based index of the first element greater than 25: {index}"); // Output: 2
```

## 🏗️ Building

To build **EnhancedLinq** from source, follow these steps:

1. Clone the repository.
2. Open the solution in your preferred IDE or Code Editor.
3. Build the desired project to restore dependencies and compile the project.

## 🤝 Contributing
I welcome contributions. If you have ideas for new features, improvements, or bug fixes, please check out the [contributing guidelines](./CONTRIBUTING.md) for more information.

## 📜 License

**EnhancedLinq** is licensed under the **Mozilla Public License 2.0**. Feel free to use and modify EnhancedLinq according to the terms of the license.

## ❓ Questions?

If you have any questions or experience any issues, please open an issue in the [repository's GitHub issues page](https://github.com/alastairlundy/enhancedlinq/issues).

## 🔄 Alternatives

While **EnhancedLinq** is a powerful tool, you may wish to explore these alternatives:

- [SuperLinq](https://github.com/viceroypenguin/SuperLinq)
- [MoreLinq](https://github.com/morelinq/MoreLINQ)
