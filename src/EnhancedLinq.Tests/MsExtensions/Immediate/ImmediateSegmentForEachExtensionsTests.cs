/*
    EnhancedLinq - Tests for MsExtensions Immediate ForEach
*/

using EnhancedLinq.MsExtensions.Immediate;

namespace ExtendedLinq.Tests.MsExtensions.Immediate;

using Microsoft.Extensions.Primitives;

public class ImmediateSegmentForEachExtensionsTests
{
    [Test]
    public async Task ForEach_WithNullAction_Throws()
    {
        StringSegment source = new("abc");

        await Assert.ThrowsAsync<NullReferenceException>(() => 
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            source.ForEach((Action<char>)null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
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
