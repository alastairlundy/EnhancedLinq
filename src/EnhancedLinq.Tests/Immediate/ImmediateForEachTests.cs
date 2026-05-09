using System.Linq;

namespace ExtendedLinq.Tests.Immediate;

public class ImmediateForEachTests
{
    private readonly Faker _faker = new();

    [Test]
    public async Task ForEach_WithNullSource_Throws()
    {
        IEnumerable<int>? source = null;
        
        await Assert.ThrowsAsync<ArgumentNullException>(() => 
        {
#pragma warning disable CS8604 // Possible null reference argument.
            source.ForEach(_ => {});
#pragma warning restore CS8604 // Possible null reference argument.
            return Task.CompletedTask;
        });
    }

    [Test]
    public async Task ForEach_WithNullAction_Throws()
    {
        IEnumerable<int> source = [1, 2, 3];
        
        await Assert.ThrowsAsync<ArgumentNullException>(() => 
        {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            source.ForEach(null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
            return Task.CompletedTask;
        });
    }

    [Test]
    public async Task ForEach_EmptyEnumerable_DoesNotThrow()
    {
        IEnumerable<int> source = Enumerable.Empty<int>();
        
        // Should not throw
        int[] enumerable = source as int[] ?? source.ToArray();
        enumerable.ForEach(x => {});

        await Assert.That(enumerable).IsEquivalentTo(Enumerable.Empty<int>());
    }

    [Test]
    public async Task ForEach_PerformsActionOnEachElement()
    {
        IList<int> source = _faker.Make(Random.Shared.Next(1, 10), () => Random.Shared.Next(1, 1000));
        List<int> results = [];
        
        source.ForEach(x => results.Add(x * 2));
        
        IList<int> expected = source.Select(i => i * 2).ToList();
        
        await Assert.That(results).IsEquivalentTo(expected);
    }
}