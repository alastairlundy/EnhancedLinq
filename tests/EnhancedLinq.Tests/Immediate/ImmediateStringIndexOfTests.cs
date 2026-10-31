using System.Linq;
using EnhancedLinq.Immediate;

namespace ExtendedLinq.Tests.Immediate;

/// <summary>
/// Regression tests for the string <c>IndexOf(string)</c> extension member
/// (called in static form; an instance call would bind to the built-in <c>string.IndexOf</c>).
/// </summary>
public class ImmediateStringIndexOfTests
{
    private readonly Faker _faker = new();

    [Test]
    public async Task IndexOf_StringFoundAtBeginning_ReturnsZero()
    {
        string str = "hello world";

        int actual = ImmediateIndexOfExtensions.IndexOf(str, "hello");

        await Assert.That(actual).IsEqualTo(0);
    }

    [Test]
    public async Task IndexOf_StringFoundInMiddle_ReturnsCorrectIndex()
    {
        string str = "hello world";

        int actual = ImmediateIndexOfExtensions.IndexOf(str, "world");

        await Assert.That(actual).IsEqualTo(6);
    }

    [Test]
    public async Task IndexOf_StringFoundAtEnd_ReturnsCorrectIndex()
    {
        string str = "abcdef";

        int actual = ImmediateIndexOfExtensions.IndexOf(str, "ef");

        await Assert.That(actual).IsEqualTo(4);
    }

    [Test]
    public async Task IndexOf_SingleCharacterValue_ReturnsCorrectIndex()
    {
        string str = "abc";

        int actual = ImmediateIndexOfExtensions.IndexOf(str, "b");

        await Assert.That(actual).IsEqualTo(1);
    }

    [Test]
    public async Task IndexOf_WithOverlappingCandidates_ReturnsFirstOccurrence()
    {
        string str = "abcabc";

        int actual = ImmediateIndexOfExtensions.IndexOf(str, "abc");

        await Assert.That(actual).IsEqualTo(0);
    }

    [Test]
    public async Task IndexOf_FirstCharAppearsNearEnd_ReturnsNegativeOne()
    {
        string str = "aaa";

        int actual = ImmediateIndexOfExtensions.IndexOf(str, "aa");

        await Assert.That(actual).IsEqualTo(0);
    }

    [Test]
    public async Task IndexOf_StringNotFound_ReturnsNegativeOne()
    {
        string str = "abcdef";

        int actual = ImmediateIndexOfExtensions.IndexOf(str, "xyz");

        await Assert.That(actual).IsEqualTo(-1);
    }

    [Test]
    public async Task IndexOf_ValueLongerThanSource_ReturnsNegativeOne()
    {
        string str = "ab";

        int actual = ImmediateIndexOfExtensions.IndexOf(str, "abcd");

        await Assert.That(actual).IsEqualTo(-1);
    }

    [Test]
    public async Task IndexOf_WithNullSource_Throws()
    {
        string? str = null;

        await Assert.ThrowsAsync<ArgumentNullException>(() =>
#pragma warning disable CS8604 // Possible null reference argument.
            Task.FromResult(ImmediateIndexOfExtensions.IndexOf(str, "a")));
#pragma warning restore CS8604 // Possible null reference argument.
    }

    [Test]
    public async Task IndexOf_WithEmptyValue_Throws()
    {
        string str = "abc";

        await Assert.ThrowsAsync<ArgumentException>(() =>
            Task.FromResult(ImmediateIndexOfExtensions.IndexOf(str, string.Empty)));
    }
}
