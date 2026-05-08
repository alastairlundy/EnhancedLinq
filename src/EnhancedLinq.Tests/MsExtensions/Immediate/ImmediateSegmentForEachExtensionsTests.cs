/*
    EnhancedLinq - Tests for MsExtensions Immediate ForEach
*/

using EnhancedLinq.MsExtensions.Immediate;

namespace ExtendedLinq.Tests.MsExtensions.Immediate;

using Microsoft.Extensions.Primitives;

public class ImmediateSegmentForEachExtensionsTests
{
    private readonly Faker _faker = new();

    [Test]
    public async Task ForEach_WithNullAction_Throws()
    {
        StringSegment source = new("abc");

        await Assert.ThrowsAsync<NullReferenceException>(() => 
        {
            source.ForEach((Action<char>)null!);
            return Task.CompletedTask;
        });
    }

    [Test]
    public async Task ForEach_PerformsActionOnEachElement()
    {
        StringSegment source = new("xyz");
        List<char> results = [];

        source.ForEach(c => results.Add(c));

        await Assert.That(results).IsEquivalentTo(new List<char> { 'x', 'y', 'z' });
    }

    [Test]
    public async Task ForEach_FuncMutatesSegment()
    {
        StringSegment source = new("abc");

        source.ForEach(c => char.ToUpper(c));

        await Assert.That(source.ToString()).IsEqualTo("ABC");
    }
}
