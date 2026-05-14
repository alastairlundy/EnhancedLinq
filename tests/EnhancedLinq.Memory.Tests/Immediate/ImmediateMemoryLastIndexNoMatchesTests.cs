namespace EnhancedLinq.Memory.Tests.Immediate;

public class ImmediateMemoryLastIndexNoMatchesTests
{
    [Test]
    public async Task LastIndex_NonEmptySpan_ReturnsLengthMinusOne()
    {
        int[] arr = [5, 6, 7];
        Span<int> span = arr;

        int last = span.LastIndex;
        await Assert.That(last).IsEqualTo(2);

        ReadOnlySpan<int> ro = arr;
        await Assert.That(ro.LastIndex).IsEqualTo(2);

        Memory<int> mem = arr.AsMemory();
        await Assert.That(mem.LastIndex).IsEqualTo(2);
    }

    [Test]
    public void LastIndex_EmptySpan_Throws()
    {
        int[] arr = [];
        Memory<int> mem = arr.AsMemory();

        Assert.Throws<InvalidOperationException>(() => Console.WriteLine(mem.LastIndex));
    }

    [Test]
    public async Task HasNoMatches_Behaviour()
    {
        int[] arr = [1, 2, 3];
        Span<int> span = arr;

        bool none = span.HasNoMatches(x => x > 10);
        bool notNone = span.HasNoMatches(x => x % 2 == 0);
        
        await Assert.That(none).IsTrue();

        await Assert.That(notNone).IsFalse();
    }
}