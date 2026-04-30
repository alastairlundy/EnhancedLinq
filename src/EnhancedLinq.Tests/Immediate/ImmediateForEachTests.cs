using System.Collections.Generic;
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
            source!.ForEach(x => {});
            return Task.CompletedTask;
        });
    }

    [Test]
    public async Task ForEach_WithNullAction_Throws()
    {
        IEnumerable<int> source = [1, 2, 3];
        
        await Assert.ThrowsAsync<ArgumentNullException>(() => 
        {
            source.ForEach(null!);
            return Task.CompletedTask;
        });
    }

    [Test]
    public async Task ForEach_EmptyEnumerable_DoesNotThrow()
    {
        IEnumerable<int> source = Enumerable.Empty<int>();
        
        // Should not throw
        source.ForEach(x => {});

        await Assert.That(source).IsEquivalentTo(Enumerable.Empty<int>());
    }

    [Test]
    public async Task ForEach_PerformsActionOnEachElement()
    {
        IList<int> source = _faker.Make(Random.Shared.Next(1, 10), () =>
        {
            return Random.Shared.Next(1, 1000);
        });
        List<int> results = [];
        
        source.ForEach(x => results.Add(x * 2));
        
        IList<int> expected = source.Select(i => i * 2).ToList();
        
        await Assert.That(results).IsEquivalentTo(expected);
    }
}