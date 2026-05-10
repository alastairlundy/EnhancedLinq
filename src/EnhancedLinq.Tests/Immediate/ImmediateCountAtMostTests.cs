using System.Linq;

namespace ExtendedLinq.Tests.Immediate;

public class ImmediateCountAtMostTests
{
    [Test]
    public async Task CountAtMost_WithNegativeCount_Throws()
    {
        IEnumerable<int> source = [1, 2, 3];
        
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => 
            Task.FromResult(source.CountAtMost(-1)));
    }

    [Test]
    public async Task CountAtMost_EmptyEnumerable_ReturnsTrue()
    {
        IEnumerable<int> source = Enumerable.Empty<int>();
        
        bool actual = source.CountAtMost(0);
        
        await Assert.That(actual).IsTrue();
    }

    [Test]
    public async Task CountAtMost_SufficientOrFewerElements_ReturnsTrue()
    {
        IEnumerable<int> source = [1, 2, 3];
        
        bool actual = source.CountAtMost(5); // Looking for at most 5, we have 3
        
        await Assert.That(actual).IsTrue();
    }

    [Test]
    public async Task CountAtMost_ExactlySpecifiedCount_ReturnsTrue()
    {
        IEnumerable<int> source = [1, 2, 3, 4];
        
        bool actual = source.CountAtMost(4); // Exactly 4 elements
        
        await Assert.That(actual).IsTrue();
    }

    [Test]
    public async Task CountAtMost_TooManyElements_ReturnsFalse()
    {
        IEnumerable<int> source = [1, 2, 3, 4, 5];
        
        bool actual = source.CountAtMost(3); // Looking for at most 3, we have 5
        
        await Assert.That(actual).IsFalse();
    }

    [Test]
    public async Task CountAtMost_WithPredicate_FewOrNoMatches_ReturnsTrue()
    {
        IEnumerable<int> source = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];
        
        bool actual = source.CountAtMost(x => x > 8, 2); // Numbers > 8: 9, 10 (2 total)
        
        await Assert.That(actual).IsTrue();
    }

    [Test]
    public async Task CountAtMost_WithPredicate_TooManyMatches_ReturnsFalse()
    {
        IEnumerable<int> source = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];
        
        bool actual = source.CountAtMost(x => x > 5, 3); // Numbers > 5: 6, 7, 8, 9, 10 (5 total)
        
        await Assert.That(actual).IsFalse();
    }

    [Test]
    public async Task CountAtMost_WithNullSource_Throws()
    {
        IEnumerable<int>? source = null;
        
        await Assert.ThrowsAsync<ArgumentNullException>(() => 
#pragma warning disable CS8604 // Possible null reference argument.
            Task.FromResult(source.CountAtMost(1)));
#pragma warning restore CS8604 // Possible null reference argument.
    }

    [Test]
    public async Task CountAtMost_WithNullPredicate_Throws()
    {
        IEnumerable<int> source = [1, 2, 3];
        
        await Assert.ThrowsAsync<ArgumentNullException>(() => 
            Task.FromResult(source.CountAtMost(null!, 1)));
    }
}