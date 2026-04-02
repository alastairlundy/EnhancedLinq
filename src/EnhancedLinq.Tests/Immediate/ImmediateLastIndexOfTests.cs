using System.Collections.Generic;
using System.Linq;

namespace ExtendedLinq.Tests.Immediate;

public class ImmediateLastIndexOfTests
{
    private readonly Faker _faker = new();

    [Test]
    public async Task LastIndexOf_WithNullSource_Throws()
    {
        IEnumerable<int>? source = null;
        
        await Assert.ThrowsAsync<ArgumentNullException>(() => 
            Task.FromResult(source!.LastIndexOf(5)));
    }

    [Test]
    public async Task LastIndexOf_ItemNotFound_ReturnsNegativeOne()
    {
        IEnumerable<int> source = _faker.Make(Random.Shared.Next(2, 10), () => Random.Shared.Next(1, 5));
        
        int actual = source.LastIndexOf(10);
        
        await Assert.That(actual).IsEqualTo(-1);
    }

    [Test]
    public async Task LastIndexOf_ItemFoundAtBeginning_ReturnsZero()
    {
        IEnumerable<int> source = [5, 2, 3, 4, 1];
        
        int actual = source.LastIndexOf(5);
        
        await Assert.That(actual).IsEqualTo(0);
    }

    [Test]
    public async Task LastIndexOf_ItemFoundAtEnd_ReturnsLastIndex()
    {
        IEnumerable<int> source = [1, 2, 3, 4, 5];
        
        int actual = source.LastIndexOf(5);
        
        await Assert.That(actual).IsEqualTo(4);
    }

    [Test]
    public async Task LastIndexOf_ItemFoundAtMiddle_ReturnsCorrectIndex()
    {
        IEnumerable<int> source = [1, 2, 5, 4, 3];
        
        int actual = source.LastIndexOf(5);
        
        await Assert.That(actual).IsEqualTo(2);
    }

    [Test]
    public async Task LastIndexOf_WithDuplicateItems_ReturnsLastOccurrence()
    {
        IEnumerable<int> source = [1, 5, 3, 5, 2];
        
        int actual = source.LastIndexOf(5);
        
        await Assert.That(actual).IsEqualTo(3); // Last occurrence at index 3
    }

    [Test]
    public async Task LastIndexOf_WithEmptySequence_ReturnsNegativeOne()
    {
        IEnumerable<int> source = Enumerable.Empty<int>();
        
        int actual = source.LastIndexOf(5);
        
        await Assert.That(actual).IsEqualTo(-1);
    }

    [Test]
    public async Task LastIndexOf_WithStringValues_ReturnsCorrectIndex()
    {
        IEnumerable<string> source = ["apple", "banana", "cherry", "banana"];
        
        int actual = source.LastIndexOf("banana");
        
        await Assert.That(actual).IsEqualTo(3); // Last occurrence of "banana"
    }
}