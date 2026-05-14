using System.Linq;

namespace ExtendedLinq.Tests.Immediate;

public class ImmediateDuplicatesTests
{
    private readonly Faker _faker = new();

    [Test]
    public async Task ContainsDuplicates_WithNullSource_Throws()
    {
        IEnumerable<int>? source = null;
        
        await Assert.ThrowsAsync<ArgumentNullException>(() => 
#pragma warning disable CS8604 // Possible null reference argument.
            Task.FromResult(source.ContainsDuplicates()));
#pragma warning restore CS8604 // Possible null reference argument.
    }

    [Test]
    public async Task ContainsDuplicates_WithNullSourceAndComparer_Throws()
    {
        IEnumerable<int>? source = null;
        
        await Assert.ThrowsAsync<ArgumentNullException>(() => 
#pragma warning disable CS8604 // Possible null reference argument.
            Task.FromResult(source.ContainsDuplicates(EqualityComparer<int>.Default)));
#pragma warning restore CS8604 // Possible null reference argument.
    }

    [Test]
    public async Task ContainsDuplicates_NoDuplicates_ReturnsFalse()
    {
        IEnumerable<int> source = [1, 2, 3, 4, 5];
        
        bool actual = source.ContainsDuplicates();
        
        await Assert.That(actual).IsFalse();
    }

    [Test]
    public async Task ContainsDuplicates_HasDuplicates_ReturnsTrue()
    {
        IEnumerable<int> source = [1, 2, 3, 2, 5];
        
        bool actual = source.ContainsDuplicates();
        
        await Assert.That(actual).IsTrue();
    }

    [Test]
    public async Task ContainsDuplicates_MultipleDuplicates_ReturnsTrue()
    {
        IEnumerable<int> source = [1, 2, 3, 2, 5, 3, 7];
        
        bool actual = source.ContainsDuplicates();
        
        await Assert.That(actual).IsTrue();
    }

    [Test]
    public async Task ContainsDuplicates_AllSameElements_ReturnsTrue()
    {
        int randomNumber = _faker.Random.Int(1, 100);
        IEnumerable<int> source = _faker.MakeLazy(Random.Shared.Next(1, 10), _ => randomNumber);
        
        bool actual = source.ContainsDuplicates();
        
        await Assert.That(actual).IsTrue();
    }

    [Test]
    public async Task ContainsDuplicates_EmptySequence_ReturnsFalse()
    {
        IEnumerable<int> source = Enumerable.Empty<int>();
        
        bool actual = source.ContainsDuplicates();
        
        await Assert.That(actual).IsFalse();
    }

    [Test]
    public async Task ContainsDuplicates_SingleElement_ReturnsFalse()
    {
        IEnumerable<int> source = [_faker.Random.Int(1, 1000)];
        
        bool actual = source.ContainsDuplicates();
        
        await Assert.That(actual).IsFalse();
    }

    [Test]
    public async Task ContainsDuplicates_WithStringComparer_IgnoreCase()
    {
        IEnumerable<string> source = ["apple", "Banana", "APPLE", "Cherry"];
        
        bool actual = source.ContainsDuplicates(StringComparer.OrdinalIgnoreCase);
        
        await Assert.That(actual).IsTrue(); // "apple" and "APPLE" are duplicates ignoring case
    }

    [Test]
    public async Task ContainsDuplicates_WithStringComparer_CaseSensitive()
    {
        IEnumerable<string> source = ["apple", "Banana", "APPLE", "Cherry"];
        
        bool actual = source.ContainsDuplicates(StringComparer.Ordinal);
        
        await Assert.That(actual).IsFalse(); // No exact duplicates when case-sensitive
    }
}