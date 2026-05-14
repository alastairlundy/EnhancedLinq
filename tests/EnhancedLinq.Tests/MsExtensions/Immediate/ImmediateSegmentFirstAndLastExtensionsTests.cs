/*
    EnhancedLinq - Tests for Immediate First/Last
*/

using EnhancedLinq.MsExtensions.Immediate;

namespace ExtendedLinq.Tests.MsExtensions.Immediate;

using Microsoft.Extensions.Primitives;

public class ImmediateSegmentFirstAndLastExtensionsTests
{
    [Test]
    public async Task First_ReturnsFirstChar()
    {
        StringSegment source = new("xyz");

        char actual = source.First();

        await Assert.That(actual).IsEqualTo('x');
    }

    [Test]
    public async Task First_Empty_Throws()
    {
        StringSegment source = new("");

        await Assert.ThrowsAsync<ArgumentException>(() => Task.FromResult(source.First()));
    }

    [Test]
    public async Task First_Predicate_ReturnsMatchingChar()
    {
        StringSegment source = new("a1b2");

        char actual = source.First(char.IsDigit);

        await Assert.That(actual).IsEqualTo('1');
    }

    [Test]
    public async Task First_Predicate_NoMatch_Throws()
    {
        StringSegment source = new("abc");

        await Assert.ThrowsAsync<ArgumentException>(() => Task.FromResult(source.First(c => c == 'z')));
    }

    [Test]
    public async Task Last_ReturnsLastChar()
    {
        StringSegment source = new("abc");

        char actual = source.Last();

        await Assert.That(actual).IsEqualTo('c');
    }

    [Test]
    public async Task Last_Predicate_ReturnsLastMatchingChar()
    {
        StringSegment source = new("1a2b3");

        char actual = source.Last(char.IsDigit);

        await Assert.That(actual).IsEqualTo('3');
    }

    [Test]
    public async Task Last_Empty_Throws()
    {
        StringSegment source = new("");

        await Assert.ThrowsAsync<ArgumentException>(() => Task.FromResult(source.Last()));
    }
}
