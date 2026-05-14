using System.Linq;

namespace ExtendedLinq.Tests.Immediate;

public class ImmediateIndexTests
{
    private readonly Faker _faker = new();

    [Test]
    public async Task IndexOf_WithNullSource_Throws()
    {
        IEnumerable<int>? source = null;
        
        await Assert.ThrowsAsync<ArgumentNullException>(() => 
#pragma warning disable CS8604 // Possible null reference argument.
            Task.FromResult(source.IndexOf(5)));
#pragma warning restore CS8604 // Possible null reference argument.
    }

    [Test]
    public async Task IndexOf_ItemNotFound_ReturnsNegativeOne()
    {
        IEnumerable<int> source = _faker.Make(Random.Shared.Next(2, 10), () => Random.Shared.Next(1, 5));
        
        int actual = source.IndexOf(10);
        
        await Assert.That(actual).IsEqualTo(-1);
    }

    [Test]
    public async Task IndexOf_ItemFoundAtBeginning_ReturnsZero()
    {
        IEnumerable<int> source = [5, 2, 3, 4, 1];
        
        int actual = source.IndexOf(5);
        
        await Assert.That(actual).IsEqualTo(0);
    }

    [Test]
    public async Task IndexOf_ItemFoundAtMiddle_ReturnsCorrectIndex()
    {
        IEnumerable<int> source = [1, 2, 5, 4, 3];
        
        int actual = source.IndexOf(5);
        
        await Assert.That(actual).IsEqualTo(2);
    }

    [Test]
    public async Task IndexOf_ItemFoundAtEnd_ReturnsLastIndex()
    {
        IEnumerable<int> source = [1, 2, 3, 4, 5];
        
        int actual = source.IndexOf(5);
        
        await Assert.That(actual).IsEqualTo(4);
    }

    [Test]
    public async Task IndexOf_WithDuplicateItems_ReturnsFirstOccurrence()
    {
        IEnumerable<int> source = [1, 5, 3, 5, 2];
        
        int actual = source.IndexOf(5);
        
        await Assert.That(actual).IsEqualTo(1); // First occurrence at index 1
    }

    [Test]
    public async Task IndexOf_WithEmptySequence_ReturnsNegativeOne()
    {
        IEnumerable<int> source = Enumerable.Empty<int>();
        
        int actual = source.IndexOf(Random.Shared.Next(1, 100));
        
        await Assert.That(actual).IsEqualTo(-1);
    }

    [Test]
    public async Task IndexOf_WithStringValues_ReturnsCorrectIndex()
    {
        IEnumerable<string> source = ["apple", "banana", "cherry", "date"];
        
        int actual = source.IndexOf("cherry");
        
        await Assert.That(actual).IsEqualTo(2);
    }

    [Test]
    public async Task IndexOf_WithCaseSensitiveStrings_ReturnsCorrectIndex()
    {
        IEnumerable<string> source = ["Apple", "banana", "Cherry"];
        
        int actual = source.IndexOf("cherry"); // lowercase 'c'
        
        await Assert.That(actual).IsEqualTo(-1); // Not found due to case sensitivity
    }

    [Test]
    public async Task IndexOf_WithNullItemInStringList_ReturnsCorrectIndex()
    {
        IEnumerable<string?> source = ["apple", null, "cherry"];
        
#pragma warning disable CS8714 // The type cannot be used as type parameter in the generic type or method. Nullability of type argument doesn't match 'notnull' constraint.
        int actual = source.IndexOf((string?)null);
#pragma warning restore CS8714 // The type cannot be used as type parameter in the generic type or method. Nullability of type argument doesn't match 'notnull' constraint.
        
        await Assert.That(actual).IsEqualTo(1);
    }
}