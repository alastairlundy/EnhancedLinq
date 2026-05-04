# EnhancedLinq.Async
This library provides asynchronous versions of the EnhancedLinq extensions, targeting `IAsyncEnumerable<T>` for deferred and immediate enumeration modes.

EnhancedLinq.Async requires consuming projects to use C# language version 14 or newer for EnhancedLinq to work. 

## 🚀 Included Methods
For a comprehensive list of included methods, check out the following resources:

- [EnhancedLinqAsync Methods](../../docs/Methods/EnhancedLinq.Async.md) [![NuGet](https://img.shields.io/nuget/v/EnhancedLinq.Async.svg)](https://www.nuget.org/packages/EnhancedLinq.Async/)  [![NuGet](https://img.shields.io/nuget/dt/EnhancedLinq.Async.svg)](https://www.nuget.org/packages/EnhancedLinq.Async/)

## 📦 NuGet Packages

The **EnhancedLinq.Async** package provides `IAsyncEnumerable` supporting extensions for EnhancedLinq.

### 🛠️ Installing EnhancedLinq.Async

Getting started with **EnhancedLinq.Async** is easy! You can install the package using the .NET SDK CLI, your IDE's package manager, or directly from the NuGet website.

| Package Id         | NuGet Link                                                                | .NET SDK CLI Command                    |
|--------------------|---------------------------------------------------------------------------|-----------------------------------------|
| EnhancedLinq.Async | [EnhancedLinq.Async NuGet](https://nuget.org/packages/EnhancedLinq.Async) | `dotnet add package EnhancedLinq.Async` |

## 📖 Usage
Here are some examples demonstrating how to use some methods provided by EnhancedLinq.Async.

### Deferred Execution Examples

**ElementsAt**
This example shows how to find the elements at a given sequence of indices asynchronously.

```csharp
        async IAsyncEnumerable<string> GetFruitsAsync()
        {
            yield return "Apple";
            yield return "Banana";
            yield return "Cherry";
            yield return "Date";
            yield return "Elderberry";
        }

        IAsyncEnumerable<string> fruits = GetFruitsAsync();
        int[] indices = [1, 3]; // We want to retrieve "Banana" and "Date"
        
        IAsyncEnumerable<string> selectedFruits = fruits.ElementsAt(indices);

        Console.WriteLine("Selected Fruits:");
        await foreach (string fruit in selectedFruits)
        {
            Console.WriteLine(fruit); // Outputs: Banana, Date
        }
```

### Immediate Enumeration Mode Examples

**ContainsDuplicates**
This example shows how to check asynchronously if an `IAsyncEnumerable<T>` contains any duplicate elements.

```csharp
        async IAsyncEnumerable<string> GetFruitsAsync()
        {
            yield return "Apple";
            yield return "Banana";
            yield return "Cherry";
            yield return "Apple"; // Contains a duplicate
        }

        IAsyncEnumerable<string> fruits = GetFruitsAsync();
        bool hasDuplicates = await fruits.ContainsDuplicates();

        Console.WriteLine($"Does the list contain duplicates? {hasDuplicates}"); // Output: True
```

## 🏗️ Building

To build **EnhancedLinq.Async** from source, follow these steps:

1. Clone the repository.
2. Open the solution in your preferred IDE or Code Editor.
3. Build the desired project to restore dependencies and compile the project.

## 🤝 Contributing
I welcome contributions. If you have ideas for new features, improvements, or bug fixes, please check out the [contributing guidelines](../../CONTRIBUTING.md) for more information.

## 📜 License

**EnhancedLinq.Async** is licensed under the **MPL 2.0 license**. Feel free to use and modify EnhancedLinq according to the terms of the license.

## ❓ Questions?

If you have any questions or experience any issues, please open a discussion in the [repository's GitHub issues page](https://github.com/alastairlundy/enhancedlinq/discussions).

## 🔄 Alternatives

While **EnhancedLinq.Async** is a powerful tool, you may wish to explore these alternatives:

- [SuperLinq](https://github.com/viceroypenguin/SuperLinq)
- [MoreLinq](https://github.com/morelinq/MoreLINQ)
