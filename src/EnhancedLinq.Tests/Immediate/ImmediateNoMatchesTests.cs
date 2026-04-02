using System.Collections.Generic;
using System.Linq;

namespace ExtendedLinq.Tests.Immediate;

public class ImmediateNoMatchesTests
{
    private readonly Faker _faker = new();

    [Test]
    public async Task None_Empty_Enumerable_Throws()
    {
        await Assert.ThrowsAsync<ArgumentNullException>(() => Task.FromResult(
            ((IEnumerable<string>?)null).HasNoMatches(s => s.Length > 0)));
    }

    [Test]
    public async Task None_Empty_Enumerable_ReturnsTrue()
    {
        bool actual = Enumerable.Empty<string>().HasNoMatches(s => s.Length > 0);
        
        await Assert.That(actual).IsTrue();
    }

    [Test]
    public async Task None_Enumerable_NoMatches_ReturnsTrue()
    {
        IList<string> source = _faker.Make(Random.Shared.Next(1, 10), _ => _faker.Lorem.Word());

        bool actual = source.HasNoMatches(s => s.Length == 0);
        
        await Assert.That(actual).IsTrue();
    }

    [Test]
    public async Task None_Enumerable_Matches_ReturnsFalse()
    {
        IEnumerable<DateTime> source = _faker.MakeLazy(Random.Shared.Next(1, 10),
            _ => _faker.Date.Between(DateTime.Today.AddDays(1), DateTime.Today.AddDays(1000)));

        bool actual = source.HasNoMatches(d => d.Date > DateTime.Today.Date);
        
        await Assert.That(actual).IsFalse();
    }

    [Test]
    public async Task None_WithNullPredicate_Throws()
    {
        IEnumerable<string> source = ["a", "b", "c"];
        
        await Assert.ThrowsAsync<ArgumentNullException>(() => Task.FromResult(
            source.HasNoMatches(null!)));
    }
}