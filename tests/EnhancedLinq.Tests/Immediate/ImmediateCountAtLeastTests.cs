using System.Linq;

namespace ExtendedLinq.Tests.Immediate;

public class ImmediateCountAtLeastTests
{
    [Test]
    public async Task CountAtLeast_WithNegativeCount_Throws()
    {
        IEnumerable<int> source = [1, 2, 3];
        
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => 
            Task.FromResult(source.CountAtLeast(-1)));
    }

    [Test]
    public async Task CountAtLeast_ZeroCount_ReturnsTrue()
    {
        IEnumerable<int> source = [1, 2, 3];
        
        bool actual = source.CountAtLeast(0);
        
        await Assert.That(actual).IsTrue();
    }

    [Test]
    public async Task CountAtLeast_EmptyEnumerable_ReturnsFalseForPositiveCount()
    {
        IEnumerable<int> source = Enumerable.Empty<int>();
        
        bool actual = source.CountAtLeast(1);
        
        await Assert.That(actual).IsFalse();
    }

    [Test]
    public async Task CountAtLeast_SufficientElements_ReturnsTrue()
    {
        IEnumerable<int> source = [1, 2, 3, 4, 5];
        
        bool actual = source.CountAtLeast(3);
        
        await Assert.That(actual).IsTrue();
    }

    [Test]
    public async Task CountAtLeast_InsufficientElements_ReturnsFalse()
    {
        IEnumerable<int> source = [1, 2];
        
        bool actual = source.CountAtLeast(3);
        
        await Assert.That(actual).IsFalse();
    }

    [Test]
    public async Task CountAtLeast_WithPredicate_SufficientMatches_ReturnsTrue()
    {
        IEnumerable<int> source = [1, 2, 3, 4, 5, 6];
        
        bool actual = source.CountAtLeast(x => x % 2 == 0, 3); // Even numbers: 2, 4, 6
        
        await Assert.That(actual).IsTrue();
    }

    [Test]
    public async Task CountAtLeast_WithPredicate_InsufficientMatches_ReturnsFalse()
    {
        IEnumerable<int> source = [1, 2, 3, 4, 5];
        
        bool actual = source.CountAtLeast(x => x % 2 == 0, 3); // Even numbers: 2, 4 (only 2)
        
        await Assert.That(actual).IsFalse();
    }

    [Test]
    public async Task CountAtLeast_WithNullSource_Throws()
    {
        IEnumerable<int>? source = null;
        
        await Assert.ThrowsAsync<ArgumentNullException>(() => 
#pragma warning disable CS8604 // Possible null reference argument.
            Task.FromResult(source.CountAtLeast(1)));
#pragma warning restore CS8604 // Possible null reference argument.
    }

    [Test]
    public async Task CountAtLeast_WithNullPredicate_Throws()
    {
        IEnumerable<int> source = [1, 2, 3];
        
        await Assert.ThrowsAsync<ArgumentNullException>(() => 
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            Task.FromResult(source.CountAtLeast(null, 1)));
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
    }
}